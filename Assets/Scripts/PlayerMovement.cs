
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

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Evitar mover un controlador inactivo
        if (!gameObject.activeInHierarchy || !controller.enabled)
            return;

        // Leer las teclas
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

        // Dirección del movimiento
        Vector3 movement =
            new Vector3(-input.x, 0f, -input.y).normalized;

        // Rotar hacia la dirección del movimiento
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

        // Mover al personaje
        controller.Move(
            movement * moveSpeed * Time.deltaTime
        );

        // Actualizar animación
        if (animator != null)
        {
            animator.SetFloat("Speed", input.magnitude);
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




