
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Objetos requeridos")]
    [SerializeField] private string requiredItem = "Llave";

    [SerializeField] private bool requiresDiamond = false;
    [SerializeField] private string requiredDiamond = "Diamante";

    [Header("Dialogo")]
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField]
    private string missingKeyMessage =
        "Necesito otra llave para abrir esta puerta.";

    [SerializeField]
    private string missingDiamondMessage =
        "No puedo irme sin recuperar el diamante.";

    [Header("Movimiento de la puerta")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;

    [Header("Colision de la puerta")]
    [SerializeField] private Collider doorCollider;

    private InteractionManager playerInteraction;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private bool isOpen = false;
    private bool isOpening = false;

    private void Start()
    {
        if (doorPivot == null)
            doorPivot = transform;

        closedRotation = doorPivot.localRotation;

        openRotation = closedRotation *
            Quaternion.Euler(0f, openAngle, 0f);
    }

    private void Update()
    {
        if (isOpening)
        {
            doorPivot.localRotation = Quaternion.Slerp(
                doorPivot.localRotation,
                openRotation,
                Time.deltaTime * openSpeed
            );

            if (Quaternion.Angle(
                doorPivot.localRotation,
                openRotation) < 0.5f)
            {
                doorPivot.localRotation = openRotation;
                isOpening = false;
                isOpen = true;

                Debug.Log("Puerta abierta permanentemente.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteraction =
                other.GetComponent<InteractionManager>();

            if (playerInteraction != null && !isOpen)
                playerInteraction.SetCurrentDoor(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerInteraction != null)
            {
                playerInteraction.ClearCurrentDoor(this);
                playerInteraction = null;
            }
        }
    }

    public void TryInteract()
    {
        if (isOpen || isOpening)
            return;

        InventoryManager inventory =
            Object.FindFirstObjectByType<InventoryManager>();

        if (inventory == null)
        {
            Debug.LogError("No se encontró InventoryManager.");
            return;
        }

        if (!inventory.HasItem(requiredItem))
        {
            MostrarMensaje(missingKeyMessage);
            return;
        }

        if (requiresDiamond &&
            !inventory.HasItem(requiredDiamond))
        {
            MostrarMensaje(missingDiamondMessage);
            return;
        }

        Debug.Log("¡Requisitos cumplidos! Abriendo puerta...");

        if (doorCollider != null)
            doorCollider.enabled = false;

        isOpening = true;
    }

    private void MostrarMensaje(string mensaje)
    {
        if (dialogueManager != null)
            dialogueManager.ShowDialogue(mensaje);
        else
            Debug.LogWarning(mensaje);
    }
}





