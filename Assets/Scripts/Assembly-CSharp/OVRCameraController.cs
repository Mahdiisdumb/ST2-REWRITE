using UnityEngine;

public class OVRCameraController : OVRComponent
{
	private bool UpdateCamerasDirtyFlag;

	private Camera CameraLeft;

	private Camera CameraRight;

	private float LensOffsetLeft;

	private float LensOffsetRight;

	private float AspectRatio = 1f;

	private float DistK0;

	private float DistK1;

	private float DistK2;

	private float DistK3;

	private Quaternion OrientationOffset = Quaternion.identity;

	private float YRotation;

	[SerializeField]
	private float ipd = 0.064f;

	[SerializeField]
	private float verticalFOV = 90f;

	public Vector3 CameraRootPosition = new Vector3(0f, 1f, 0f);

	public Vector3 NeckPosition = new Vector3(0f, 0.7f, 0f);

	public Vector3 EyeCenterPosition = new Vector3(0f, 0.15f, 0.09f);

	public bool UsePlayerEyeHeight;

	private bool PrevUsePlayerEyeHeight;

	public Transform FollowOrientation;

	public bool TrackerRotatesY;

	public bool PortraitMode;

	private bool PrevPortraitMode;

	public bool EnableOrientation = true;

	public bool PredictionOn = true;

	public bool CallInPreRender;

	public bool WireMode;

	public bool LensCorrection = true;

	public bool Chromatic = true;

	[SerializeField]
	private Color backgroundColor = new Color(0.192f, 0.302f, 0.475f, 1f);

	[SerializeField]
	private float nearClipPlane = 0.15f;

	[SerializeField]
	private float farClipPlane = 1000f;

	public float IPD
	{
		get
		{
			return ipd;
		}
		set
		{
			ipd = value;
			UpdateCamerasDirtyFlag = true;
		}
	}

	public float VerticalFOV
	{
		get
		{
			return verticalFOV;
		}
		set
		{
			verticalFOV = value;
			UpdateCamerasDirtyFlag = true;
		}
	}

	public Color BackgroundColor
	{
		get
		{
			return backgroundColor;
		}
		set
		{
			backgroundColor = value;
			UpdateCamerasDirtyFlag = true;
		}
	}

	public float NearClipPlane
	{
		get
		{
			return nearClipPlane;
		}
		set
		{
			nearClipPlane = value;
			UpdateCamerasDirtyFlag = true;
		}
	}

	public float FarClipPlane
	{
		get
		{
			return farClipPlane;
		}
		set
		{
			farClipPlane = value;
			UpdateCamerasDirtyFlag = true;
		}
	}

	private new void Awake()
	{
		base.Awake();
		Camera[] componentsInChildren = base.gameObject.GetComponentsInChildren<Camera>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].name == "CameraLeft")
			{
				CameraLeft = componentsInChildren[i];
			}
			if (componentsInChildren[i].name == "CameraRight")
			{
				CameraRight = componentsInChildren[i];
			}
		}
		if (CameraLeft == null || CameraRight == null)
		{
			Debug.LogWarning("WARNING: Unity Cameras in OVRCameraController not found!");
		}
	}

	private new void Start()
	{
		base.Start();
		InitCameraControllerVariables();
		UpdateCamerasDirtyFlag = true;
		UpdateCameras();
		SetMaximumVisualQuality();
	}

	private new void Update()
	{
		base.Update();
		UpdateCameras();
	}

	public void InitCameraControllerVariables()
	{
		OVRDevice.GetIPD(ref ipd);
		OVRDevice.CalculatePhysicalLensOffsets(ref LensOffsetLeft, ref LensOffsetRight);
		VerticalFOV = OVRDevice.VerticalFOV();
		AspectRatio = OVRDevice.CalculateAspectRatio();
		OVRDevice.GetDistortionCorrectionCoefficients(ref DistK0, ref DistK1, ref DistK2, ref DistK3);
		if (!PortraitMode)
		{
			PortraitMode = OVRDevice.RenderPortraitMode();
		}
		PrevPortraitMode = false;
		if (FollowOrientation != null)
		{
			OrientationOffset = FollowOrientation.rotation;
		}
		else
		{
			OrientationOffset = base.transform.rotation;
		}
	}

	private void UpdateCameras()
	{
		if (FollowOrientation != null)
		{
			OrientationOffset = FollowOrientation.rotation;
		}
		SetPortraitMode();
		UpdatePlayerEyeHeight();
		if (UpdateCamerasDirtyFlag)
		{
			float distOffset = 0.5f + LensOffsetLeft * 0.5f;
			float lensOffsetLeft = LensOffsetLeft;
			float eyePositionOffset = (0f - IPD) * 0.5f;
			ConfigureCamera(ref CameraLeft, distOffset, lensOffsetLeft, eyePositionOffset);
			distOffset = 0.5f + LensOffsetRight * 0.5f;
			lensOffsetLeft = LensOffsetRight;
			eyePositionOffset = IPD * 0.5f;
			ConfigureCamera(ref CameraRight, distOffset, lensOffsetLeft, eyePositionOffset);
			UpdateCamerasDirtyFlag = false;
		}
	}

	private bool ConfigureCamera(ref Camera camera, float distOffset, float perspOffset, float eyePositionOffset)
	{
		Vector3 offset = Vector3.zero;
		Vector3 eyeCenterPosition = EyeCenterPosition;
		camera.fieldOfView = VerticalFOV;
		camera.aspect = AspectRatio;
		camera.GetComponent<OVRLensCorrection>()._Center.x = distOffset;
		ConfigureCameraLensCorrection(ref camera);
		offset.x = perspOffset;
		camera.GetComponent<OVRCamera>().SetPerspectiveOffset(ref offset);
		camera.GetComponent<OVRCamera>().NeckPosition = NeckPosition;
		eyeCenterPosition.x = eyePositionOffset;
		camera.GetComponent<OVRCamera>().EyePosition = eyeCenterPosition;
		camera.backgroundColor = BackgroundColor;
		camera.nearClipPlane = NearClipPlane;
		camera.farClipPlane = FarClipPlane;
		return true;
	}

	private void ConfigureCameraLensCorrection(ref Camera camera)
	{
		float num = 1f / OVRDevice.DistortionScale();
		float num2 = OVRDevice.CalculateAspectRatio();
		float num3 = 1f;
		float num4 = 1f;
		OVRLensCorrection component = camera.GetComponent<OVRLensCorrection>();
		component._Scale.x = num3 / 2f * num;
		component._Scale.y = num4 / 2f * num * num2;
		component._ScaleIn.x = 2f / num3;
		component._ScaleIn.y = 2f / num4 / num2;
		component._HmdWarpParam.x = DistK0;
		component._HmdWarpParam.y = DistK1;
		component._HmdWarpParam.z = DistK2;
	}

	private void SetPortraitMode()
	{
		if (PortraitMode != PrevPortraitMode)
		{
			Rect rect = new Rect(0f, 0f, 0f, 0f);
			if (PortraitMode)
			{
				rect.x = 0f;
				rect.y = 0.5f;
				rect.width = 1f;
				rect.height = 0.5f;
				CameraLeft.rect = rect;
				rect.x = 0f;
				rect.y = 0f;
				rect.width = 1f;
				rect.height = 0.499999f;
				CameraRight.rect = rect;
			}
			else
			{
				rect.x = 0f;
				rect.y = 0f;
				rect.width = 0.5f;
				rect.height = 1f;
				CameraLeft.rect = rect;
				rect.x = 0.5f;
				rect.y = 0f;
				rect.width = 0.499999f;
				rect.height = 1f;
				CameraRight.rect = rect;
			}
		}
		PrevPortraitMode = PortraitMode;
	}

	private void UpdatePlayerEyeHeight()
	{
		if (UsePlayerEyeHeight && !PrevUsePlayerEyeHeight)
		{
			float eyeHeight = 0f;
			if (OVRDevice.GetPlayerEyeHeight(ref eyeHeight))
			{
				NeckPosition.y = eyeHeight - CameraRootPosition.y - EyeCenterPosition.y;
			}
		}
		PrevUsePlayerEyeHeight = UsePlayerEyeHeight;
	}

	public void SetCameras(ref Camera cameraLeft, ref Camera cameraRight)
	{
		CameraLeft = cameraLeft;
		CameraRight = cameraRight;
		UpdateCamerasDirtyFlag = true;
	}

	public void GetIPD(ref float ipd)
	{
		ipd = IPD;
	}

	public void SetIPD(float ipd)
	{
		IPD = ipd;
		UpdateCamerasDirtyFlag = true;
	}

	public void GetVerticalFOV(ref float verticalFOV)
	{
		verticalFOV = VerticalFOV;
	}

	public void SetVerticalFOV(float verticalFOV)
	{
		VerticalFOV = verticalFOV;
		UpdateCamerasDirtyFlag = true;
	}

	public void GetAspectRatio(ref float aspecRatio)
	{
		aspecRatio = AspectRatio;
	}

	public void SetAspectRatio(float aspectRatio)
	{
		AspectRatio = aspectRatio;
		UpdateCamerasDirtyFlag = true;
	}

	public void GetDistortionCoefs(ref float distK0, ref float distK1, ref float distK2, ref float distK3)
	{
		distK0 = DistK0;
		distK1 = DistK1;
		distK2 = DistK2;
		distK3 = DistK3;
	}

	public void SetDistortionCoefs(float distK0, float distK1, float distK2, float distK3)
	{
		DistK0 = distK0;
		DistK1 = distK1;
		DistK2 = distK2;
		DistK3 = distK3;
		UpdateCamerasDirtyFlag = true;
	}

	public void GetCameraRootPosition(ref Vector3 cameraRootPosition)
	{
		cameraRootPosition = CameraRootPosition;
	}

	public void SetCameraRootPosition(ref Vector3 cameraRootPosition)
	{
		CameraRootPosition = cameraRootPosition;
		UpdateCamerasDirtyFlag = true;
	}

	public void GetNeckPosition(ref Vector3 neckPosition)
	{
		neckPosition = NeckPosition;
	}

	public void SetNeckPosition(Vector3 neckPosition)
	{
		if (!UsePlayerEyeHeight)
		{
			NeckPosition = neckPosition;
			UpdateCamerasDirtyFlag = true;
		}
	}

	public void GetEyeCenterPosition(ref Vector3 eyeCenterPosition)
	{
		eyeCenterPosition = EyeCenterPosition;
	}

	public void SetEyeCenterPosition(Vector3 eyeCenterPosition)
	{
		EyeCenterPosition = eyeCenterPosition;
		UpdateCamerasDirtyFlag = true;
	}

	public void GetOrientationOffset(ref Quaternion orientationOffset)
	{
		orientationOffset = OrientationOffset;
	}

	public void SetOrientationOffset(Quaternion orientationOffset)
	{
		OrientationOffset = orientationOffset;
	}

	public void GetYRotation(ref float yRotation)
	{
		yRotation = YRotation;
	}

	public void SetYRotation(float yRotation)
	{
		YRotation = yRotation;
	}

	public void GetTrackerRotatesY(ref bool trackerRotatesY)
	{
		trackerRotatesY = TrackerRotatesY;
	}

	public void SetTrackerRotatesY(bool trackerRotatesY)
	{
		TrackerRotatesY = trackerRotatesY;
	}

	public bool GetCameraOrientationEulerAngles(ref Vector3 angles)
	{
		if (CameraRight == null)
		{
			return false;
		}
		angles = CameraRight.transform.rotation.eulerAngles;
		return true;
	}

	public bool GetCameraOrientation(ref Quaternion quaternion)
	{
		if (CameraRight == null)
		{
			return false;
		}
		quaternion = CameraRight.transform.rotation;
		return true;
	}

	public bool GetCameraPosition(ref Vector3 position)
	{
		if (CameraRight == null)
		{
			return false;
		}
		position = CameraRight.transform.position;
		return true;
	}

	public void GetCamera(ref Camera camera)
	{
		camera = CameraRight;
	}

	public bool AttachGameObjectToCamera(ref GameObject gameObject)
	{
		if (CameraRight == null)
		{
			return false;
		}
		gameObject.transform.parent = CameraRight.transform;
		return true;
	}

	public bool DetachGameObjectFromCamera(ref GameObject gameObject)
	{
		if (CameraRight != null && CameraRight.transform == gameObject.transform.parent)
		{
			gameObject.transform.parent = null;
			return true;
		}
		return false;
	}

	public bool GetPlayerEyeHeight(ref float eyeHeight)
	{
		eyeHeight = CameraRootPosition.y + NeckPosition.y + EyeCenterPosition.y;
		return true;
	}

	public void SetMaximumVisualQuality()
	{
		QualitySettings.softVegetation = true;
		QualitySettings.maxQueuedFrames = 0;
		QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
		QualitySettings.vSyncCount = 1;
	}
}
