using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ProbeAnimator : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public PlayerMovement playerMovementScript;
    private float velocityX;
    private float velocityY;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Update velocity values every frame
        velocityX = Mathf.Abs(rb.velocity.x);
        velocityY = rb.velocity.y;
        
        // Debug.Log(velocityY);
        facing();
        isJumping();
    }

    public void facing()
    {
        // Example of how you might use the animator and rb in Update
        if (rb.velocity.x > 0.2f)
        {
            // Flip the player to face right
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < -0.2f)
        {
            // Flip the player to face left
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void isJumping()
    {
        animator.SetFloat("XVelocity", velocityX);
        animator.SetFloat("YVelocity", velocityY);
        animator.SetBool("isGrounded", playerMovementScript.isGrounded);
        animator.SetBool("isJumping", playerMovementScript.isJumping);
    }
}
