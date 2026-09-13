using UnityEngine;

public class Menu2DifficultyHorizontalSlider : MonoBehaviour
{
	public float maxXPos;

	public float minXPos;

	private int maxValue = 4;

	public int Value;

	private bool move;

	private bool changeValue;

	private void Start()
	{
		base.gameObject.AddComponent<BoxCollider>();
		Vector3 size = GetComponent<BoxCollider>().size;
		GetComponent<BoxCollider>().size = new Vector3(600f, size.y, size.z);
		if (!PlayerPrefs.HasKey("difficulty"))
		{
			PlayerPrefs.SetInt("difficulty", maxValue);
		}
		Value = PlayerPrefs.GetInt("difficulty");
		if (Value == 0)
		{
			PlayerPrefs.SetString("difficultyName", "Noob");
		}
		else if (Value == 1)
		{
			PlayerPrefs.SetString("difficultyName", "Easy");
		}
		else if (Value == 2)
		{
			PlayerPrefs.SetString("difficultyName", "Normal");
		}
		else if (Value == 3)
		{
			PlayerPrefs.SetString("difficultyName", "Hard");
		}
		else
		{
			PlayerPrefs.SetString("difficultyName", "Pro");
		}
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
			changeValue = true;
		}
		else if (0f - vector.x < minXPos)
		{
			base.transform.localPosition = new Vector3(minXPos, base.transform.localPosition.y, 0f);
			Value = 0;
			changeValue = true;
		}
		else
		{
			base.transform.localPosition = new Vector3(0f - vector.x, base.transform.localPosition.y, 0f);
			float num = Vector3.Distance(new Vector3(minXPos, 0f, 0f), new Vector3(maxXPos, 0f, 0f));
			float num2 = base.transform.position.x - minXPos;
			float num3 = Vector3.Distance(new Vector3(0f, 0f, 0f), new Vector3(maxValue, 0f, 0f));
			Value = (int)(num3 * (num2 / num));
			changeValue = true;
		}
		if (changeValue)
		{
			PlayerPrefs.SetInt("difficulty", Value);
			if (Value == 0)
			{
				PlayerPrefs.SetString("difficultyName", "Noob");
			}
			else if (Value == 1)
			{
				PlayerPrefs.SetString("difficultyName", "Easy");
			}
			else if (Value == 2)
			{
				PlayerPrefs.SetString("difficultyName", "Normal");
			}
			else if (Value == 3)
			{
				PlayerPrefs.SetString("difficultyName", "Hard");
			}
			else
			{
				PlayerPrefs.SetString("difficultyName", "Pro");
			}
			changeValue = false;
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
