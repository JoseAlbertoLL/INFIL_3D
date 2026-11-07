
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Rotación")]
    public float rotationSpeed = 12f;

    [Header("Sonido")]
    [SerializeField] private EmisorRuido emisorRuido;
    [SerializeField] private float radioCarrera = 4f;
    [SerializeField] private float intervaloPasos = 0.55f;

    private float tiempoProximoRuido;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        if (emisorRuido == null)
            emisorRuido = GetComponent<EmisorRuido>();
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy || !controller.enabled)
            return;

        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
                input.x = -1f;

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
                input.x = 1f;

            if (Keyboard.current.sKey.isPressed ||
                Keyboard.current.downArrowKey.isPressed)
                input.y = -1f;

            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
                input.y = 1f;
        }

        Vector3 movement =
            new Vector3(-input.x, 0f, -input.y).normalized;

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        controller.Move(
            movement * moveSpeed * Time.deltaTime
        );

        if (animator != null)
        {
            animator.SetFloat("Speed", input.magnitude);
        }

        // Generación periódica de ruido
        if (movement != Vector3.zero && controller.isGrounded)
        {
            tiempoProximoRuido -= Time.deltaTime;

            if (tiempoProximoRuido <= 0f)
            {
                if (emisorRuido != null)
                {
                    emisorRuido.GenerarRuido(radioCarrera);
                }

                tiempoProximoRuido = intervaloPasos;
            }
        }
        else
        {
            tiempoProximoRuido = 0f;
        }

        // Gravedad
        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
