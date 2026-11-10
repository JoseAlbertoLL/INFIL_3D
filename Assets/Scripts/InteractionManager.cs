using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    // Referencia al objeto interactuable que está actualmente cerca del jugador
    private ItemInteraction currentItem;

    // Métodos para que los objetos se registren cuando el jugador se acerque
    public void SetCurrentItem(ItemInteraction item)
    {
        currentItem = item;
    }

    public void ClearCurrentItem(ItemInteraction item)
    {
        if (currentItem == item)
        {
            currentItem = null;
        }
    }

    // Este es el método que llamará tu botón táctil de interacción en pantalla
    public void TriggerInteraction()
    {
        if (currentItem != null)
        {
            currentItem.TryInteract();
        }
        else
        {
            Debug.Log("No hay ningún objeto interactuable cerca.");
        }
    }
}