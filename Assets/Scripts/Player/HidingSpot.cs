using UnityEngine;
using UnityEngine.InputSystem;

public class HidingSpot : MonoBehaviour
{
    private StealthHiding playerHiding;
    private PlayerInteraction playerInteraction;
    private bool playerNearby;

    

    [Header("Mensajes Flotantes")]
    [SerializeField] private string mensajeAlEntrar = "¡Oculto!";
    [SerializeField] private string mensajeAlSalir = "¡Fuera!";

    [Header("Prefab de Texto Flotante 3D")]
    [SerializeField] private GameObject simpleTextPrefab; // Arrastra aquí tu prefab Text (TMP)

    [Header("Sistema de diálogo")]
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private string dialogoAlEntrar =
        "Debo mantenerme quieto...";

    [SerializeField] private string dialogoAlSalir =
        "Es hora de continuar.";
    


    [Header("Sonidos del escondite")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoEntrar;
    [SerializeField] private AudioClip sonidoSalir;

    private void Update()
    {
        if (!playerNearby || playerHiding == null)
            return;

        // Soporte para teclado (tecla E)
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    public void Interact()
    {
        Debug.Log("HidingSpot: Interact ejecutado");

        if (playerHiding == null)
        {
            Debug.LogWarning("No se encontró StealthHiding");
            return;
        }

        if (dialogueManager == null)
        {
            Debug.LogError("No hay DialogueManager asignado");
        }

        if (playerHiding.IsHidden)
        {
            playerHiding.Unhide();

            if (audioSource != null && sonidoSalir != null)
                audioSource.PlayOneShot(sonidoSalir);

            Debug.Log("Jugador salió del escondite");

            if (dialogueManager != null)
                dialogueManager.ShowDialogue(dialogoAlSalir);
        }
        else
        {
            playerHiding.Hide();

            if (audioSource != null && sonidoEntrar != null)
                audioSource.PlayOneShot(sonidoEntrar);

            Debug.Log("Jugador entró al escondite");

            if (dialogueManager != null)
                dialogueManager.ShowDialogue(dialogoAlEntrar);
        }
    }

    private void MostrarTexto(Vector3 posicion, string mensaje)
    {
        // Si el escondite no tiene asignado el prefab, intentamos buscarlo o avisar
        if (simpleTextPrefab != null)
        {
            GameObject textObj = Instantiate(simpleTextPrefab, posicion + Vector3.up * 2f, Quaternion.identity);
            SimpleFloatingText floating = textObj.GetComponent<SimpleFloatingText>();
            if (floating != null)
            {
                floating.SetMessage(mensaje);
            }
        }
        else
        {
            Debug.LogWarning("El HidingSpot no tiene asignado el simpleTextPrefab en el Inspector.", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StealthHiding hiding = other.GetComponent<StealthHiding>() ?? other.GetComponentInParent<StealthHiding>();

        if (hiding != null)
        {
            playerHiding = hiding;
            playerNearby = true;

            playerInteraction = hiding.GetComponent<PlayerInteraction>() ?? hiding.GetComponentInParent<PlayerInteraction>();

            if (playerInteraction != null)
            {
                playerInteraction.SetHidingSpot(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StealthHiding hiding = other.GetComponent<StealthHiding>() ?? other.GetComponentInParent<StealthHiding>();

        if (hiding != null && hiding == playerHiding)
        {
            if (playerHiding.IsHidden)
                return; // Si sigue escondido, no rompemos la referencia al salir del collider

            playerNearby = false;
            playerHiding = null;

            if (playerInteraction != null)
            {
                playerInteraction.SetHidingSpot(null);
            }
            playerInteraction = null;
        }
    }
}