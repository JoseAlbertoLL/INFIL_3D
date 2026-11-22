
using UnityEngine;

public class LockInteraction : MonoBehaviour
{
    private InteractionManager playerInteraction;

    [Header("Minijuego")]
    [SerializeField] private LockPuzzleManager puzzleManager;

    [Header("Objeto requerido")]
    [SerializeField] private string requiredItem = "Ganzua";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteraction = other.GetComponent<InteractionManager>();

            if (playerInteraction != null)
            {
                playerInteraction.SetCurrentLock(this);

                Debug.Log("Cerradura cerca. ¡Pulsa interactuar!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerInteraction != null)
            {
                playerInteraction.ClearCurrentLock(this);
                playerInteraction = null;
            }
        }
    }

    public void TryInteract()
    {
        Debug.Log("Intentando interactuar con la cerradura...");

        InventoryManager inventory =
            Object.FindFirstObjectByType<InventoryManager>();

        if (inventory == null)
        {
            Debug.LogError("No se encontró InventoryManager.");
            return;
        }

        Debug.Log("Objeto requerido: [" + requiredItem + "]");

        bool hasItem = inventory.HasItem(requiredItem);

        Debug.Log("¿Tiene el objeto?: " + hasItem);

        if (!hasItem)
        {
            Debug.Log("Necesitas una ganzúa para abrir esta cerradura.");
            return;
        }

        Debug.Log("¡Ganzúa encontrada! Intentando abrir el puzzle...");

        if (puzzleManager != null)
        {
            puzzleManager.OpenPuzzle();
        }
        else
        {
            Debug.LogError("No se asignó LockPuzzleManager.");
        }
    }
}


