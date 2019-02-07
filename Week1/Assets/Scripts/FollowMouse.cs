using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Follow();
        FollowWithEase();
    }

    void Follow()
    {
        Vector3 mousePos = Input.mousePosition; //get mouse position in screen pixels
        Debug.Log(mousePos);
        //Convert screen coordinates to world space
        Vector3 adjustedPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Clear z axis in vector 3
        adjustedPos.z = 0f;
        //reposition player
        transform.position = adjustedPos;
    }

    void FollowWithEase()
    {
        Vector3 playerPos = transform.position; //get player position in world space
        //Converts world space into screen coordinates
        playerPos = Camera.main.WorldToScreenPoint(playerPos);
        Vector3 mousePos = Input.mousePosition; //get mouse position in screen pixels
        Vector3 diff = playerPos - mousePos; //get difference player and mouse
        playerPos -= diff / 8; //divide by number, the higher the number, the longer the transition
        //convert screen coordinates to world space
        Vector3 adjustedPos = Camera.main.ScreenToWorldPoint(playerPos);
        //zero out the z
        adjustedPos.z = 0;
        transform.position = adjustedPos;
    }
}
