using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior
{

    public float speed = 50f;
    public float runMultiplier = 2f;
    public bool running;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        running = false;

        var right = inputState.GetButtonValue(inputButtons[0]);
        var left = inputState.GetButtonValue(inputButtons[1]);

        if (right || left)
        {

            var tmpSpeed = speed;

            if (collisionState.standing)
            {
                tmpSpeed *= runMultiplier;
                running = true;
            }

            var velX = tmpSpeed * (float)inputState.direction;

            body2D.velocity = new Vector2(velX, body2D.velocity.y);

        }
        else
        {
            body2D.velocity = new Vector2(0, body2D.velocity.y);
        } 
    }
}
