
using UnityEngine;

public class ScansAnimator : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public PlayerMovement playerMovementScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        facing();
        Jumping();
    }

    public void facing()
    {
        if (rb.velocity.x > 0.2f)
        {

        }
        else if (rb.velocity.x < -0.2f)
        {

        }

        animator.SetFloat("XVelocity", rb.velocity.x);

    }

    public void Jumping()
    {
        animator.SetBool("isJumping", rb.velocity.y > 0);
        animator.SetBool("isGrounded", playerMovementScript.isGrounded);
        animator.SetFloat("YVelocity", rb.velocity.y);
    }

}
