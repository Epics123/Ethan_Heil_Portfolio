using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    Square target;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == true)
        {
            LerpPlayer();
        }
    }

    public void MovePlayer(Square square = null)
    {
        gameObject.transform.position = square.transform.position;
    }

    public void LerpPlayer(Square square = null)
    {
        if(isMoving == false)
        {
            target = square;
            isMoving = true;
        }
        else if(isMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            if (transform.position == target.transform.position)
            {
                isMoving = false;
            }
        }
    }
}
