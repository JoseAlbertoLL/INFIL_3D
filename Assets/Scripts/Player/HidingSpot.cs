using UnityEngine;
using UnityEngine.InputSystem;

public class HidingSpot : MonoBehaviour
{
    private StealthHiding playerHiding;
    private PlayerInteraction playerInteraction;
    private bool playerNearby;

    private void Update()
    {
        if (!playerNearby || playerHiding == null)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    [SerializeField] private SkillCheckManager skillCheckManager;

    public void Interact()
    {
        if (playerHiding == null)
            return;

        if (playerHiding.IsHidden)
        {
            playerHiding.Unhide();
        }
        else
        {
            playerHiding.Hide();
            
            // ELIMINADO de aquí para evitar que se duplique, 
            // ya que StealthHiding ya se encarga de llamarlo dentro de Hide().
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

            playerInteraction =
                hiding.GetComponent<PlayerInteraction>();

            if (playerInteraction == null)
                playerInteraction =
                    hiding.GetComponentInParent<PlayerInteraction>();

            if (playerInteraction != null)
                playerInteraction.SetHidingSpot(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StealthHiding hiding =
            other.GetComponent<StealthHiding>();

        if (hiding == null)
            hiding = other.GetComponentInParent<StealthHiding>();

        if (hiding != null && hiding == playerHiding)
        {
            // Si está escondido, conservar la referencia.
            if (playerHiding.IsHidden)
                return;

            playerNearby = false;
            playerHiding = null;

            if (playerInteraction != null)
                playerInteraction.SetHidingSpot(null);

            playerInteraction = null;
        }
    }
}