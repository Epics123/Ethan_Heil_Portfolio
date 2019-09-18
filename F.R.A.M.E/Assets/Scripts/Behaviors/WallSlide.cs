using UnityEngine;
using System.Collections;

public class WallSlide : StickToWall
{

    public float slideVelocity = -5f;

    private float timeElapsed = 0f;

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        if (onWallDetected && !collisionState.standing)
        {
            var velY = slideVelocity * 10;

            body2D.velocity = new Vector2(body2D.velocity.x, velY);

            timeElapsed += Time.deltaTime;
        }
    }

    override protected void OnStick()
    {
        body2D.velocity = Vector2.zero;
    }

    override protected void OffWall()
    {
        timeElapsed = 0;
    }
}
