
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject panelPausa;

    [Header("Dialogos")]
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private string nombreMenuPrincipal = "MainMenu";

    private void Start()
    {
        Time.timeScale = 1f;
        panelGameOver.SetActive(false);
    }

    public void MostrarGameOver()
    {
        // Ocultar cualquier diálogo activo
        if (dialogueManager != null)
            dialogueManager.HideDialogue();

        panelGameOver.SetActive(true);

        if (panelPausa != null)
            panelPausa.SetActive(false);

        Time.timeScale = 0f;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreMenuPrincipal);
    }
}


