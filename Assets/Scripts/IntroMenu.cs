using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    [SerializeField] private string nombreSiguienteNivel;

    // Método que se ejecutará al hacer clic en el botón
    public void IrAlSiguienteNivel()
    {
        if (!string.IsNullOrEmpty(nombreSiguienteNivel))
        {
            SceneManager.LoadScene(nombreSiguienteNivel);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena no está configurado en el inspector.");
        }
    }
}