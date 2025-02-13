using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerData data;
    [SerializeField] Transform view;
    [SerializeField] Animator animator;

    CharacterController characterController;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction sprintAction;
    InputAction punchAction;

    Vector2 movementInput = Vector2.zero;
    Vector3 velocity = Vector3.zero;

    bool isSprinting = false;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        jumpAction = InputSystem.actions.FindAction("Jump");
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJump;

        sprintAction = InputSystem.actions.FindAction("Sprint");
        sprintAction.performed += OnSprint;
        sprintAction.canceled += OnSprint;

        punchAction = InputSystem.actions.FindAction("Punch");
        punchAction.performed += OnPunch;
        punchAction.canceled += OnPunch;


        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Check if the player is on ground
		bool onGround = characterController.isGrounded; // || (velocity.y < 0 && PredictGroundContact())
 
		// Reset vertical velocity when grounded to prevent accumulating downward force
		if (onGround && velocity.y < 0)
		{
			velocity.y = -1; // Small downward force to keep player grounded
		}
 
		// Convert movement input into a world-space direction based on the player's view rotation
		Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
		movement = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up) * movement;
 
		// Initialize acceleration vector for movement calculations
		Vector3 acceleration = Vector3.zero;
		acceleration.x = movement.x * data.acceleration;
		acceleration.z = movement.z * data.acceleration;
 
		// Reduce acceleration while in the air for smoother movement control
		if (!onGround) acceleration *= 0.1f;
 
		// Extract horizontal velocity (ignoring vertical movement)
		Vector3 vXZ = new Vector3(velocity.x, 0, velocity.z);
 
		// Apply acceleration to velocity while limiting max speed
		vXZ += acceleration * Time.deltaTime;
		vXZ = Vector3.ClampMagnitude(vXZ, (isSprinting) ? data.sprintSpeed : data.speed);
 
		// Assign updated velocity values
		velocity.x = vXZ.x;
		velocity.z = vXZ.z;
 
		// Apply drag to slow the player down when there is no input or when airborne
		if (movement.sqrMagnitude <= 0 || !onGround)
		{
			float drag = (onGround) ? 10 : 4;
			velocity.x = Mathf.MoveTowards(velocity.x, 0, drag * Time.deltaTime);
			velocity.z = Mathf.MoveTowards(velocity.z, 0, drag * Time.deltaTime);
		}
 
		// Smoothly rotate the player towards the movement direction
		if (movement.sqrMagnitude > 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * data.turnRate);
		}
 
		// Apply gravity
		velocity.y += data.gravity * Time.deltaTime;
 
		// Move the character using the CharacterController component
		characterController.Move(velocity * Time.deltaTime);
 
		// Update animator parameters for movement animations
		animator.SetFloat("Speed", new Vector3(velocity.x, 0, velocity.z).magnitude);
		animator.SetFloat("AirSpeed", characterController.velocity.y);
		animator.SetBool("OnGround", onGround);
    }
    void OnMove(InputAction.CallbackContext callbackContext)
    {
        movementInput = callbackContext.ReadValue<Vector2>();
    }
    void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2*data.gravity*data.jumpHeight);
            animator.SetTrigger("Jump");
        }
    }

    void OnSprint(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed) isSprinting = true;
        else if (callbackContext.phase == InputActionPhase.Canceled) isSprinting = false;
    }

    void OnPunch(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed) animator.SetTrigger("Punch");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rb = hit.collider.attachedRigidbody;

        if(rb==null || rb.isKinematic || hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rb.linearVelocity = pushDir * data.pushForce;
    }
}
