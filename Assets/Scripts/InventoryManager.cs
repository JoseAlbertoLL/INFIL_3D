using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Configuración de Inventario")]
    [SerializeField] private int maxSlots = 3;
    private int currentItemsCount = 0;

    public bool AddItem(string itemName)
    {
        if (currentItemsCount < maxSlots)
        {
            currentItemsCount++;
            Debug.Log($"¡Ítem añadido: {itemName}! Total en inventario: {currentItemsCount}/{maxSlots}");
            return true;
        }
        else
        {
            Debug.Log("¡Inventario lleno!");
            return false;
        }
    }
}