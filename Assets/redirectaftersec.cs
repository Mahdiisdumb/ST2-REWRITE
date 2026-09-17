using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class redirectaftersec : MonoBehaviour
{
    public float seconds = 5f;
    public int SceneInt = 1;

    void Awake()
    {
        if (seconds > 0f)
        {
            StartCoroutine(RedirectAfterSeconds());
        }
    }

    IEnumerator RedirectAfterSeconds()
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneInt);
    }
}