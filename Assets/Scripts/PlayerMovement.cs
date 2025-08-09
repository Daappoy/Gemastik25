using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove;
    public float moveSpeed;
    public float jumpForce;
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField]
    public bool isGrounded = true;
    public bool isJumping = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundCheck();
        
        // Apply movement continuously based on stored input
        if (canMove)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Store the input value to be used in Update
        moveInput = context.ReadValue<Vector2>();
        // Debug.Log("Player input: " + moveInput);
    }

    void GroundCheck()
    {
        // debugging raycast for ground check

        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // }
        isJumping = !isGrounded && rb.velocity.y > 0;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // Debug.Log("Player jumped");
        }
        isJumping = !isGrounded && rb.velocity.y > 0;
    }
}
