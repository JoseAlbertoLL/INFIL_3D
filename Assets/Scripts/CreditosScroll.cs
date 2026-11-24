
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditosScroll : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float velocidad = 50f;
    [SerializeField] private float esperaAntesDelMenu = 3f;
    [SerializeField] private string nombreEscenaMenu = "MainMenu";

    private RectTransform rectTransform;
    private bool creditosTerminados;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (creditosTerminados)
            return;

        rectTransform.anchoredPosition +=
            Vector2.up * velocidad * Time.deltaTime;

        // Comprobar si los créditos ya salieron de la pantalla
        if (rectTransform.position.y >
            ((RectTransform)rectTransform.parent).rect.height +
            rectTransform.rect.height)
        {
            creditosTerminados = true;
            StartCoroutine(VolverAlMenu());
        }
    }

    private IEnumerator VolverAlMenu()
    {
        yield return new WaitForSeconds(esperaAntesDelMenu);

        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}
