using UnityEngine;

public class Menu2Quit : MonoBehaviour
{
	public Color overColor;

	public Color exitColor;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
	}

	private void OnMouseDown()
	{
		Application.Quit();
	}

	private void OnMouseOver()
	{
		GetComponent<Renderer>().material.color = overColor;
	}

	private void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = exitColor;
	}
}
