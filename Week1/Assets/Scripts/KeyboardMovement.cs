using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{

    float xMove = 0f;
    float yMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckInput();
        MovePlayer();
    }

    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
    }

    void MovePlayer()
    {
        transform.Translate((new Vector3(xMove, yMove, 0)*5) * Time.deltaTime);
    }

}
