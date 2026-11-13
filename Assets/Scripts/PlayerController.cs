using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float groundCheckDistance = 0.05f;
    //[SerializeField] private float jumpRepeatTime = 1f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private bool shouldFaceMoveDirection = false;
    private float lastJumpTime;
    private float verticalVelocity;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (context.performed && moveInput.x != 0 || moveInput.y != 0)
        {
            animator.SetBool("walking", true);
            animator.SetBool("standing", false);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("standing", true);
        }
        Debug.Log($"Move input: {moveInput}");
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log($"Move input: {context.performed} - is grounded: {controller.isGrounded}");
        if (context.performed && controller.isGrounded)
        {
            Debug.Log("We are supposed to jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Update()
    {
        //bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        //bool isMoving = moveDir

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (shouldFaceMoveDirection && moveDirection.sqrMagnitude>0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation,10f*Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
