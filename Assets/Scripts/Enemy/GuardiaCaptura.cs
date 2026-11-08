
using UnityEngine;

public class GuardiaCaptura : MonoBehaviour
{
    private bool jugadorCapturado = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("El guardia detectó: " + other.name);

        if (jugadorCapturado)
            return;

        if (other.CompareTag("Player"))
        {
            jugadorCapturado = true;

            Debug.Log("¡Jugador capturado!");

            GameOverManager gameOver =
                FindFirstObjectByType<GameOverManager>();

            if (gameOver != null)
                gameOver.MostrarGameOver();
            else
                Debug.LogError("No se encontró GameOverManager.");
        }
    }
}


