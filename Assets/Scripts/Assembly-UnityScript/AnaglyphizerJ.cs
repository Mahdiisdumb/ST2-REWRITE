using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[AddComponentMenu("Anaglyphizer/Anaglyph-izer Js Version")]
[RequireComponent(typeof(Camera))]
public class AnaglyphizerJ : MonoBehaviour
{
	private object leftEyeRT;

	private object rightEyeRT;

	private object leftEye;

	private object rightEye;

	public Material anaglyphMat;

	internal float zvalue;

	public bool enableKeys;

	public KeyCode downEyeDistance;

	public KeyCode upEyeDistance;

	public KeyCode downFocalDistance;

	public KeyCode upFocalDistance;

	public AnaglyphizerJ()
	{
		enableKeys = true;
		downEyeDistance = KeyCode.O;
		upEyeDistance = KeyCode.P;
		downFocalDistance = KeyCode.K;
		upFocalDistance = KeyCode.L;
	}

	public virtual void Start()
	{
		if (!anaglyphMat)
		{
			Debug.LogError("No Material Found Please Drag The material in the appropriate Field");
			enabled = false;
			return;
		}
		leftEye = new GameObject("leftEye", typeof(Camera));
		rightEye = new GameObject("rightEye", typeof(Camera));
		UnityRuntimeServices.Invoke(UnityRuntimeServices.GetProperty(leftEye, "camera"), "CopyFrom", new object[1] { GetComponent<Camera>() }, typeof(MonoBehaviour));
		UnityRuntimeServices.Invoke(UnityRuntimeServices.GetProperty(rightEye, "camera"), "CopyFrom", new object[1] { GetComponent<Camera>() }, typeof(MonoBehaviour));
		UnityRuntimeServices.Invoke(leftEye, "AddComponent", new object[1] { typeof(GUILayer) }, typeof(MonoBehaviour));
		UnityRuntimeServices.Invoke(rightEye, "AddComponent", new object[1] { typeof(GUILayer) }, typeof(MonoBehaviour));
		leftEyeRT = new RenderTexture(Screen.width, Screen.height, 24);
		rightEyeRT = new RenderTexture(Screen.width, Screen.height, 24);
		object value = leftEyeRT;
		object property = UnityRuntimeServices.GetProperty(leftEye, "camera");
		RuntimeServices.SetProperty(property, "targetTexture", value);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "camera", property)
		});
		object value2 = rightEyeRT;
		object property2 = UnityRuntimeServices.GetProperty(rightEye, "camera");
		RuntimeServices.SetProperty(property2, "targetTexture", value2);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "camera", property2)
		});
		Material material = anaglyphMat;
		object obj = leftEyeRT;
		if (!(obj is Texture))
		{
			obj = RuntimeServices.Coerce(obj, typeof(Texture));
		}
		material.SetTexture("_LeftTex", (Texture)obj);
		Material material2 = anaglyphMat;
		object obj2 = rightEyeRT;
		if (!(obj2 is Texture))
		{
			obj2 = RuntimeServices.Coerce(obj2, typeof(Texture));
		}
		material2.SetTexture("_RightTex", (Texture)obj2);
		float num = GetComponent<Camera>().depth - 2f;
		object property3 = UnityRuntimeServices.GetProperty(leftEye, "camera");
		RuntimeServices.SetProperty(property3, "depth", num);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "camera", property3)
		});
		float num2 = GetComponent<Camera>().depth - 1f;
		object property4 = UnityRuntimeServices.GetProperty(rightEye, "camera");
		RuntimeServices.SetProperty(property4, "depth", num2);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "camera", property4)
		});
		Vector3 vector = transform.position + transform.TransformDirection(0f - S3DV.eyeDistance, 0f, 0f);
		object property5 = UnityRuntimeServices.GetProperty(leftEye, "transform");
		RuntimeServices.SetProperty(property5, "position", vector);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "transform", property5)
		});
		Vector3 vector2 = transform.position + transform.TransformDirection(S3DV.eyeDistance, 0f, 0f);
		object property6 = UnityRuntimeServices.GetProperty(rightEye, "transform");
		RuntimeServices.SetProperty(property6, "position", vector2);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "transform", property6)
		});
		Matrix4x4 matrix4x = projectionMatrix(true);
		object property7 = UnityRuntimeServices.GetProperty(leftEye, "camera");
		RuntimeServices.SetProperty(property7, "projectionMatrix", matrix4x);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "camera", property7)
		});
		Matrix4x4 matrix4x2 = projectionMatrix(false);
		object property8 = UnityRuntimeServices.GetProperty(rightEye, "camera");
		RuntimeServices.SetProperty(property8, "projectionMatrix", matrix4x2);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "camera", property8)
		});
		UnityRuntimeServices.Invoke(UnityRuntimeServices.GetProperty(leftEye, "transform"), "LookAt", new object[1] { transform.position + transform.TransformDirection(Vector3.forward) * S3DV.focalDistance }, typeof(MonoBehaviour));
		UnityRuntimeServices.Invoke(UnityRuntimeServices.GetProperty(rightEye, "transform"), "LookAt", new object[1] { transform.position + transform.TransformDirection(Vector3.forward) * S3DV.focalDistance }, typeof(MonoBehaviour));
		Transform value3 = transform;
		object property9 = UnityRuntimeServices.GetProperty(leftEye, "transform");
		RuntimeServices.SetProperty(property9, "parent", value3);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "transform", property9)
		});
		Transform value4 = transform;
		object property10 = UnityRuntimeServices.GetProperty(rightEye, "transform");
		RuntimeServices.SetProperty(property10, "parent", value4);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "transform", property10)
		});
		GetComponent<Camera>().cullingMask = 0;
		GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f, 0f);
		GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
	}

	public virtual void Stop()
	{
	}

	public virtual void UpdateView()
	{
		float num = GetComponent<Camera>().depth - 2f;
		object property = UnityRuntimeServices.GetProperty(leftEye, "camera");
		RuntimeServices.SetProperty(property, "depth", num);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "camera", property)
		});
		float num2 = GetComponent<Camera>().depth - 1f;
		object property2 = UnityRuntimeServices.GetProperty(rightEye, "camera");
		RuntimeServices.SetProperty(property2, "depth", num2);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "camera", property2)
		});
		Vector3 vector = transform.position + transform.TransformDirection(0f - S3DV.eyeDistance, 0f, 0f);
		object property3 = UnityRuntimeServices.GetProperty(leftEye, "transform");
		RuntimeServices.SetProperty(property3, "position", vector);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "transform", property3)
		});
		Vector3 vector2 = transform.position + transform.TransformDirection(S3DV.eyeDistance, 0f, 0f);
		object property4 = UnityRuntimeServices.GetProperty(rightEye, "transform");
		RuntimeServices.SetProperty(property4, "position", vector2);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "transform", property4)
		});
		Matrix4x4 matrix4x = projectionMatrix(true);
		object property5 = UnityRuntimeServices.GetProperty(leftEye, "camera");
		RuntimeServices.SetProperty(property5, "projectionMatrix", matrix4x);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "camera", property5)
		});
		Matrix4x4 matrix4x2 = projectionMatrix(false);
		object property6 = UnityRuntimeServices.GetProperty(rightEye, "camera");
		RuntimeServices.SetProperty(property6, "projectionMatrix", matrix4x2);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "camera", property6)
		});
		UnityRuntimeServices.Invoke(UnityRuntimeServices.GetProperty(leftEye, "transform"), "LookAt", new object[1] { transform.position + transform.TransformDirection(Vector3.forward) * S3DV.focalDistance }, typeof(MonoBehaviour));
		UnityRuntimeServices.Invoke(UnityRuntimeServices.GetProperty(rightEye, "transform"), "LookAt", new object[1] { transform.position + transform.TransformDirection(Vector3.forward) * S3DV.focalDistance }, typeof(MonoBehaviour));
		Transform value = transform;
		object property7 = UnityRuntimeServices.GetProperty(leftEye, "transform");
		RuntimeServices.SetProperty(property7, "parent", value);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(leftEye, "transform", property7)
		});
		Transform value2 = transform;
		object property8 = UnityRuntimeServices.GetProperty(rightEye, "transform");
		RuntimeServices.SetProperty(property8, "parent", value2);
		UnityRuntimeServices.PropagateValueTypeChanges(new UnityRuntimeServices.ValueTypeChange[1]
		{
			new UnityRuntimeServices.MemberValueTypeChange(rightEye, "transform", property8)
		});
	}

	public virtual void LateUpdate()
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
				S3DV.focalDistance = (int)((float)S3DV.focalDistance + num2);
			}
			else if (Input.GetKeyDown(downFocalDistance))
			{
				S3DV.focalDistance = (int)((float)S3DV.focalDistance - num2);
			}
		}
	}

	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
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

	public virtual Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
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

	public virtual Matrix4x4 projectionMatrix(bool isLeftEye)
	{
		float num = default(float);
		float num2 = default(float);
		float num3 = default(float);
		float num4 = default(float);
		float num5 = default(float);
		num5 = GetComponent<Camera>().fieldOfView / 180f * (float)Math.PI;
		float aspect = GetComponent<Camera>().aspect;
		num3 = GetComponent<Camera>().nearClipPlane * Mathf.Tan(num5 * 0.5f);
		num4 = GetComponent<Camera>().nearClipPlane / (float)S3DV.focalDistance;
		if (isLeftEye)
		{
			num = (0f - aspect) * num3 + S3DV.eyeDistance * num4;
			num2 = aspect * num3 + S3DV.eyeDistance * num4;
		}
		else
		{
			num = (0f - aspect) * num3 - S3DV.eyeDistance * num4;
			num2 = aspect * num3 - S3DV.eyeDistance * num4;
		}
		return PerspectiveOffCenter(num, num2, 0f - num3, num3, GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);
	}

	public virtual void Main()
	{
	}
}
