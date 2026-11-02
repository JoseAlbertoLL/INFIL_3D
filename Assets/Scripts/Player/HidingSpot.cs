
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingSpot : MonoBehaviour
{
    private StealthHiding playerHiding;
    private bool playerNearby;

    private void Update()
    {
        if (!playerNearby || playerHiding == null)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (playerHiding.IsHidden)
                playerHiding.Unhide();
            else
                playerHiding.Hide();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StealthHiding hiding =
            other.GetComponent<StealthHiding>();

        if (hiding == null)
            hiding = other.GetComponentInParent<StealthHiding>();

        if (hiding != null)
        {
            playerHiding = hiding;
            playerNearby = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        StealthHiding hiding = other.GetComponent<StealthHiding>();

        if (hiding == null)
            hiding = other.GetComponentInParent<StealthHiding>();

        if (hiding != null && hiding == playerHiding)
        {
            // Si está escondido, conservar la referencia
            if (playerHiding.IsHidden)
                return;

            playerNearby = false;
            playerHiding = null;
        }
    }

}

