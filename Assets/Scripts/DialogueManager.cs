using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Elementos UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Configuración")]
    [SerializeField] private float dialogueDuration = 3f;

    private Coroutine currentDialogue;

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string message)
    {
        Debug.Log("DialogueManager recibió: " + message);

        if (dialoguePanel == null || dialogueText == null)
        {
            Debug.LogError("Falta asignar el panel o el texto");
            return;
        }

        if (currentDialogue != null)
            StopCoroutine(currentDialogue);

        currentDialogue = StartCoroutine(DisplayDialogue(message));
    }

    private IEnumerator DisplayDialogue(string message)
    {
        dialoguePanel.SetActive(true);

        dialogueText.text = message;

        yield return new WaitForSeconds(dialogueDuration);

        dialoguePanel.SetActive(false);

        currentDialogue = null;
    }
}