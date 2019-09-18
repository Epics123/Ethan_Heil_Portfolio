using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : AbstractBehavior
{

    public float jumpSpeed = 200f;
    public float jumpDelay = .1f;
    public int jumpCount = 2;

    protected float lastJumpTime = 0;
    protected int jumpRemaining = 0;
    //public GameObject dustEffectPrefab;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        var canJump = inputState.GetButtonValue(inputButtons[0]);
        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

        if (collisionState.standing)
        {
            if (canJump && holdTime < .1f)
            {
                jumpRemaining = jumpCount - 1;
                OnJump();
            }
        }
        else
        {
            if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay)
            {
                if (jumpRemaining > 0)
                {
                    OnJump();
                    jumpRemaining--;
                    // var clone = Instantiate(dustEffectPrefab);
                    // clone.transform.position = transform.position;
                }
            }
        }
    }
    protected virtual void OnJump()
    {
        var vel = body2D.velocity;
        lastJumpTime = Time.time;
        body2D.velocity = new Vector2(vel.x, jumpSpeed);
    }
}
