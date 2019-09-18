using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    // Use this for initialization
    private InputState inputState;
    private Walk walkBehavior;
    //private Duck duckBehavior;
    private Animator animator;
    private CollisionState collisionState;

    void Awake()
    {
        inputState = GetComponent<InputState>();
        walkBehavior = GetComponent<Walk>();
        animator = GetComponent<Animator>();
        collisionState = GetComponent<CollisionState>();
        //duckBehavior = GetComponent<Duck>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (collisionState.standing)
        {
            ChangeAnimationState(0);
        }
        if (inputState.absVelX > 0)
        {
            ChangeAnimationState(1);
        }

        if (inputState.absVelY > .01)
        {
            ChangeAnimationState(2);
        }
        /*
        if (duckBehavior.ducking)
        {
            ChangeAnimationState(3);
        }
        */
        if (!collisionState.standing && collisionState.onWall)
        {
            ChangeAnimationState(4);
        }

        animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;
    }

    void ChangeAnimationState(int value)
    {
        animator.SetInteger("AnimState", value);
    }

}
