
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Configuración de Inventario")]
    [SerializeField] private int maxSlots = 3;

    [Header("Iconos de los slots")]
    [SerializeField] private Image[] slotIcons;

    private int currentItemsCount = 0;

    private void Start()
    {
        // Ocultar todos los iconos al iniciar
        foreach (Image icon in slotIcons)
        {
            if (icon != null)
                icon.enabled = false;
        }
    }

    public bool AddItem(string itemName, Sprite itemIcon)
    {
        if (currentItemsCount >= maxSlots)
        {
            Debug.Log("¡Inventario lleno!");
            return false;
        }

        if (itemIcon == null)
        {
            Debug.LogError("El objeto " + itemName + " no tiene icono asignado.");
            return false;
        }

        if (currentItemsCount >= slotIcons.Length)
        {
            Debug.LogError("No hay suficientes referencias de slots.");
            return false;
        }

        // Obtener el primer slot disponible
        Image currentSlot = slotIcons[currentItemsCount];

        if (currentSlot == null)
        {
            Debug.LogError("El slot " + currentItemsCount + " no está asignado.");
            return false;
        }

        // Colocar el icono
        currentSlot.sprite = itemIcon;
        currentSlot.enabled = true;

        currentItemsCount++;

        Debug.Log(
            $"¡Ítem añadido: {itemName}! " +
            $"Total: {currentItemsCount}/{maxSlots}"
        );

        return true;
    }
}
