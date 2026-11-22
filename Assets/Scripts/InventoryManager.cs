
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Configuración de Inventario")]
    [SerializeField] private int maxSlots = 3;

    [Header("Iconos de los slots")]
    [SerializeField] private Image[] slotIcons;

    private string[] itemNames;
    private int currentItemsCount = 0;

    private void Start()
    {
        itemNames = new string[maxSlots];

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

        Image currentSlot = slotIcons[currentItemsCount];

        if (currentSlot == null)
        {
            Debug.LogError("El slot " + currentItemsCount + " no está asignado.");
            return false;
        }

        currentSlot.sprite = itemIcon;
        currentSlot.enabled = true;

        itemNames[currentItemsCount] = itemName;

        Debug.Log("Nombre guardado en inventario: [" +
                  itemNames[currentItemsCount] + "]");

        currentItemsCount++;

        Debug.Log(
            $"¡Ítem añadido: {itemName}! " +
            $"Total: {currentItemsCount}/{maxSlots}"
        );

        return true;
    }


    public bool HasItem(string itemName)
    {
        Debug.Log("Buscando objeto: [" + itemName + "]");
        Debug.Log("Cantidad de objetos: " + currentItemsCount);

        for (int i = 0; i < currentItemsCount; i++)
        {
            Debug.Log("Slot " + i + " contiene: [" + itemNames[i] + "]");

            if (itemNames[i] == itemName)
            {
                Debug.Log("¡Objeto encontrado!");
                return true;
            }
        }

        Debug.Log("Objeto no encontrado en el inventario.");
        return false;
    }

}


