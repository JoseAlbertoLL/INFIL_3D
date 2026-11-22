
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

    [Header("Visual del cono")]
    [SerializeField] private Material materialCono;
    [SerializeField, Min(0f)] private float alturaCono = 0.03f;
    [SerializeField, Min(0f)] private float alturaRaycast = 1f;

    public Transform Jugador => jugador;

    private GameObject objetoCono;
    private Mesh mallaCono;

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

    private void Awake()
    {
        CrearConoVisual();
    }

    private void LateUpdate()
    {
        if (objetoCono == null)
            return;

        // Mantener el cono en su posición y orientarlo
        // hacia donde mira el guardia.
        objetoCono.transform.position = puntoVision != null
            ? puntoVision.position
            : transform.position + Vector3.up * alturaCono;

        objetoCono.transform.rotation = transform.rotation;

        // Actualizar el cono para comprobar los obstáculos
        // aunque el guardia se encuentre en movimiento.
        ActualizarMalla();
    }

    private void CrearConoVisual()
    {
        objetoCono = new GameObject("ConoVision");

        objetoCono.transform.SetParent(transform);
        objetoCono.transform.localPosition = Vector3.zero;
        objetoCono.transform.localRotation = Quaternion.identity;
        objetoCono.transform.localScale = Vector3.one;

        MeshFilter filtro = objetoCono.AddComponent<MeshFilter>();
        MeshRenderer renderizador = objetoCono.AddComponent<MeshRenderer>();

        renderizador.material = materialCono;
        renderizador.shadowCastingMode =
            UnityEngine.Rendering.ShadowCastingMode.Off;

        renderizador.receiveShadows = false;

        mallaCono = new Mesh();
        mallaCono.name = "MallaConoVision";

        filtro.mesh = mallaCono;

        ActualizarMalla();
    }

    private void ActualizarMalla()
    {
        if (mallaCono == null)
            return;

        mallaCono.Clear();

        int segmentos = 40;

        Vector3[] vertices = new Vector3[segmentos + 2];
        int[] triangulos = new int[segmentos * 3];

        vertices[0] = Vector3.zero;

        // Altura desde donde se comprueban las paredes.
        Vector3 origenRaycast =
            objetoCono.transform.position + Vector3.up * alturaRaycast;

        for (int i = 0; i <= segmentos; i++)
        {
            float porcentaje = (float)i / segmentos;

            float angulo = Mathf.Lerp(
                -anguloVision / 2f,
                anguloVision / 2f,
                porcentaje
            );

            float radianes = angulo * Mathf.Deg2Rad;

            Vector3 direccionLocal = new Vector3(
                Mathf.Sin(radianes),
                0f,
                Mathf.Cos(radianes)
            );

            // Convertir la dirección local a dirección mundial.
            Vector3 direccionMundo =
                objetoCono.transform.TransformDirection(direccionLocal);

            // Distancia máxima del cono.
            float distancia = distanciaVision;

            // Si encuentra una pared, reducir la distancia.
            if (Physics.Raycast(
                origenRaycast,
                direccionMundo,
                out RaycastHit impacto,
                distanciaVision,
                capasObstaculos,
                QueryTriggerInteraction.Ignore))
            {
                distancia = Mathf.Max(0f, impacto.distance - 0.05f);
            }

            // Crear el vértice hasta donde se permite ver.
            vertices[i + 1] = direccionLocal * distancia;
        }

        for (int i = 0; i < segmentos; i++)
        {
            triangulos[i * 3] = 0;
            triangulos[i * 3 + 1] = i + 1;
            triangulos[i * 3 + 2] = i + 2;
        }

        mallaCono.vertices = vertices;
        mallaCono.triangles = triangulos;

        mallaCono.RecalculateNormals();
        mallaCono.RecalculateBounds();
    }

    private void OnDestroy()
    {
        if (mallaCono != null)
            Destroy(mallaCono);
    }
}

