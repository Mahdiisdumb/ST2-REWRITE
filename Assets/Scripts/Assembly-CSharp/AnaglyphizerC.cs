using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Anaglyphizer/Anaglyph-izer Cs Version")]
public class AnaglyphizerC : MonoBehaviour
{
	public class S3DV
	{
		internal static float eyeDistance = 0.02f;

		internal static float focalDistance = 10f;
	}

	private RenderTexture leftEyeRT;

	private RenderTexture rightEyeRT;

	private GameObject leftEye;

	private GameObject rightEye;

	public Material anaglyphMat;

	internal float zvalue;

	public bool enableKeys = true;

	public KeyCode downEyeDistance = KeyCode.O;

	public KeyCode upEyeDistance = KeyCode.P;

	public KeyCode downFocalDistance = KeyCode.K;

	public KeyCode upFocalDistance = KeyCode.L;

	public bool useProjectionMatrix;

	private void Start()
	{
		if (anaglyphMat == null)
		{
			Debug.LogError("No Material Found Please Drag The material in the appropriate Field");
			base.enabled = false;
			return;
		}
		leftEye = new GameObject("leftEye", typeof(Camera));
		rightEye = new GameObject("rightEye", typeof(Camera));
		leftEye.GetComponent<Camera>().CopyFrom(GetComponent<Camera>());
		rightEye.GetComponent<Camera>().CopyFrom(GetComponent<Camera>());
		leftEye.AddComponent<GUILayer>();
		rightEye.AddComponent<GUILayer>();
		leftEyeRT = new RenderTexture(Screen.width, Screen.height, 24);
		rightEyeRT = new RenderTexture(Screen.width, Screen.height, 24);
		leftEye.GetComponent<Camera>().targetTexture = leftEyeRT;
		rightEye.GetComponent<Camera>().targetTexture = rightEyeRT;
		anaglyphMat.SetTexture("_LeftTex", leftEyeRT);
		anaglyphMat.SetTexture("_RightTex", rightEyeRT);
		leftEye.GetComponent<Camera>().depth = GetComponent<Camera>().depth - 2f;
		rightEye.GetComponent<Camera>().depth = GetComponent<Camera>().depth - 1f;
		leftEye.transform.position = base.transform.position + base.transform.TransformDirection(0f - S3DV.eyeDistance, 0f, 0f);
		rightEye.transform.position = base.transform.position + base.transform.TransformDirection(S3DV.eyeDistance, 0f, 0f);
		if (!useProjectionMatrix)
		{
			leftEye.transform.LookAt(base.transform.position + base.transform.TransformDirection(Vector3.forward) * S3DV.focalDistance);
			rightEye.transform.LookAt(base.transform.position + base.transform.TransformDirection(Vector3.forward) * S3DV.focalDistance);
		}
		else
		{
			leftEye.transform.rotation = base.transform.rotation;
			rightEye.transform.rotation = base.transform.rotation;
			leftEye.GetComponent<Camera>().projectionMatrix = projectionMatrix(true);
			rightEye.GetComponent<Camera>().projectionMatrix = projectionMatrix(false);
		}
		leftEye.transform.parent = base.transform;
		rightEye.transform.parent = base.transform;
		GetComponent<Camera>().cullingMask = 0;
		GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f, 0f);
		GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
	}

	private void Stop()
	{
	}

	private void UpdateView()
	{
		leftEye.GetComponent<Camera>().depth = GetComponent<Camera>().depth - 2f;
		rightEye.GetComponent<Camera>().depth = GetComponent<Camera>().depth - 1f;
		leftEye.transform.position = base.transform.position + base.transform.TransformDirection(0f - S3DV.eyeDistance, 0f, 0f);
		rightEye.transform.position = base.transform.position + base.transform.TransformDirection(S3DV.eyeDistance, 0f, 0f);
		if (!useProjectionMatrix)
		{
			leftEye.transform.LookAt(base.transform.position + base.transform.TransformDirection(Vector3.forward) * S3DV.focalDistance);
			rightEye.transform.LookAt(base.transform.position + base.transform.TransformDirection(Vector3.forward) * S3DV.focalDistance);
		}
		else
		{
			leftEye.transform.rotation = base.transform.rotation;
			rightEye.transform.rotation = base.transform.rotation;
			leftEye.GetComponent<Camera>().projectionMatrix = projectionMatrix(true);
			rightEye.GetComponent<Camera>().projectionMatrix = projectionMatrix(false);
		}
		leftEye.transform.parent = base.transform;
		rightEye.transform.parent = base.transform;
	}

	private void LateUpdate()
	{
		UpdateView();
		if (enableKeys)
		{
			float num = 0.01f;
			if (Input.GetKeyDown(upEyeDistance))
			{
				S3DV.eyeDistance += num;
			}
			else if (Input.GetKeyDown(downEyeDistance))
			{
				S3DV.eyeDistance -= num;
			}
			float num2 = 0.5f;
			if (Input.GetKeyDown(upFocalDistance))
			{
				S3DV.focalDistance += num2;
			}
			else if (Input.GetKeyDown(downFocalDistance))
			{
				S3DV.focalDistance -= num2;
			}
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture.active = destination;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < anaglyphMat.passCount; i++)
		{
			anaglyphMat.SetPass(i);
			DrawQuad();
		}
		GL.PopMatrix();
	}

	private void DrawQuad()
	{
		GL.Begin(7);
		GL.TexCoord2(0f, 0f);
		GL.Vertex3(0f, 0f, zvalue);
		GL.TexCoord2(1f, 0f);
		GL.Vertex3(1f, 0f, zvalue);
		GL.TexCoord2(1f, 1f);
		GL.Vertex3(1f, 1f, zvalue);
		GL.TexCoord2(0f, 1f);
		GL.Vertex3(0f, 1f, zvalue);
		GL.End();
	}

	private Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
	{
		float value = 2f * near / (right - left);
		float value2 = 2f * near / (top - bottom);
		float value3 = (right + left) / (right - left);
		float value4 = (top + bottom) / (top - bottom);
		float value5 = (0f - (far + near)) / (far - near);
		float value6 = (0f - 2f * far * near) / (far - near);
		float value7 = -1f;
		Matrix4x4 result = default(Matrix4x4);
		result[0, 0] = value;
		result[0, 1] = 0f;
		result[0, 2] = value3;
		result[0, 3] = 0f;
		result[1, 0] = 0f;
		result[1, 1] = value2;
		result[1, 2] = value4;
		result[1, 3] = 0f;
		result[2, 0] = 0f;
		result[2, 1] = 0f;
		result[2, 2] = value5;
		result[2, 3] = value6;
		result[3, 0] = 0f;
		result[3, 1] = 0f;
		result[3, 2] = value7;
		result[3, 3] = 0f;
		return result;
	}

	private Matrix4x4 projectionMatrix(bool isLeftEye)
	{
		float num = GetComponent<Camera>().fieldOfView / 180f * (float)Math.PI;
		float aspect = GetComponent<Camera>().aspect;
		float num2 = GetComponent<Camera>().nearClipPlane * Mathf.Tan(num * 0.5f);
		float num3 = GetComponent<Camera>().nearClipPlane / S3DV.focalDistance;
		float left;
		float right;
		if (isLeftEye)
		{
			left = (0f - aspect) * num2 + S3DV.eyeDistance * num3;
			right = aspect * num2 + S3DV.eyeDistance * num3;
		}
		else
		{
			left = (0f - aspect) * num2 - S3DV.eyeDistance * num3;
			right = aspect * num2 - S3DV.eyeDistance * num3;
		}
		return PerspectiveOffCenter(left, right, 0f - num2, num2, GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);
	}
}
