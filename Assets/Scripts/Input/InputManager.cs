using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField, ReadOnly] private Vector2 movementInput;
    [SerializeField, ReadOnly] private Vector2 lookInput;
    private PlayerControls playerControls;

    public Vector2 MovementInput => movementInput;
    public Vector2 LookInput => lookInput;

    public /*static*/ event Action<InputAction.CallbackContext> OnMovementStarted;
    public /*static*/ event Action<InputAction.CallbackContext> OnMovementPerformed;
    public /*static*/ event Action<InputAction.CallbackContext> OnMovementCanceled;

    public /*static*/ event Action<InputAction.CallbackContext> OnLookStarted;
    public /*static*/ event Action<InputAction.CallbackContext> OnLookPerformed;
    public /*static*/ event Action<InputAction.CallbackContext> OnLookCanceled;

    private void Awake()
    {
        playerControls = new();
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
