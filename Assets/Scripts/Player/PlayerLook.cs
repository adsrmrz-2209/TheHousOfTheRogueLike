using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerLook : MonoBehaviour
{
    private Player player;
    [SerializeField] private Camera camera;

    [Header("Sensitivity")]
    [SerializeField] private float xSensitivity = 12f;
    [SerializeField] private float ySensitivity = 12f;

    [Header("Dead Zone")]
    [SerializeField, Range(0f, 1f)] private float freeLookZone = 0.2f;
    private Vector2 lookOffset;


    [Header("Input Debug")]
    [SerializeField, ReadOnly] private Vector2 lookInput;
    [SerializeField, ReadOnly] private float xRotation;

    public Vector2 LookInput => lookInput;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        if (player == null)
            return;

        player.OnLookPerformed += OnLookPerformed;
        player.OnLookCanceled += OnLookCanceled;
    }

    private void OnDisable()
    {
        if (player == null)
            return;

        player.OnLookPerformed -= OnLookPerformed;
        player.OnLookCanceled -= OnLookCanceled;
    }

    private void Update()
    {
        ProcessLook();
    }

    private void ProcessLook()
    {
        if (player == null || camera == null)
            return;

        lookOffset += lookInput;

        lookOffset.x = Mathf.Clamp(lookOffset.x, -1f, 1f);
        lookOffset.y = Mathf.Clamp(lookOffset.y, -1f, 1f);

        if (IsInsideFreeLookZone())
            return;

        float mouseX = lookInput.x * xSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * xSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private bool IsInsideFreeLookZone()
    {
        return Mathf.Abs(lookOffset.x) <= freeLookZone &&
           Mathf.Abs(lookOffset.y) <= freeLookZone;
    }

    private void OnLookPerformed(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext ctx)
    {
        lookInput = Vector2.zero;
    }
}
