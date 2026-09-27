using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using System;

public class Player : MonoBehaviour
{
    public PlayerData Data {  get; private set; }

    private PlayerControls playerControls;

    public event Action<InputAction.CallbackContext> OnMovementStarted;
    public event Action<InputAction.CallbackContext> OnMovementPerformed;
    public event Action<InputAction.CallbackContext> OnMovementCanceled;

    public event Action<InputAction.CallbackContext> OnJumpStarted;
    public event Action<InputAction.CallbackContext> OnJumpPerformed;
    public event Action<InputAction.CallbackContext> OnJumpCanceled;

    public event Action<InputAction.CallbackContext> OnLookStarted;
    public event Action<InputAction.CallbackContext> OnLookPerformed;
    public event Action<InputAction.CallbackContext> OnLookCanceled;

    public void InitData(PlayerData playerData)
    {
        this.Data = playerData;
    }

    private void Awake()
    {
        if (Data == null)
        {
            Data = new PlayerData(this);
        }

        playerControls = new();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Movement.started += MovementInputInvoke;
        playerControls.Gameplay.Movement.performed += MovementInputInvoke;
        playerControls.Gameplay.Movement.canceled += MovementInputInvoke;

        playerControls.Gameplay.Jump.started += JumpInputInvoke;
        playerControls.Gameplay.Jump.performed += JumpInputInvoke;
        playerControls.Gameplay.Jump.canceled += JumpInputInvoke;

        playerControls.Gameplay.Look.started += LookInputInvoke;
        playerControls.Gameplay.Look.performed += LookInputInvoke;
        playerControls.Gameplay.Look.canceled += LookInputInvoke;

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Movement.started -= MovementInputInvoke;
        playerControls.Gameplay.Movement.performed -= MovementInputInvoke;
        playerControls.Gameplay.Movement.canceled -= MovementInputInvoke;

        playerControls.Gameplay.Jump.started -= JumpInputInvoke;
        playerControls.Gameplay.Jump.performed -= JumpInputInvoke;
        playerControls.Gameplay.Jump.canceled -= JumpInputInvoke;

        playerControls.Gameplay.Look.started -= LookInputInvoke;
        playerControls.Gameplay.Look.performed -= LookInputInvoke;
        playerControls.Gameplay.Look.canceled -= LookInputInvoke;

        playerControls.Disable();
    }

    private void OnDestroy()
    {
        playerControls.Dispose();

        Data.Player = null;
    }

    #region MOVEMENT
    private void MovementInputInvoke(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnMovementStarted?.Invoke(ctx);
        }
        else if (ctx.performed)
        {
            OnMovementPerformed?.Invoke(ctx);
        }
        else if (ctx.canceled)
        {
            OnMovementCanceled?.Invoke(ctx);
        }
    }

    private void JumpInputInvoke(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnJumpStarted?.Invoke(ctx);
        }
        else if (ctx.performed)
        {
            OnJumpPerformed?.Invoke(ctx);
        }
        else if (ctx.canceled)
        {
            OnJumpCanceled?.Invoke(ctx);
        }
    }

    #endregion

    #region POINTER
    private void LookInputInvoke(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnLookStarted?.Invoke(ctx);
        }
        else if (ctx.performed)
        {
            OnLookPerformed?.Invoke(ctx);
        }
        else if (ctx.canceled)
        {
            OnLookCanceled?.Invoke(ctx);
        }
    }
    #endregion
}
