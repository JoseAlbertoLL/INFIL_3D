
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Llave requerida")]
    [SerializeField] private string requiredItem = "LlaveMansion";

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
        {
            doorPivot = transform;
        }

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
            {
                playerInteraction.SetCurrentDoor(this);

                Debug.Log("Puerta cerca. ¡Pulsa interactuar!");
            }
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
            Debug.Log("Necesitas la llave de la mansión.");
            return;
        }

        Debug.Log("¡Llave encontrada! Abriendo puerta...");

        // Desactivar colisión inmediatamente
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        isOpening = true;
    }
}



