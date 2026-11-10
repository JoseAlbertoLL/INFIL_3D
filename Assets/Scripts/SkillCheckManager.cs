using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SkillCheckManager : MonoBehaviour, IPointerDownHandler
{
    [Header("Referencias de UI")]
    [SerializeField] private GameObject skillCheckPanel;
    [SerializeField] private RectTransform indicator;
    [SerializeField] private RectTransform targetZone;
    [SerializeField] private RectTransform backgroundBar;

    [Header("Configuración")]
    [SerializeField] private float indicatorSpeed = 350f; // Velocidad de movimiento
    [SerializeField] private float noiseRadiusOnFail = 8f; // Radio del ruido si falla
    [SerializeField] private float minWaitBetweenChecks = 2f; // Tiempo mínimo para el siguiente skillcheck
    [SerializeField] private float maxWaitBetweenChecks = 4f; // Tiempo máximo para el siguiente skillcheck

    private bool isRunning = false;
    private bool isHidingLoopActive = false;
    private float direction = 1f;
    private float minX, maxX;
    
    private Transform playerTransform;
    private StealthHiding currentStealthHiding; // Referencia al script del jugador

    void Awake()
    {
        if (skillCheckPanel != null)
            skillCheckPanel.SetActive(false);
    }
    
    void Start()
    {
        if (skillCheckPanel != null)
            skillCheckPanel.SetActive(false);

        if (backgroundBar != null)
        {
            float barWidth = backgroundBar.rect.width;
            minX = -barWidth / 2f;
            maxX = barWidth / 2f;
        }
    }

    void Update()
    {
        // Si el jugador sale del escondite mientras el bucle está activo, detenemos todo
        if (isHidingLoopActive && currentStealthHiding != null && !currentStealthHiding.IsHidden)
        {
            StopHidingLoop();
        }

        if (!isRunning) return;

        // Mover el indicador de izquierda a derecha
        float movement = indicatorSpeed * direction * Time.deltaTime;
        indicator.anchoredPosition += new Vector2(movement, 0f);

        // Invertir dirección o fallar automáticamente si llega al borde exterior
        if (indicator.anchoredPosition.x >= maxX)
        {
            indicator.anchoredPosition = new Vector2(maxX, indicator.anchoredPosition.y);
            direction = -1f;
        }
        else if (indicator.anchoredPosition.x <= minX)
        {
            indicator.anchoredPosition = new Vector2(minX, indicator.anchoredPosition.y);
            direction = 1f;
        }
    }

    // Método para iniciar el proceso cuando el jugador se esconde
    public void StartSkillCheck(Transform player)
    {
        playerTransform = player;
        currentStealthHiding = player.GetComponent<StealthHiding>();

        if (!isHidingLoopActive)
        {
            isHidingLoopActive = true;
            StartCoroutine(SkillCheckLoop());
        }
    }

    // Bucle que lanza skillchecks de forma repetitiva mientras esté escondido
    private IEnumerator SkillCheckLoop()
    {
        while (currentStealthHiding != null && currentStealthHiding.IsHidden)
        {
            // Esperar un tiempo aleatorio antes de lanzar el siguiente skillcheck
            float waitTime = Random.Range(minWaitBetweenChecks, maxWaitBetweenChecks);
            yield return new WaitForSeconds(waitTime);

            // Verificar de nuevo por si salió del escondite durante la espera
            if (!currentStealthHiding.IsHidden) break;

            // Iniciar un skillcheck individual
            TriggerSingleSkillCheck();

            // Esperar a que el jugador responda (mientras isRunning sea true)
            yield return new WaitWhile(() => isRunning);
        }

        isHidingLoopActive = false;
    }

    private void TriggerSingleSkillCheck()
    {
        isRunning = true;
        direction = 1f;

        if (indicator != null)
            indicator.anchoredPosition = new Vector2(minX, indicator.anchoredPosition.y);

        if (skillCheckPanel != null)
            skillCheckPanel.SetActive(true);

        RandomizeTargetZone();
    }

    private void RandomizeTargetZone()
    {
        if (targetZone != null && backgroundBar != null)
        {
            float barWidth = backgroundBar.rect.width;
            float zoneWidth = targetZone.rect.width;
            float randomX = Random.Range(-barWidth / 2f + zoneWidth, barWidth / 2f - zoneWidth);
            targetZone.anchoredPosition = new Vector2(randomX, targetZone.anchoredPosition.y);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isRunning) return;

        EvaluateSkillCheck();
    }

    private void EvaluateSkillCheck()
    {
        isRunning = false;

        if (skillCheckPanel != null)
            skillCheckPanel.SetActive(false);

        float indicatorX = indicator.anchoredPosition.x;
        float targetMin = targetZone.anchoredPosition.x - (targetZone.rect.width / 2f);
        float targetMax = targetZone.anchoredPosition.x + (targetZone.rect.width / 2f);

        bool success = indicatorX >= targetMin && indicatorX <= targetMax;

        if (success)
        {
            Debug.Log("¡Skillcheck EXITOSO! Sigues oculto en silencio.");
        }
        else
        {
            Debug.Log("¡Skillcheck FALLIDO! Haciendo ruido...");
            
            if (playerTransform != null)
            {
                SistemaRuido.EmitirRuido(playerTransform.position, noiseRadiusOnFail);
            }
        }
    }

    private void StopHidingLoop()
    {
        isHidingLoopActive = false;
        isRunning = false;
        StopAllCoroutines();

        if (skillCheckPanel != null)
            skillCheckPanel.SetActive(false);
    }
}