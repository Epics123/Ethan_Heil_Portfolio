using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameManager gm;
    public Transform target;
    public Transform startTarget;
    public Vector3 offset;
    public float smoothTime;
    public float startSize = 8f;

    private Vector3 velocity = Vector3.zero;

    bool canFollow = false;
    bool zoomIn;
    bool zoomOut;
    float targetSize = 25f;
    float elapsedTime = 0.0f;
    float duration = 1.0f;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        transform.position = startTarget.position + new Vector3(0, 0, -10);
        zoomOut = true;
        StartCoroutine(CameraZoom());
    }

    void Update()
    {
        if(zoomOut)
        {
            gm.player.GetComponent<PlayerMovement>().canMove = false;
            elapsedTime += Time.deltaTime / duration;
            Camera.main.orthographicSize = Mathf.SmoothStep(startSize, targetSize, elapsedTime);
            if(elapsedTime >= duration)
            {
                zoomOut = false;
            }
        }

        if(zoomIn)
        {
            elapsedTime += Time.deltaTime / duration;
            Camera.main.orthographicSize = Mathf.SmoothStep(targetSize, startSize, elapsedTime);
            if (elapsedTime >= duration)
            {
                zoomIn = false;
                gm.player.GetComponent<PlayerMovement>().canMove = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(canFollow == true)
            FollowPlayer();
    }

    //Smooths camera from it's current position to the player's position
    void FollowPlayer()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothPosition;
    }

    IEnumerator CameraZoom()
    {
        elapsedTime = 0.0f;
        yield return new WaitForSeconds(3f);

        canFollow = true;
        zoomOut = false;
        zoomIn = true;
    }
}
