using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement Variables
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 playerVelocity;
    public float speed = 5.0f;
    public float gravityValue = -9.81f;
    public Vector3 respawnPoint;

    // Camera/Look Variables
    public GameObject playerCamera; 
    public float lookSensitivity = 0.1f;
    private Vector2 lookInput;
    private float xRotation = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        respawnPoint = transform.position;

        // This locks the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue value) { moveInput = value.Get<Vector2>(); }
    
    // NEW: Function to get mouse movement
    public void OnLook(InputValue value) { lookInput = value.Get<Vector2>(); }

    void Update()
    {
        // 1. MOVEMENT LOGIC
        if (controller.isGrounded && playerVelocity.y < 0) { playerVelocity.y = 0f; }
        
        // Move relative to where the player is facing
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * Time.deltaTime * speed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // 2. LOOK LOGIC (The Lab 3 Fix)
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        // Rotate Body Left/Right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate Camera Up/Down (X-axis)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevents back-flipping
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DeathPlane") { Respawn(); }
    }

    void Respawn()
    {
        controller.enabled = false;
        transform.position = respawnPoint;
        controller.enabled = true;
    }
}