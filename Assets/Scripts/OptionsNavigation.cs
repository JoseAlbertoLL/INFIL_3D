
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsNavigation : MonoBehaviour
{
    private const string ReturnSceneKey = "OptionsReturnScene";

    public void OpenOptions()
    {
        StartCoroutine(OpenOptionsRoutine());
    }

    private IEnumerator OpenOptionsRoutine()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetString(ReturnSceneKey, currentScene);
        PlayerPrefs.Save();

        if (SceneManager.GetSceneByName("Opciones").isLoaded)
            yield break;

        yield return SceneManager.LoadSceneAsync(
            "Opciones",
            LoadSceneMode.Additive
        );

        SceneManager.SetActiveScene(
            SceneManager.GetSceneByName("Opciones")
        );
    }

    public void ReturnFromOptions()
    {
        string returnScene = PlayerPrefs.GetString(
            ReturnSceneKey,
            "MainMenu"
        );

        if (returnScene != "MansionLevel")
            returnScene = "MainMenu";

        SceneManager.SetActiveScene(
            SceneManager.GetSceneByName(returnScene)
        );

        SceneManager.UnloadSceneAsync("Opciones");
    }

    private IEnumerator ReturnRoutine()
    {
        string returnScene = PlayerPrefs.GetString(
            ReturnSceneKey,
            "MainMenu"
        );

        if (returnScene != "MansionLevel")
            returnScene = "MainMenu";

        yield return SceneManager.UnloadSceneAsync("Opciones");

        SceneManager.SetActiveScene(
            SceneManager.GetSceneByName(returnScene)
        );
    }
}


