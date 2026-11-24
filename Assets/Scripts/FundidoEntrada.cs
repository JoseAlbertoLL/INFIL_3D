
using UnityEngine;
using System.Collections;

public class FundidoEntrada : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duracion = 2f;

    private void Start()
    {
        canvasGroup.alpha = 1f;
        StartCoroutine(Fundir());
    }

    private IEnumerator Fundir()
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;

            canvasGroup.alpha = 1f - (tiempo / duracion);

            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
