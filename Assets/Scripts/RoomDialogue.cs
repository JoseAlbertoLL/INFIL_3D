
using UnityEngine;

public class RoomDialogue : MonoBehaviour
{
    [Header("Dialogo")]
    [SerializeField] private DialogueManager dialogueManager;

    [TextArea(2, 4)]
    [SerializeField]
    private string message =
        "Detrás de esta puerta debe estar el diamante...";

    private bool dialogueShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialogueShown)
        {
            dialogueShown = true;

            if (dialogueManager != null)
            {
                dialogueManager.ShowDialogue(message);
            }
            else
            {
                Debug.LogWarning("No se asignó DialogueManager.");
            }
        }
    }
}

