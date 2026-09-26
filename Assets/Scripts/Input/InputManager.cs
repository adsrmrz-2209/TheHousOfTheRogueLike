using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField, ReadOnly] private Vector2 movementInput;
    [SerializeField, ReadOnly] private Vector2 pointerInput;
    private PlayerControls playerControls;

    public Vector2 MovementInput => movementInput;
    public Vector2 PointerInput => pointerInput;

    public /*static*/ event Action<InputAction.CallbackContext> OnMovementStarted;
    public /*static*/ event Action<InputAction.CallbackContext> OnMovementPerformed;
    public /*static*/ event Action<InputAction.CallbackContext> OnMovementCanceled;

    public /*static*/ event Action<InputAction.CallbackContext> OnPointerStarted;
    public /*static*/ event Action<InputAction.CallbackContext> OnPointerPerformed;
    public /*static*/ event Action<InputAction.CallbackContext> OnPointerCanceled;

    private void Awake()
    {
        playerControls = new();
        
        MovementInputSubscribe();
        PointerInputSubscribe();

        playerControls.Enable();
    }

    private void OnDestroy()
    {
        MovementInputUnSubscribe();
        PointerInputUnSubscribe();

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

    private void PointerInputSubscribe()
    {
        playerControls.Gameplay.Pointer.started += PointerInputInvoke;
        playerControls.Gameplay.Pointer.performed += PointerInputInvoke;
        playerControls.Gameplay.Pointer.canceled += PointerInputInvoke;
    }

    private void PointerInputUnSubscribe()
    {
        playerControls.Gameplay.Pointer.started -= PointerInputInvoke;
        playerControls.Gameplay.Pointer.performed -= PointerInputInvoke;
        playerControls.Gameplay.Pointer.canceled -= PointerInputInvoke;
    }

    private void PointerInputInvoke(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnPointerStarted?.Invoke(ctx);
        }
        else if (ctx.performed)
        {
            pointerInput = ctx.ReadValue<Vector2>();
            OnPointerPerformed?.Invoke(ctx);
        }
        else if (ctx.canceled)
        {
            OnPointerCanceled?.Invoke(ctx);
        }
    }
    #endregion

}
