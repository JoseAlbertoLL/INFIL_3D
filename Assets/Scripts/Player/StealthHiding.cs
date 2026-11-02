
using UnityEngine;

public class StealthHiding : MonoBehaviour
{
    [Header("Modelo visual del jugador")]
    [SerializeField] private GameObject visualModel;

    [Header("Scripts que se desactivan al esconderse")]
    [SerializeField] private MonoBehaviour[] movementScripts;

    [Header("Animador del jugador")]
    [SerializeField] private Animator playerAnimator;

    public bool IsHidden { get; private set; }

    public void Hide()
    {
        if (IsHidden) return;

        // Detener la animación
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Speed", 0f);
            playerAnimator.enabled = false;
        }

        // Ocultar el modelo visual
        if (visualModel != null)
            visualModel.SetActive(false);

        // Desactivar los scripts de movimiento
        foreach (MonoBehaviour script in movementScripts)
        {
            if (script != null)
                script.enabled = false;
        }

        IsHidden = true;
    }

    public void Unhide()
    {
        if (!IsHidden) return;

        // Mostrar nuevamente el modelo
        if (visualModel != null)
            visualModel.SetActive(true);

        // Reactivar el Animator
        if (playerAnimator != null)
            playerAnimator.enabled = true;

        // Reactivar los scripts de movimiento
        foreach (MonoBehaviour script in movementScripts)
        {
            if (script != null)
                script.enabled = true;
        }

        IsHidden = false;
    }
}





