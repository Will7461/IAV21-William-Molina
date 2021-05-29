using Bolt;
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
    /// Player's camera
    /// </summary>
    [Tooltip("Player's camera")]
    public GameObject cam;
    /// <summary>
    /// Player's push power to any rigidbody
    /// </summary>
    float pushPower = 8.0f;
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
    /// <summary>
    /// Current player's push power to any rigidbody
    /// </summary>
    float currentPushPower;
    /// <summary>
    /// Raycast distance
    /// </summary>
    float raycastDistance = 7;
    /// <summary>
    /// Current item selected
    /// </summary>
    Transform selection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = speed;
    }
    /// <summary>
    /// Input and player movement
    /// </summary>
    void Update()
    {
        //Checks if player is on floor
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -10f;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //Detection of SHIFT key for sprinting
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprint : speed;
		controller.Move(move * currentSpeed * Time.deltaTime);

        // We calculate the percentage of pushing objects, based on the velocity of the player
        currentPushPower = pushPower * (controller.velocity.magnitude/sprint);

        //Jump input, we modify the Y value of the velocity on SPACE key pressed
        if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        //Gravity factor calculation
        velocity.y += gravity * Time.deltaTime;
        //Apply gravity factor to the player
        controller.Move(velocity * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.E) && selection != null)
		{
            Debug.Log(selection.name);
			if (selection.GetComponent<Food>())
			{
                int freeIndex = GameManager.Instance.freeInventorySlot();
                if (freeIndex != -1)
                {
                    GameManager.Instance.addToInventory(freeIndex, selection.name);
                    Destroy(selection.gameObject);
                    GameManager.Instance.setEKeyUIActionable(false);
                }
            }
            else if (selection.name == "Body")
			{
                GameObject animal = selection.parent.gameObject;
                if((string)Variables.Object(animal).Get("State") == "Fighting")
				{
                    Variables.Object(animal).Set("State", "");
				}
                else if ((string)Variables.Object(animal).Get("State") == "Starving" && (bool)Variables.Object(animal).Get("Fighting"))
				{
                    Variables.Object(animal).Set("State", "");
                }
			}
            
		}
        string itemDropped;
		if (Input.GetKeyDown(KeyCode.Q) && GameManager.Instance.canDropItem(out itemDropped))
		{
            GameObject gO = GameManager.Instance.InstantiateFood(itemDropped);
            gO.transform.position = cam.transform.position + cam.transform.forward;
            gO.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 5, ForceMode.Impulse);
        }
    }

	private void FixedUpdate()
	{
		if (selection != null)
		{
            GameManager.Instance.setEKeyUIActionable(false);
            selection.GetComponent<Outline>().setOutline(0);
            selection = null;
		}

        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position , cam.transform.TransformDirection(Vector3.forward), out hit, raycastDistance, 1 << LayerMask.NameToLayer("Item"), QueryTriggerInteraction.Collide))
		{
            Outline outLine = hit.transform.GetComponent<Outline>();
            if (outLine != null)
			{
                GameManager.Instance.setEKeyUIActionable(true);
                outLine.setOutline(10);
                selection = hit.transform;
			}
        }
        else if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, raycastDistance, 1 << LayerMask.NameToLayer("Animal"), QueryTriggerInteraction.Collide))
		{
            Outline outLine = hit.transform.GetChild(0).GetComponent<Outline>();
            if (outLine != null)
            {
                GameManager.Instance.setEKeyUIActionable(true);
                outLine.setOutline(10);
                selection = hit.transform.GetChild(0);
            }
        }
		else
		{
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * raycastDistance, Color.white);
        }

	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        Rigidbody body = hit.collider.attachedRigidbody;

        // We dont want to push objects below us or not having a rigidbody
        if (body == null || body.isKinematic || hit.moveDirection.y < -0.3) return;

        // Calculate push direction from move direction,
        // we only push objects to the sides
        Vector3 pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);

        // Apply the push
        body.velocity = pushDir * currentPushPower;
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Health>())
		{
            other.GetComponent<Health>().showHealthBar();
        }
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.GetComponent<Health>())
        {
            other.GetComponent<Health>().hideHealthBar();
        }
    }
}
