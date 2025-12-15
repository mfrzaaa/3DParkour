using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensX = 15f; // Sensitivitas Mouse X
    [SerializeField] private float sensY = 15f; // Sensitivitas Mouse Y

    [SerializeField] private Transform orientation; // Referensi tubuh player utk diputar
    [SerializeField] private Transform cameraHolder; // Posisi kamera

    private PlayerControls inputActions;
    private float xRotation;
    private float yRotation;
    private Vector2 lookInput;

    private void Awake()
    {
        inputActions = new PlayerControls();
        
        // Mengunci cursor mouse ke tengah layar dan menghilangkannya
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable() => inputActions.Gameplay.Enable();
    private void OnDisable() => inputActions.Gameplay.Disable();

    private void Update()
    {
        // Baca input mouse delta atau Right Stick gamepad
        lookInput = inputActions.Gameplay.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * sensX * Time.deltaTime;
        float mouseY = lookInput.y * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamp agar tidak bisa melihat ke belakang sampai leher patah (batas 90 derajat)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 1. Putar Kamera (Atas Bawah)
        cameraHolder.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        
        // 2. Putar Orientation (Kiri Kanan) - Ini mempengaruhi arah lari "Maju"
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}