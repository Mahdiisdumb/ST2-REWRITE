using CodeStage.AntiCheat.Detectors;
using Photon;
using UnityEngine;

public class AntiCheat : Photon.MonoBehaviour
{
	private void Start()
	{
		if (base.photonView.isMine)
		{
			SpeedHackDetector.StartDetection(OnSpeedHackDetected);
		}
	}

	private void OnSpeedHackDetected()
	{
		UnityEngine.MonoBehaviour.print("ERROR");
		Application.Quit();
	}
}
