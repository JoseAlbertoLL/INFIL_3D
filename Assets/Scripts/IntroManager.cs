using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private float duracionIntro = 3f;
    [SerializeField] private string escenaMenu = "MainMenu";

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(duracionIntro);

        SceneManager.LoadScene(escenaMenu);
    }
}