/*
El guardia debe estar orientado hacia donde mira usando el "transform.forward"
Tienes que hacer un Layer para obstaculos (puertas, closets, paredes, etc. ) 

*/

using UnityEngine;

public class VisionGuardia : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform jugador;
    [SerializeField] private Transform puntoVision;

    [Header("Configuración de visión")]
    [SerializeField, Min(0.1f)] private float distanciaVision = 5f;
    [SerializeField, Range(1f, 180f)] private float anguloVision = 45f;

    [Header("Obstáculos")]
    [SerializeField] private LayerMask capasObstaculos;

    public Transform Jugador => jugador;

    public bool PuedeVerJugador()
    {
        if (jugador == null)
            return false;

        Vector3 origen = puntoVision != null
            ? puntoVision.position
            : transform.position + Vector3.up * 1.5f;

        Vector3 destino = jugador.position + Vector3.up;
        Vector3 direccion = destino - origen;

        if (direccion.magnitude > distanciaVision)
            return false;

        if (Vector3.Angle(transform.forward, direccion) > anguloVision / 2f)
            return false;

        // Si un objeto de las capas seleccionadas bloquea el rayo,
        // el guardia no puede ver al jugador.
        if (Physics.Raycast(
            origen,
            direccion.normalized,
            direccion.magnitude,
            capasObstaculos))
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origen = puntoVision != null
            ? puntoVision.position
            : transform.position + Vector3.up * 1.5f;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origen, distanciaVision);

        Vector3 izquierda = Quaternion.Euler(
            0, -anguloVision / 2f, 0) * transform.forward;

        Vector3 derecha = Quaternion.Euler(
            0, anguloVision / 2f, 0) * transform.forward;

        Gizmos.DrawRay(origen, izquierda * distanciaVision);
        Gizmos.DrawRay(origen, derecha * distanciaVision);
    }
}
