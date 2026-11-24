
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonVolverMenu : MonoBehaviour
{
    [SerializeField] private string nombreEscenaMenu = "MainMenu";

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}
