
using UnityEngine;

public class LockInteraction : MonoBehaviour
{
    private InteractionManager playerInteraction;

    [Header("Minijuego")]
    [SerializeField] private LockPuzzleManager puzzleManager;

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
