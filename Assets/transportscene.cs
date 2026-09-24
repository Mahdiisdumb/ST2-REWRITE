using UnityEngine;
using UnityEngine.SceneManagement;

public class transportscene : MonoBehaviour
{
	public int scene;
	public float time;

	void Update()
	{
		time -= Time.deltaTime;

		if (time <= 0)
		{
			SceneManager.LoadScene(scene);
		}
	}
}