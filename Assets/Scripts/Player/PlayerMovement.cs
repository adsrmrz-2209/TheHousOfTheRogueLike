using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private CharacterController controller;

    [SerializeField, ReadOnly] private Vector2 movementInput;
    
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private void Awake()
    {
        player = GetComponent<Player>();
        if (player == null)
            Application.Quit();

        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        player.OnMovementPerformed += OnMovementPerformed;
        player.OnMovementCanceled += OnMovementCancelled;

        player.OnJumpPerformed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        player.OnMovementPerformed -= OnMovementPerformed;
        player.OnMovementCanceled -= OnMovementCancelled;

        player.OnJumpPerformed -= OnJumpPerformed;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (player == null ||  controller == null)
            return;

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f || playerVelocity.y < 0)
                playerVelocity.y = -2f;
        }

        // Read input
        Vector2 input = movementInput;
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        move = Vector3.ClampMagnitude(move, 1f);

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Move
        Vector3 finalMove = move * player.Data.Speed + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }

    private void OnMovementPerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext ctx)
    {
        movementInput = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        // Jump using WasPressedThisFrame()
        if (groundedPlayer && ctx.action.WasPressedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(player.Data.JumpHeight * -2f * gravityValue);
        }
    }
}
