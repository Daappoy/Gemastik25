using System.Collections;
using System.Collections.Generic;
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
        // Example of how you might use the animator and rb in Update
        if (rb.velocity.x > 0)
        {
            // Flip the player to face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            // Flip the player to face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    public void Jumping()
    {
        // Set animator parameters based on jumping state
        animator.SetFloat("XVelocity", rb.velocity.x);
        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetBool("isJumping", rb.velocity.y > 0);
        animator.SetBool("isGrounded", playerMovementScript.isGrounded);
    }
}
