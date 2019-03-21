using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerMovement movement;
    public bool facingRight = true;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;


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
        Vector2 localVel = transform.InverseTransformDirection(rb2D.velocity);
        animator.SetFloat("xMove", Mathf.Abs(localVel.x));
        animator.SetBool("onGround", movement.isGrounded);
    }

    void CheckFilp()
    {
        if (facingRight)
        {
            spriteRenderer.flipX = false;
        }
        else if (!facingRight)
        {
            spriteRenderer.flipX = true;
        }
    }
}
