using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Player's ground check
    /// </summary>
    [Tooltip("Player's ground check")]
    public Transform groundCheck;
    /// <summary>
    /// Ground distance detection
    /// </summary>
    [Tooltip("round distance detection")]
    public float groundDistance = 0.4f;
    /// <summary>
    /// Ground Layer Mask
    /// </summary>
    [Tooltip("Ground Layer Mask")]
    public LayerMask groundMask;
    /// <summary>
    /// Player's sprint
    /// </summary>
    [Tooltip("Player's sprint")]
    public float sprint = 12f;
    /// <summary>
    /// Player's speed
    /// </summary>
    [Tooltip("Player's speed")]
    public float speed = 10f;
    /// <summary>
    /// Player's jump height
    /// </summary>
    [Tooltip("Player's jump height in units")]
    public float jumpHeight = 3f;
    /// <summary>
    /// Player's gravity
    /// </summary>
    [Tooltip("Player's gravity")]
    public float gravity = -9.81f;
    /// <summary>
    /// Player's push power to any rigidbody
    /// </summary>
    [Tooltip("Player's push power to any rigidbody")]
    float pushPower = 2.0f;
    /// <summary>
    /// CharacterController component of the player
    /// </summary>
    CharacterController controller;
    /// <summary>
    /// Movement axis
    /// </summary>
    float x, z;
    /// <summary>
    /// Player velocity
    /// </summary>
    Vector3 velocity;
    /// <summary>
    /// Boolean to know if the player is touching the ground
    /// </summary>
    bool isGrounded;
    /// <summary>
    /// Current player's speed
    /// </summary>
    float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = speed;
    }
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprint : speed;
		controller.Move(move * currentSpeed * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        Rigidbody body = hit.collider.attachedRigidbody;

        // We dont want to push objects below us or not having a rigidbody
        if (body == null || body.isKinematic || hit.moveDirection.y < -0.3) return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
