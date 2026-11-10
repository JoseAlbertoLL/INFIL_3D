using UnityEngine;

public class StealthHiding : MonoBehaviour
{
    [Header("Modelo visual del jugador")]
    [SerializeField] private GameObject visualModel;

    [Header("Scripts que se desactivan al esconderse")]
    [SerializeField] private MonoBehaviour[] movementScripts;

    [Header("Animador del jugador")]
    [SerializeField] private Animator playerAnimator;

    [Header("Skillcheck")]
    [SerializeField] private SkillCheckManager skillCheckManager;

    [Header("Configuración de Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hideSound;   // Sonido al entrar al escondite
    [SerializeField] private AudioClip unhideSound; // Sonido al salir del escondite

    public bool IsHidden { get; private set; }

    public void Hide()
    {
        if (IsHidden) return;

        // Reproducir sonido de entrada
        if (audioSource != null && hideSound != null)
        {
            audioSource.PlayOneShot(hideSound);
        }

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

        // Activar el minijuego de skillcheck al completarse la ocultación
        if (skillCheckManager != null)
        {
            skillCheckManager.StartSkillCheck(transform);
        }
    }

    public void Unhide()
    {
        if (!IsHidden) return;

        // Reproducir sonido de salida
        if (audioSource != null && unhideSound != null)
        {
            audioSource.PlayOneShot(unhideSound);
        }

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