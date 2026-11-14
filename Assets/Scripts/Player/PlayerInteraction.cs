using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private HidingSpot currentHidingSpot;

    public void SetHidingSpot(HidingSpot spot)
    {
        currentHidingSpot = spot;
    }

    // Este método se vincula a tu botón táctil de pantalla en el UI
    public void Interact()
    {
        if (currentHidingSpot != null)
        {
            currentHidingSpot.Interact();
        }
        else
        {
            Debug.Log("No hay ningún escondite cerca.");
        }
    }
}