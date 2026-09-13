using UnityEngine;

public class Menu2Toggle : MonoBehaviour
{
	public bool Value;

	public Texture2D trueTexture;

	public Texture2D falseTexture;

	public string saveName;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
		int @int = PlayerPrefs.GetInt(saveName);
		if (@int == 1)
		{
			Value = true;
		}
		else
		{
			Value = false;
		}
		if (Value)
		{
			GetComponent<Renderer>().material.mainTexture = trueTexture;
		}
		else
		{
			GetComponent<Renderer>().material.mainTexture = falseTexture;
		}
	}

	private void OnMouseDown()
	{
		Value = !Value;
		if (Value)
		{
			GetComponent<Renderer>().material.mainTexture = trueTexture;
		}
		else
		{
			GetComponent<Renderer>().material.mainTexture = falseTexture;
		}
		if (Value)
		{
			PlayerPrefs.SetInt(saveName, 1);
		}
		else
		{
			PlayerPrefs.SetInt(saveName, 0);
		}
	}
}
