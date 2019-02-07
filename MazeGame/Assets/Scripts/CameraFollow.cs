using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes the camera follow the player with smoothing

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float smoothTime;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        FollowPlayer();
    }

    //Smooths camera from it's current position to the player's position
    void FollowPlayer()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime); 
        transform.position = smoothPosition;
    }
}
