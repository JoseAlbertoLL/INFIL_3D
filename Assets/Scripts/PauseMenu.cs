using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject panelOpciones;

    [Header("Escenas")]
    [SerializeField] private string nombreMenuPrincipal = "MainMenu";

    private void Start()
    {
        Time.timeScale = 1f;

        if (panelPausa != null)
            panelPausa.SetActive(false);

        if (panelOpciones != null)
            panelOpciones.SetActive(false);
    }

    public void Pausar()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        panelPausa.SetActive(false);

        if (panelOpciones != null)
            panelOpciones.SetActive(false);

        Time.timeScale = 1f;
    }

    public void AbrirOpciones()
    {
        if (panelOpciones == null)
        {
            Debug.LogWarning("Todavía no has asignado el PanelOpciones.");
            return;
        }

        panelPausa.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void VolverAPausa()
    {
        if (panelOpciones != null)
            panelOpciones.SetActive(false);

        panelPausa.SetActive(true);
    }

    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreMenuPrincipal);
    }
}
