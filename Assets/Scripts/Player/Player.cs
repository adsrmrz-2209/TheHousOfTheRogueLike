using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using System;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public PlayerData playerData {  get; private set; }

    [SerializeField, ReadOnly] private Vector2 movementInput;
    [SerializeField, ReadOnly] private Vector2 lookInput;
    public Vector2 MovementInput => movementInput;
    public Vector2 LookInput => lookInput;

    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private CharacterController controller;
    private PlayerControls playerControls;

    public event Action<InputAction.CallbackContext> OnMovementStarted;
    public event Action<InputAction.CallbackContext> OnMovementPerformed;
    public event Action<InputAction.CallbackContext> OnMovementCanceled;

    public event Action<InputAction.CallbackContext> OnLookStarted;
    public event Action<InputAction.CallbackContext> OnLookPerformed;
    public event Action<InputAction.CallbackContext> OnLookCanceled;

    public void CreatePlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    private void Awake()
    {
        playerControls = new();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        MovementInputSubscribe();
        LookInputSubscribe();

        playerControls.Enable();
    }

    private void OnDisable()
    {
        MovementInputUnSubscribe();
        LookInputUnSubscribe();

        playerControls.Disable();
    }

    private void OnDestroy()
    {
        playerControls.Dispose();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        // Read input
        Vector2 input = movementInput;
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
            transform.forward = move;

        // Jump using WasPressedThisFrame()
        //if (groundedPlayer && jumpAction.action.WasPressedThisFrame())
        //{
        //    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        //}

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Move
        Vector3 finalMove = move * playerData.Speed+ Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }

    #region MOVEMENT
    private void MovementInputSubscribe()
    {
        playerControls.Gameplay.Movement.started += MovementInputInvoke;
        playerControls.Gameplay.Movement.performed += MovementInputInvoke;
        playerControls.Gameplay.Movement.canceled += MovementInputInvoke;
    }

    private void MovementInputUnSubscribe()
    {
        playerControls.Gameplay.Movement.started -= MovementInputInvoke;
        playerControls.Gameplay.Movement.performed -= MovementInputInvoke;
        playerControls.Gameplay.Movement.canceled -= MovementInputInvoke;
    }

    private void MovementInputInvoke(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnMovementStarted?.Invoke(ctx);
        }
        else if (ctx.performed)
        {
            movementInput = ctx.ReadValue<Vector2>();
            OnMovementPerformed?.Invoke(ctx);
        }
        else if (ctx.canceled)
        {
            movementInput = Vector2.zero;
            OnMovementCanceled?.Invoke(ctx);
        }
    }
    #endregion

    #region POINTER

    private void LookInputSubscribe()
    {
        playerControls.Gameplay.Look.started += LookInputInvoke;
        playerControls.Gameplay.Look.performed += LookInputInvoke;
        playerControls.Gameplay.Look.canceled += LookInputInvoke;
    }

    private void LookInputUnSubscribe()
    {
        playerControls.Gameplay.Look.started -= LookInputInvoke;
        playerControls.Gameplay.Look.performed -= LookInputInvoke;
        playerControls.Gameplay.Look.canceled -= LookInputInvoke;
    }

    private void LookInputInvoke(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnLookStarted?.Invoke(ctx);
        }
        else if (ctx.performed)
        {
            lookInput = ctx.ReadValue<Vector2>();
            OnLookPerformed?.Invoke(ctx);
        }
        else if (ctx.canceled)
        {
            OnLookCanceled?.Invoke(ctx);
        }
    }
    #endregion
}
