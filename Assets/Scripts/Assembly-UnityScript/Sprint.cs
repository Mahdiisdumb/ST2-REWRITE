using System;
using UnityEngine;

[Serializable]
public class Sprint : MonoBehaviour
{
	public float speed;

	public float walkspeed;

	public float runspeed;

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		CharacterMotor characterMotor = (CharacterMotor)gameObject.GetComponent(typeof(CharacterMotor));
		characterMotor.movement.maxForwardSpeed = speed;
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			speed = runspeed;
		}
		else
		{
			speed = walkspeed;
			if (Input.GetKeyDown(KeyCode.H))
			{
				Screen.lockCursor = !Screen.lockCursor;
				Cursor.visible = !Cursor.visible;
			}
		}
		if ((bool)GameObject.FindGameObjectWithTag("walkspeed"))
		{
			walkspeed = float.Parse(GameObject.FindGameObjectWithTag("walkspeed").name);
		}
		if ((bool)GameObject.FindGameObjectWithTag("runspeed"))
		{
			runspeed = float.Parse(GameObject.FindGameObjectWithTag("runspeed").name);
		}
	}

	public virtual void Main()
	{
	}
}
