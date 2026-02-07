using UnityEngine;
using UnityEngine.InputSystem; // Required for New Input System

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 playerVelocity;
    
    public float speed = 5.0f;
    public float gravityValue = -9.81f;
    public Vector3 respawnPoint;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        // Set the initial respawn point to where we start
        respawnPoint = transform.position;
    }

    // This matches the "New Input System" requirement
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        // Grounded check (Physics logic)
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Calculate movement direction based on Input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * Time.deltaTime * speed);

        // Apply Gravity (Simple Physics)
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Death Plane Logic (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DeathPlane")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Disable controller to teleport, then re-enable
        controller.enabled = false;
        transform.position = respawnPoint;
        controller.enabled = true;
    }
}