using UnityEngine;

public class SimplePlayer : MonoBehaviour
{
	[SerializeField]
	private float maxHP = 100f;

	private float HP = 100f;

	[SerializeField]
	private float damageBloodAmount = 3f;

	[SerializeField]
	private float maxBloodIndication = 0.5f;

	[SerializeField]
	private float recoverSpeed = 1f;

	private void Start()
	{
		HP = maxHP;
		HP = 20f;
	}

	private void Update()
	{
		if (HP > maxHP)
		{
			HP = maxHP;
		}
		HP -= HP * Time.deltaTime;
		BleedBehavior.minBloodAmount = maxBloodIndication * (maxHP - HP) / maxHP;
	}

	public void Damage(int amount)
	{
		BleedBehavior.BloodAmount += Mathf.Clamp01(damageBloodAmount * (float)amount / HP);
		HP -= amount;
		if (HP <= 0f)
		{
			HP = maxHP;
			Debug.Log("Player died, resetting HP");
		}
		BleedBehavior.minBloodAmount = maxBloodIndication * (maxHP - HP) / maxHP;
	}

	private void OnGUI()
	{
	}
}
