using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public Transform enemy;
    public Transform movePoint1;
    public Transform movePoint2;
    public Transform currentTarget;
    public float speed = 1.5f;
    public float smoothTime = 0.2f;

    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = movePoint1;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        Vector3 targetPos = currentTarget.position;
        enemy.position = Vector3.SmoothDamp(enemy.position, targetPos, ref velocity, smoothTime);
 
        if(enemy.position == movePoint1.position)
        {
            currentTarget = movePoint2;
        }
        else if(enemy.position == movePoint2.position)
        {
            currentTarget = movePoint1;
        }
    }
}
