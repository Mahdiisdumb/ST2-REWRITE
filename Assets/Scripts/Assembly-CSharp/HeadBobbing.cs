using System;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
	public float bobbingFreq = 1.8f;

	private float walkfeq;

	private float runfeq;

	private float bobbingFreqCached;

	public float bobbingRatio = 0.08f;

	private float phase = (float)Math.PI;

	private CharacterMotor characterMotor;

	private CharacterController characterController;

	private float height;

	private float bobbingAmount;

	private float heightDependency;

	private float currentBobbing;

	private float bobbingDelta;

	private int stateMask;

	private const int isWalking = 1;

	private const int isStepping = 2;

	private const int isStopping = 4;

	private Vector3 OriginalCameraLocalPosition;

	private void Awake()
	{
		walkfeq = bobbingFreq;
		runfeq = bobbingFreq * 2f;
	}

	private void Start()
	{
		characterMotor = GetComponent<CharacterMotor>();
		characterController = GetComponent<CharacterController>();
		height = characterController.height;
		OriginalCameraLocalPosition = Camera.main.transform.localPosition;
		heightDependency = base.transform.localScale.y * height / 2f;
		currentBobbing = 0f;
		bobbingFreqCached = bobbingFreq;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			bobbingFreq = runfeq;
		}
		else
		{
			bobbingFreq = walkfeq;
		}
		bobbingAmount = bobbingRatio * heightDependency;
		if (characterMotor.inputMoveDirection != Vector3.zero && characterMotor.IsGrounded())
		{
			UpdatePhaseAndBobbingDelta();
			stateMask |= 1;
			stateMask &= -5;
		}
		else if ((stateMask & 1) > 0)
		{
			stateMask |= 4;
			UpdatePhaseAndBobbingDelta((phase > 0f) ? 1 : (-1));
			if ((stateMask & 2) > 0)
			{
				Camera.main.transform.localPosition = OriginalCameraLocalPosition;
				stateMask &= -2;
				phase = (float)Math.PI;
			}
		}
		else
		{
			bobbingDelta = 0f;
			stateMask = 0;
		}
		Camera.main.transform.Translate(Vector3.up * bobbingDelta, Space.World);
	}

	private void UpdatePhaseAndBobbingDelta()
	{
		UpdatePhaseAndBobbingDelta(1f);
	}

	private void UpdatePhaseAndBobbingDelta(float direction)
	{
		float num = (float)Math.PI * 2f;
		float num2 = currentBobbing;
		currentBobbing = (Mathf.Cos(phase) + 1f) * bobbingAmount;
		bobbingDelta = currentBobbing - num2;
		phase += direction * (num * Time.deltaTime * bobbingFreq);
		if (Mathf.Abs(phase) > (float)Math.PI)
		{
			phase -= direction * num;
			stateMask |= 2;
		}
		else
		{
			stateMask &= -3;
		}
	}

	public bool getIsStepping()
	{
		return (2 & stateMask) > 0;
	}
}
