using UnityEngine;

public class Menu2AudioHorizontalSlider : MonoBehaviour
{
	public float maxXPos;

	public float minXPos;

	public int maxValue;

	public int Value;

	public string saveName;

	private bool move;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
		Vector3 size = GetComponent<BoxCollider>().size;
		GetComponent<BoxCollider>().size = new Vector3(600f, size.y, size.z);
		if (!PlayerPrefs.HasKey(saveName))
		{
			PlayerPrefs.SetInt(saveName, maxValue);
		}
		Value = PlayerPrefs.GetInt(saveName);
		float num = Vector3.Distance(new Vector3(minXPos, 0f, 0f), new Vector3(maxXPos, 0f, 0f));
		float num2 = Vector3.Distance(new Vector3(0f, 0f, 0f), new Vector3(maxValue, 0f, 0f));
		float x = minXPos + num / num2 * (float)Value;
		base.transform.localPosition = new Vector3(x, base.transform.localPosition.y, 0f);
	}

	private void Update()
	{
		if (!move)
		{
			return;
		}
		Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, base.transform.position.z - 9f));
		if (0f - vector.x > maxXPos)
		{
			base.transform.localPosition = new Vector3(maxXPos, base.transform.localPosition.y, 0f);
			Value = maxValue;
			PlayerPrefs.SetInt(saveName, Value);
			return;
		}
		if (0f - vector.x < minXPos)
		{
			base.transform.localPosition = new Vector3(minXPos, base.transform.localPosition.y, 0f);
			Value = 0;
			PlayerPrefs.SetInt(saveName, Value);
			return;
		}
		base.transform.localPosition = new Vector3(0f - vector.x, base.transform.localPosition.y, 0f);
		float num = Vector3.Distance(new Vector3(minXPos, 0f, 0f), new Vector3(maxXPos, 0f, 0f));
		float num2 = base.transform.position.x - minXPos;
		float num3 = Vector3.Distance(new Vector3(0f, 0f, 0f), new Vector3(maxValue, 0f, 0f));
		float num4 = Value;
		Value = (int)(num3 * (num2 / num));
		if (num4 != (float)Value)
		{
			PlayerPrefs.SetInt(saveName, Value);
		}
	}

	private void OnMouseDown()
	{
		move = true;
	}

	private void OnMouseUp()
	{
		move = false;
	}
}
