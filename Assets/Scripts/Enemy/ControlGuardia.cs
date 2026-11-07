
using UnityEngine;
using UnityEngine.AI;

public class ControlGuardia : MonoBehaviour
{
    private enum Estado
    {
        Patrullando,
        Persiguiendo,
        Investigando,
        Regresando
    }

    [Header("Componentes")]
    [SerializeField] private NavMeshAgent agente;
    [SerializeField] private VisionGuardia vision;
    [SerializeField] private PatrullaGuardia patrulla;

    [Header("Investigación")]
    [SerializeField, Min(0f)] private float tiempoInvestigacion = 2f;

    private Estado estado = Estado.Patrullando;

    private Vector3 ultimaPosicionJugador;
    private float tiempoBuscando;
    private Transform puntoRegreso;

    private void Awake()
    {
        if (agente == null)
            agente = GetComponent<NavMeshAgent>();

        if (vision == null)
            vision = GetComponent<VisionGuardia>();

        if (patrulla == null)
            patrulla = GetComponent<PatrullaGuardia>();
    }

    private void Start()
    {
        if (agente == null || vision == null || patrulla == null)
        {
            Debug.LogError(
                "Al guardia le falta NavMeshAgent, VisionGuardia " +
                "o PatrullaGuardia.", this);

            enabled = false;
            return;
        }

        if (!patrulla.TienePuntos)
        {
            Debug.LogError("El guardia no tiene puntos de patrulla.", this);
            enabled = false;
            return;
        }

        agente.isStopped = false;
        agente.SetDestination(patrulla.PuntoActual.position);
    }

    private void Update()
    {
        bool veJugador = vision.PuedeVerJugador();

        if (veJugador)
        {
            ultimaPosicionJugador = vision.Jugador.position;

            if (estado != Estado.Persiguiendo)
            {
                estado = Estado.Persiguiendo;
                Debug.Log("Guardia: jugador detectado.", this);
            }
        }
        else if (estado == Estado.Persiguiendo)
        {
            estado = Estado.Investigando;
            tiempoBuscando = 0f;

            Debug.Log("Guardia: perdió de vista al jugador.", this);
        }

        switch (estado)
        {
            case Estado.Patrullando:
                ActualizarPatrulla();
                break;

            case Estado.Persiguiendo:
                ActualizarPersecucion();
                break;

            case Estado.Investigando:
                ActualizarInvestigacion();
                break;

            case Estado.Regresando:
                ActualizarRegreso();
                break;
        }
    }

    private void ActualizarPatrulla()
    {
        Transform punto = patrulla.PuntoActual;

        if (punto == null)
            return;

        agente.SetDestination(punto.position);

        if (LlegoAlDestino())
        {
            patrulla.AvanzarAlSiguiente();

            Transform siguiente = patrulla.PuntoActual;

            if (siguiente != null)
                agente.SetDestination(siguiente.position);
        }
    }

    private void ActualizarPersecucion()
    {
        agente.SetDestination(ultimaPosicionJugador);
    }

    private void ActualizarInvestigacion()
    {
        agente.SetDestination(ultimaPosicionJugador);

        if (!LlegoAlDestino())
        {
            tiempoBuscando = 0f;
            return;
        }

        tiempoBuscando += Time.deltaTime;

        if (tiempoBuscando >= tiempoInvestigacion)
        {
            puntoRegreso = patrulla.PuntoMasCercano(transform.position);

            if (puntoRegreso == null)
                return;

            estado = Estado.Regresando;
            Debug.Log("Guardia: terminó la búsqueda. Regresando a patrullar.", this);
        }
    }

    private void ActualizarRegreso()
    {
        if (puntoRegreso == null)
        {
            estado = Estado.Patrullando;
            return;
        }

        agente.SetDestination(puntoRegreso.position);

        if (LlegoAlDestino())
        {
            patrulla.EstablecerPuntoActual(puntoRegreso);
            estado = Estado.Patrullando;

            Debug.Log("Guardia: retomando patrulla.", this);
        }
    }

    private bool LlegoAlDestino()
    {
        return !agente.pathPending &&
               agente.remainingDistance <=
               agente.stoppingDistance + 0.1f;
    }

    private void OnEnable()
{
    SistemaRuido.RuidoGenerado += EscucharRuido;
}


//SISTEMA DE RUIDO
private void OnDisable()
{
    SistemaRuido.RuidoGenerado -= EscucharRuido;
}

private void EscucharRuido(Vector3 posicion, float radio)
{
    // No abandonar una persecución activa.
    if (estado == Estado.Persiguiendo)
        return;

    // Si ya puede ver al jugador, no necesita investigar el ruido.
    if (vision != null && vision.PuedeVerJugador())
        return;

    float distancia = Vector3.Distance(
        transform.position,
        posicion
    );

    if (distancia > radio)
        return;

    // Comprobar que la posición del ruido esté cerca del NavMesh.
    if (!NavMesh.SamplePosition(
        posicion,
        out NavMeshHit hit,
        2f,
        agente.areaMask))
    {
        return;
    }

    ultimaPosicionJugador = hit.position;
    tiempoBuscando = 0f;
    puntoRegreso = null;

    estado = Estado.Investigando;

    agente.isStopped = false;
    agente.SetDestination(ultimaPosicionJugador);

    Debug.Log("Guardia: escuchó un ruido. Investigando.", this);
}
}
