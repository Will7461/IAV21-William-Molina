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
    /// Player's speed
    /// </summary>
    [Tooltip("Player's speed")]
    public float speed = 12f;
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
    bool isGrounded;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
		controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
	}
}
