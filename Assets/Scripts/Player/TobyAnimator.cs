
using UnityEngine;

public class TobyAnimator : MonoBehaviour
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
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < -0.2f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    public void Jumping()
    {
        animator.SetFloat("XVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetBool("isJumping", rb.velocity.y > 0);
        animator.SetBool("isGrounded", playerMovementScript.isGrounded);
    }
}
