using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongJump : Jump
{
    public float longJumpDelay = .15f;
    public float longJumpMultiplier = 1.5f;
    public bool canLongJump;
    public bool isLongJumping;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        var canJump = inputState.GetButtonValue(inputButtons[0]);
        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

        if (!canJump)
            canLongJump = false;

        if (collisionState.standing && !isLongJumping)
        {
            canLongJump = false;
        }
        base.Update();

        if (canLongJump && !collisionState.standing && holdTime > longJumpDelay)
        {
            var vel = body2D.velocity;
            body2D.velocity = new Vector2(vel.x, jumpSpeed * longJumpMultiplier);
            canLongJump = false;
            isLongJumping = true;
        }
    }
    protected override void OnJump()
    {
        base.OnJump();
        canLongJump = true;
    }
}
