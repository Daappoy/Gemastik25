using System;
using System.Collections;
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
    public AudioManager audioManager;
    private bool isWalkSoundPlaying = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    void Update()
    {
        GroundCheck();
        if (canMove && isGrounded)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
            

            if (isGrounded && Math.Abs(moveInput.x) > 0 && !isWalkSoundPlaying)
            {
                StartCoroutine(WalkSound());
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void GroundCheck()
    {
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        isJumping = !isGrounded && rb.velocity.y > 0;
    }

    IEnumerator WalkSound()
    {
        isWalkSoundPlaying = true;
        while (isGrounded && canMove && Math.Abs(moveInput.x) > 0)
        {
            audioManager.PlaySFX(audioManager.WalkSound);
            yield return new WaitForSeconds(0.5f);
        }
        isWalkSoundPlaying = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (isGrounded && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioManager.PlaySFX(audioManager.JumpSound);
        }
        isJumping = !isGrounded && rb.velocity.y > 0;
    }
}
