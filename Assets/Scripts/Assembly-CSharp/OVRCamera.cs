using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OVRCamera : OVRComponent
{
	private RenderTexture CameraTexture;

	private Material ColorOnlyMaterial;

	private Color QuadColor = Color.red;

	private float CameraTextureScale = 1f;

	private OVRCameraController CameraController;

	[HideInInspector]
	public Vector3 NeckPosition = new Vector3(0f, 0f, 0f);

	[HideInInspector]
	public Vector3 EyePosition = new Vector3(0f, 0.09f, 0.16f);

	private static Quaternion CameraOrientation = Quaternion.identity;

	private new void Awake()
	{
		base.Awake();
		if (ColorOnlyMaterial == null)
		{
			ColorOnlyMaterial = new Material("Shader \"Solid Color\" {\nProperties {\n_Color (\"Color\", Color) = (1,1,1)\n}\nSubShader {\nColor [_Color]\nPass {}\n}\n}");
		}
	}

	private new void Start()
	{
		base.Start();
		CameraController = base.gameObject.transform.parent.GetComponent<OVRCameraController>();
		if (CameraController == null)
		{
			Debug.LogWarning("WARNING: OVRCameraController not found!");
		}
		if (CameraTexture == null && CameraTextureScale != 1f)
		{
			int width = (int)((float)Screen.width / 2f * CameraTextureScale);
			int height = (int)((float)Screen.height * CameraTextureScale);
			CameraTexture = new RenderTexture(width, height, 24);
			CameraTexture.antiAliasing = QualitySettings.antiAliasing;
		}
	}

	private new void Update()
	{
		base.Update();
	}

	private void OnPreCull()
	{
		if (!CameraController.CallInPreRender)
		{
			SetCameraOrientation();
		}
	}

	private void OnPreRender()
	{
		if (CameraController.CallInPreRender)
		{
			SetCameraOrientation();
		}
		if (CameraController.WireMode)
		{
			GL.wireframe = true;
		}
		if (CameraTexture != null)
		{
			Graphics.SetRenderTarget(CameraTexture);
			GL.Clear(true, true, base.gameObject.GetComponent<Camera>().backgroundColor);
		}
	}

	private void OnPostRender()
	{
		if (CameraController.WireMode)
		{
			GL.wireframe = false;
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture source2 = source;
		if (CameraTexture != null)
		{
			source2 = CameraTexture;
		}
		Material material = null;
		if (CameraController.LensCorrection)
		{
			material = ((!CameraController.Chromatic) ? GetComponent<OVRLensCorrection>().GetMaterial(CameraController.PortraitMode) : GetComponent<OVRLensCorrection>().GetMaterial_CA(CameraController.PortraitMode));
		}
		if (material != null)
		{
			Graphics.Blit(source2, destination, material);
		}
		else
		{
			Graphics.Blit(source2, destination);
		}
		LatencyTest(destination);
	}

	private void SetCameraOrientation()
	{
		Quaternion identity = Quaternion.identity;
		Vector3 forward = Vector3.forward;
		if (base.gameObject.GetComponent<Camera>().depth == 0f)
		{
			if (CameraController.TrackerRotatesY)
			{
				Vector3 eulerAngles = base.gameObject.GetComponent<Camera>().transform.rotation.eulerAngles;
				eulerAngles.x = 0f;
				eulerAngles.z = 0f;
				base.gameObject.transform.parent.transform.eulerAngles = eulerAngles;
			}
			if (CameraController != null && CameraController.EnableOrientation)
			{
				if (!CameraController.PredictionOn)
				{
					OVRDevice.GetOrientation(0, ref CameraOrientation);
				}
				else
				{
					OVRDevice.GetPredictedOrientation(0, ref CameraOrientation);
				}
			}
			OVRDevice.ProcessLatencyInputs();
		}
		float yRotation = 0f;
		CameraController.GetYRotation(ref yRotation);
		identity = Quaternion.Euler(0f, yRotation, 0f);
		forward = identity * Vector3.forward;
		identity.SetLookRotation(forward, Vector3.up);
		Quaternion orientationOffset = Quaternion.identity;
		CameraController.GetOrientationOffset(ref orientationOffset);
		identity = orientationOffset * identity;
		if (CameraController != null)
		{
			identity *= CameraOrientation;
		}
		base.gameObject.GetComponent<Camera>().transform.rotation = identity;
		base.gameObject.GetComponent<Camera>().transform.position = base.gameObject.GetComponent<Camera>().transform.parent.transform.position + NeckPosition;
		base.gameObject.GetComponent<Camera>().transform.position += identity * EyePosition;
	}

	private void LatencyTest(RenderTexture dest)
	{
		byte r = 0;
		byte g = 0;
		byte b = 0;
		string text = Marshal.PtrToStringAnsi(OVRDevice.GetLatencyResultsString());
		if (text != null)
		{
			string text2 = "\n\n---------------------\nLATENCY TEST RESULTS:\n---------------------\n";
			text2 += text;
			text2 += "\n\n\n";
			MonoBehaviour.print(text2);
		}
		if (OVRDevice.DisplayLatencyScreenColor(ref r, ref g, ref b))
		{
			RenderTexture.active = dest;
			Material colorOnlyMaterial = ColorOnlyMaterial;
			QuadColor.r = (float)(int)r / 255f;
			QuadColor.g = (float)(int)g / 255f;
			QuadColor.b = (float)(int)b / 255f;
			colorOnlyMaterial.SetColor("_Color", QuadColor);
			GL.PushMatrix();
			colorOnlyMaterial.SetPass(0);
			GL.LoadOrtho();
			GL.Begin(7);
			GL.Vertex3(0.3f, 0.3f, 0f);
			GL.Vertex3(0.3f, 0.7f, 0f);
			GL.Vertex3(0.7f, 0.7f, 0f);
			GL.Vertex3(0.7f, 0.3f, 0f);
			GL.End();
			GL.PopMatrix();
		}
	}

	public void SetPerspectiveOffset(ref Vector3 offset)
	{
		base.gameObject.GetComponent<Camera>().ResetProjectionMatrix();
		Matrix4x4 identity = Matrix4x4.identity;
		identity.SetColumn(3, new Vector4(offset.x, offset.y, 0f, 1f));
		if (CameraController != null && CameraController.PortraitMode)
		{
			Vector3 zero = Vector3.zero;
			Quaternion q = Quaternion.Euler(0f, 0f, -90f);
			Vector3 one = Vector3.one;
			Matrix4x4 matrix4x = Matrix4x4.TRS(zero, q, one);
			base.gameObject.GetComponent<Camera>().projectionMatrix = matrix4x * identity * base.gameObject.GetComponent<Camera>().projectionMatrix;
		}
		else
		{
			base.gameObject.GetComponent<Camera>().projectionMatrix = identity * base.gameObject.GetComponent<Camera>().projectionMatrix;
		}
	}
}
