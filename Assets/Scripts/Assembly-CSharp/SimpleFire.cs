using UnityEngine;

public class SimpleFire : MonoBehaviour
{
	[SerializeField]
	private int damage = 10;

	[SerializeField]
	private float damageDelay = 1f;

	private float lastDamageTime;

	private void OnTriggerStay(Collider other)
	{
		DoDamage(other);
	}

	private void DoDamage(Collider other)
	{
		if (Time.time > lastDamageTime + damageDelay)
		{
			other.SendMessageUpwards("Damage", damage);
			lastDamageTime = Time.time;
		}
	}
}
