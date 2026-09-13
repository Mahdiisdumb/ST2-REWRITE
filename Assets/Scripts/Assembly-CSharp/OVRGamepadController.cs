using System.Runtime.InteropServices;
using UnityEngine;

public class OVRGamepadController : MonoBehaviour
{
	public enum Axis
	{
		LeftXAxis = 0,
		LeftYAxis = 1,
		RightXAxis = 2,
		RightYAxis = 3,
		LeftTrigger = 4,
		RightTrigger = 5
	}

	public enum Button
	{
		A = 0,
		B = 1,
		X = 2,
		Y = 3,
		Up = 4,
		Down = 5,
		Left = 6,
		Right = 7,
		Start = 8,
		Back = 9,
		LStick = 10,
		RStick = 11,
		L1 = 12,
		R1 = 13
	}

	private static bool GPC_Available;

	[DllImport("OculusPlugin")]
	private static extern bool OVR_GamepadController_Initialize();

	[DllImport("OculusPlugin")]
	private static extern bool OVR_GamepadController_Destroy();

	[DllImport("OculusPlugin")]
	private static extern bool OVR_GamepadController_Update();

	[DllImport("OculusPlugin")]
	private static extern float OVR_GamepadController_GetAxis(int axis);

	[DllImport("OculusPlugin")]
	private static extern bool OVR_GamepadController_GetButton(int button);

	public static bool GPC_Initialize()
	{
		return OVR_GamepadController_Initialize();
	}

	public static bool GPC_Destroy()
	{
		return OVR_GamepadController_Destroy();
	}

	public static bool GPC_Update()
	{
		return OVR_GamepadController_Update();
	}

	public static float GPC_GetAxis(int axis)
	{
		return OVR_GamepadController_GetAxis(axis);
	}

	public static bool GPC_GetButton(int button)
	{
		return OVR_GamepadController_GetButton(button);
	}

	public static bool GPC_IsAvailable()
	{
		return GPC_Available;
	}

	private void GPC_Test()
	{
		Debug.Log(string.Format("LT:{0:F3} RT:{1:F3} LX:{2:F3} LY:{3:F3} RX:{4:F3} RY:{5:F3}", GPC_GetAxis(4), GPC_GetAxis(5), GPC_GetAxis(0), GPC_GetAxis(1), GPC_GetAxis(2), GPC_GetAxis(3)));
		Debug.Log(string.Format("A:{0} B:{1} X:{2} Y:{3} U:{4} D:{5} L:{6} R:{7} SRT:{8} BK:{9} LS:{10} RS:{11} L1{12} R1{13}", GPC_GetButton(0), GPC_GetButton(1), GPC_GetButton(2), GPC_GetButton(3), GPC_GetButton(4), GPC_GetButton(5), GPC_GetButton(6), GPC_GetButton(7), GPC_GetButton(8), GPC_GetButton(9), GPC_GetButton(10), GPC_GetButton(11), GPC_GetButton(12), GPC_GetButton(13)));
	}

	private void Awake()
	{
	}

	private void Start()
	{
		GPC_Available = GPC_Initialize();
	}

	private void Update()
	{
		GPC_Available = GPC_Update();
	}

	private void OnDestroy()
	{
		GPC_Destroy();
		GPC_Available = false;
	}
}
