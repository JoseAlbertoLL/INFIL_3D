
using UnityEngine;
using UnityEngine.InputSystem;

public class EmisorRuido : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float radioRuido = 10f;

    [Header("Visualización")]
    [SerializeField] private bool mostrarRadio = true;

    private float radioActual;

    private void Awake()
    {
        Debug.Log("DIAGNOSTICO: Awake funciona.");
    }

    private void Start()
    {
        Debug.Log("DIAGNOSTICO: Start funciona.");
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.pKey.wasPressedThisFrame)
        {
            Debug.Log("Tecla P detectada.");

            GenerarRuido(radioRuido);
        }
    }

    public void GenerarRuido(float radio)
    {
        radioActual = radio;

        Debug.Log("Ruido generado. Radio: " + radio);

        SistemaRuido.EmitirRuido(
            transform.position,
            radio
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (!mostrarRadio || radioActual <= 0f)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            radioActual
        );
    }
}
