using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerLook : MonoBehaviour
{
    private Player player;
    [SerializeField] private Camera camera;
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;
    [SerializeField, ReadOnly] private Vector2 lookInput;
    [SerializeField, ReadOnly] private float xRotation;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.OnLookPerformed += OnLookPerformed;
    }

    private void OnDisable()
    {
        player.OnLookPerformed -= OnLookPerformed;
    }

    private void LateUpdate()
    {
        ProcessLook();
    }

    private void ProcessLook()
    {
        float mouseX = lookInput.x;
        float mousey = lookInput.y;

        xRotation -= (mousey * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    private void OnLookPerformed(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }
}
