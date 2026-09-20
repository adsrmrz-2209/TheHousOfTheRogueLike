using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [ReadOnly] public Vector2 movementInput;
    [ReadOnly] public Vector2 pointerInput;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new();

        playerControls.Enable();
        playerControls.Gameplay.Movement.performed += MovementPerformed;
        playerControls.Gameplay.Pointer.performed += PointerPerformed;
    }

    private void OnDestroy()
    {
        playerControls.Gameplay.Movement.performed -= MovementPerformed;
        playerControls.Gameplay.Pointer.performed -= PointerPerformed;
        playerControls.Disable();
    }

    private void SubscribeToInputAction(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        inputAction.started += action;
        inputAction.performed += action;
        inputAction.canceled += action;
    }

    private void MovementPerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    private void PointerPerformed(InputAction.CallbackContext ctx)
    {
        pointerInput = ctx.ReadValue<Vector2>();
    }
}
