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

    [Header("Input Debug")]
    [SerializeField, ReadOnly] private Vector2 lookInput;
    [SerializeField, ReadOnly] private float xRotation;

    public Vector2 LookInput => lookInput;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        ProcessLook();
    }

    private void ProcessLook()
    {
        if (player == null || camera == null)
            return;

        lookInput = player.playerControls.Gameplay.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * xSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * xSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
