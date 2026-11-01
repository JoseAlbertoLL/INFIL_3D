using UnityEngine;
using UnityEngine.AI;

public class AnimacionGuardia : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private NavMeshAgent agente;
    [SerializeField] private Animator animator;

    [Header("Animaciones")]
    [SerializeField] private float velocidadMinima = 0.1f;

    private void Awake()
    {
        if (agente == null)
            agente = GetComponent<NavMeshAgent>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (agente == null || animator == null)
            return;

        float velocidad = agente.velocity.magnitude;

        if (agente.isStopped || !agente.hasPath)
            velocidad = 0f;

        if (velocidad < velocidadMinima)
            velocidad = 0f;

        animator.SetFloat("Velocidad", velocidad);
    }
}