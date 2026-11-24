
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalNivel : MonoBehaviour
{
    [SerializeField] private string nombreEscenaCreditos = "Creditos";

    private bool nivelTerminado;

    private void OnTriggerEnter(Collider other)
    {
        if (nivelTerminado)
            return;

        if (other.CompareTag("Player"))
        {
            nivelTerminado = true;

            Debug.Log("¡Nivel completado! Cargando créditos...");

            Time.timeScale = 1f;

            SceneManager.LoadScene(nombreEscenaCreditos);
        }
    }
}
