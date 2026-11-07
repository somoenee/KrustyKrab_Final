
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 4f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Read keyboard input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Calculate movement relative to the camera
        Vector3 moveDirection = Vector3.zero;

        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            moveDirection = (forward * vertical + right * horizontal).normalized;
        }
        else
        {
            moveDirection = inputDirection;
        }

        // Rotate character towards movement direction
       // Do not rotate when moving backwards with S
	if (moveDirection.magnitude > 0.1f && vertical >= 0f)
	{
    	Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

    	transform.rotation = Quaternion.Slerp(
        	transform.rotation,
	        targetRotation,
        	rotationSpeed * Time.deltaTime
    );
}
        // Apply gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMovement = moveDirection * moveSpeed;
        finalMovement.y = verticalVelocity;

        controller.Move(finalMovement * Time.deltaTime);

        // Update Idle / Running animation
        animator.SetFloat("Speed", inputDirection.magnitude);
    }
}

