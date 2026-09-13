using UnityEngine;

public class CustardLabel : MonoBehaviour
{
	private void Awake()
	{
		PlayerPrefs.SetInt("custardamount", 15);
	}

	private void Update()
	{
		int @int = PlayerPrefs.GetInt("custardamount");
		GetComponent<TextMesh>().text = @int.ToString();
	}
}
