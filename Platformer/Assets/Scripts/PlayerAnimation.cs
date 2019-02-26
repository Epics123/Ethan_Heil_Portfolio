using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerMovement movement;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimation();
        CheckFilp();
    }

    void CheckAnimation()
    {
        animator.SetFloat("xMove", Mathf.Abs(rb2D.velocity.x));
        animator.SetBool("onGround", movement.isGrounded);
    }

    void CheckFilp()
    {
        if(facingRight && rb2D.velocity.x < -0.1f)
        {
            Flip();
        }
        else if (!facingRight && rb2D.velocity.x > 0.1f)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
