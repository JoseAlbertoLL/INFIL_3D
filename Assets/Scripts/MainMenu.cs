
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("MansionLevel");
    }

    public void Salir()
    {
        Application.Quit();
    }
        
}

