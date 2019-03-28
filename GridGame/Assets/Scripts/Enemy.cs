using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gm;
    public int startX = 0;
    public int startY = 0;
    public float speed = 5f;
    public bool moveVertical;
    public bool moveHorizontal;

    public Vector2 target;
    bool isMoving;
    bool movePositive;
    bool moveNegative;

    readonly float spacer = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        transform.position = new Vector2(startX + (spacer * startX), startY + (spacer * startY));
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == true)
        {
            LerpEnemy();       
        }
    }

    public void LerpEnemy()
    {
        if (isMoving == false)
        {
            target = new Vector2(transform.position.x + 1, transform.position.y);
            isMoving = true;
        }
        else if (isMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
            if (transform.position.Equals(target))
            {
                isMoving = false;
                target = new Vector2(transform.position.x + 1, transform.position.y);
            }
        }
    }

    public bool CheckBounds()
    {
        if(moveHorizontal == true)
        {
            if(transform.position.x + 1 >= gm.gridRows || transform.position.x < 0)
            {
                return false;
            }
        }

        if(moveVertical == true)
        {
            if (transform.position.y + 1 >= gm.gridCols || transform.position.y < 0)
            {
                return false;
            }
        }

        return true;
    }
}
