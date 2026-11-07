
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private HidingSpot currentHidingSpot;

    public void SetHidingSpot(HidingSpot spot)
    {
        currentHidingSpot = spot;
    }

    public void Interact()
    {
        if (currentHidingSpot != null)
        {
            currentHidingSpot.Interact();
        }
    }
}

