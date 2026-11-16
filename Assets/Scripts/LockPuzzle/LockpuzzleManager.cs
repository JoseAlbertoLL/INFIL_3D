
using UnityEngine;
using TMPro;

public class LockPuzzleManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject puzzlePanel;

    [Header("Pines")]
    [SerializeField] private LockPin[] pins;
    [SerializeField] private RectTransform precisionZone;

    [Header("Interfaz")]
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text attemptsText;

    private int currentPin = 0;
    private int remainingAttempts = 3;

    public bool IsPuzzleActive { get; private set; }

    public void OpenPuzzle()
    {
        if (pins == null || pins.Length == 0)
        {
            Debug.LogError("No se han asignado los pines del minijuego.");
            return;
        }

        foreach (LockPin pin in pins)
        {
            if (pin == null)
            {
                Debug.LogError("Hay un pin sin asignar en LockPuzzleManager.");
                return;
            }
        }

        puzzlePanel.SetActive(true);
        IsPuzzleActive = true;

        Time.timeScale = 0f;

        currentPin = 0;
        remainingAttempts = 3;

        foreach (LockPin pin in pins)
        {
            pin.ResetPin();
            pin.SetMoving(false);
        }

        pins[currentPin].SetMoving(true);

        UpdateUI();
    }

    public void TryPick()
    {
        if (!IsPuzzleActive)
            return;

        if (pins[currentPin].IsInsideZone(precisionZone))
        {
            Debug.Log("¡Pin desbloqueado!");

            pins[currentPin].SetMoving(false);
            pins[currentPin].SetLocked();

            currentPin++;

            if (currentPin >= pins.Length)
            {
                Debug.Log("¡Cerradura forzada correctamente!");
                ClosePuzzle();
                return;
            }

            pins[currentPin].SetMoving(true);
        }
        else
        {
            remainingAttempts--;

            Debug.Log("Fallaste. Intentos restantes: " + remainingAttempts);

            if (remainingAttempts <= 0)
            {
                Debug.Log("Se agotaron los intentos.");
                ClosePuzzle();
                return;
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        progressText.text = "Pines desbloqueados: " +
            currentPin + "/" + pins.Length;

        attemptsText.text = "Errores restantes: " +
            remainingAttempts;
    }

    public void ClosePuzzle()
    {
        puzzlePanel.SetActive(false);
        IsPuzzleActive = false;

        Time.timeScale = 1f;
    }
}
