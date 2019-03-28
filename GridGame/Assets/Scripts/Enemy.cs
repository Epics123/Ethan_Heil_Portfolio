using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startX = 0;
    public int startY = 0;
    public float speed = 5f;
    public bool moveVertical;
    public bool moveHorizontal;

    Square target;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(startX, startY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LerpEnemy(Square square = null)
    {
        if (isMoving == false)
        {
            target = square;
            isMoving = true;
        }
        else if (isMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            if (transform.position == target.transform.position)
            {
                isMoving = false;
            }
        }
    }

    public bool CheckBounds(Square square = null)
    {
        if(moveHorizontal == true)
        {
            if(transform.position.x + 1 > 1 || transform.position.x < 0)
            {
                return false;
            }
        }

        if(moveVertical == true)
        {
            if (transform.position.y + 1 > 1 || transform.position.y < 0)
            {
                return false;
            }
        }

        return true;
    }
}
