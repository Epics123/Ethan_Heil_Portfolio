using UnityEngine;
using System.Collections;

public class CollisionState : MonoBehaviour
{

   // public LayerMask collisionLayer;
    public bool standing;
    public bool onWall;
    public Vector2 bottomPosition = Vector2.zero;
    public Vector2 rightPosition = Vector2.zero;
    public Vector2 leftPosition = Vector2.zero;
    public float collisionRadius = 10f;
    public Color debugCollisionColor = Color.red;
    private bool touching;

    private InputState inputState;

    //private GameObject[] solidObjects;
    public Collider2D[] collidingObjects;// = { };

    // Use this for initialization
    void Awake()
    {
        inputState = GetComponent<InputState>();
    }
    public void getSolidObjects()
    {
        //solidObjects = GameObject.FindGameObjectsWithTag("Solid");
    }
    void FixedUpdate()
    {

        var pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;
        touching = false;
        collidingObjects = Physics2D.OverlapCircleAll(pos, collisionRadius);
        if (collidingObjects != null && collidingObjects.Length > 0)
        {
            foreach (Collider2D collider in collidingObjects)
            {
                if (collider.gameObject.layer == gameObject.layer && collider.gameObject.name.Substring(0,6) != "Player" && collider.gameObject.name != "Switch")
                {
                    touching = true;
                    standing = true;
                    break;
                }
            }
        }
        if (!touching)
        {
           standing = false;
        }

        pos = inputState.direction == Directions.Right ? rightPosition : leftPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        touching = false;
        collidingObjects = Physics2D.OverlapCircleAll(pos, collisionRadius);
        if (collidingObjects != null && collidingObjects.Length > 0)
        {
            foreach (Collider2D collider in collidingObjects)
            {
                if (collider.gameObject.layer == gameObject.layer && collider.gameObject.name.Substring(0, 6) != "Player" && collider.gameObject.name != "Switch")
                {
                    touching = true;
                    onWall = true;
                    break;
                }
            }
        }
        if (standing || !touching)
        {
           onWall = false;
        }
        collidingObjects = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        var positions = new Vector2[] { rightPosition, bottomPosition, leftPosition };

        foreach (var position in positions)
        {
            var pos = position;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            Gizmos.DrawWireSphere(pos, collisionRadius);
        }
    }
}
