
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void Salir()
    {
        Application.Quit();
    }
        
}

