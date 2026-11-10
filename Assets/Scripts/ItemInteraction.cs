using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [Header("Datos del Ítem")]
    [SerializeField] private string itemName;

    private InteractionManager playerInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInteraction = other.GetComponent<InteractionManager>();
            if (playerInteraction != null)
            {
                playerInteraction.SetCurrentItem(this);
                Debug.Log($"Cerca de: {itemName}. ¡Pulsa el botón de interactuar!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerInteraction != null)
            {
                playerInteraction.ClearCurrentItem(this);
                playerInteraction = null;
            }
        }
    }

    public void TryInteract()
    {
        InventoryManager playerInventory = Object.FindFirstObjectByType<InventoryManager>();
        
        if (playerInventory != null)
        {
            bool added = playerInventory.AddItem(itemName);

            if (added)
            {
                Debug.Log($"¡Has recogido: {itemName}!");
                Destroy(gameObject); // El cubo desaparece del suelo al interactuar
            }
        }
        else
        {
            Debug.LogError("No se encontró ningún InventoryManager en la escena.");
        }
    }
}