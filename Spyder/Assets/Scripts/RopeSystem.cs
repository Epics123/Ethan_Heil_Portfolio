using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeSystem : MonoBehaviour
{
    public LayerMask ropeLayerMask;
    public PlayerMovement playerMovement;
    public float climbSpeed = 3f;
    public GameObject crosshairLight;

    [SerializeField] private GameObject ropeHingeAnchor;
    [SerializeField] private DistanceJoint2D ropeJoint;
    [SerializeField] private Transform crosshair;
    [SerializeField] private SpriteRenderer crosshairSprite;
    [SerializeField] private LineRenderer ropeRenderer;
    [SerializeField] private float maxCastDistance;
    [SerializeField] private bool distanceSet;
    [SerializeField] private List<Vector2> ropePositions = new List<Vector2>();

    private bool ropeAttached;
    private bool isColliding;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();

	private GrappleSound soundSource;
     
    void Awake()
    {
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

	private void Start()
	{
		soundSource = GetComponent<GrappleSound>();
	}

	// Update is called once per frame
	void Update()
    {
        HandleInput(CalculateAimDirection());
        UpdateRopePositions();
        HandleRopeLength();
        HandleRopeUnwrap();
    }

    Vector3 CalculateAimDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        Vector3 facingDirection = mousePos - transform.position;
        float aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);

        if (aimAngle < 0f)
            aimAngle = Mathf.PI * 2 + aimAngle;

        Vector3 aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        playerPosition = transform.position;

        if (!ropeAttached)
            SetCrosshairPosition(aimAngle, mousePos);
        else
        {
            playerMovement.isSwinging = true;
            playerMovement.ropeHook = ropePositions.Last();
            crosshairSprite.enabled = false;
            crosshairLight.SetActive(false);
            WrapRope();
        }

        return aimDirection;
    }

    private void SetCrosshairPosition(float angle, Vector3 mousePos)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
            crosshairLight.SetActive(true);
        }

        float xPos = mousePos.x;
        float yPos = mousePos.y;

        Vector3 crosshairPos = new Vector3(xPos, yPos);
        float distance = Vector3.Distance(crosshairPos, transform.position);

        if(distance > maxCastDistance)
        {
            Vector3 newCrosshairPos = crosshairPos - transform.position;
            newCrosshairPos *= maxCastDistance / distance;
            crosshairPos = transform.position + newCrosshairPos;
            crosshair.transform.position = crosshairPos;
        }
        else
        {
            crosshair.transform.position = crosshairPos;
        }
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetMouseButtonDown(0) && !playerMovement.zoom)
        {
            if (ropeAttached)
                return;
            ropeRenderer.enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(playerPosition, aimDirection, maxCastDistance, ropeLayerMask);

            if(hit.collider != null)
            {
                ropeAttached = true;
                playerMovement.canMove = true;
                playerMovement.afterSwing = false;
                if (!ropePositions.Contains(hit.point))
                {
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;
					soundSource.playHit();
                }
            }
            else
            {
				soundSource.playLaunch();
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            playerMovement.canMove = false;
            playerMovement.afterSwing = true;
            ResetRope();
        }
    }

    private void HandleRopeLength()
    {
        if (Input.GetKey(KeyCode.W) && ropeAttached)
        {
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
			soundSource.playMoveUp();
        }
        else if (Input.GetKey(KeyCode.S) && ropeAttached)
        {
            ropeJoint.distance += Time.deltaTime * climbSpeed;
            ropeJoint.distance = Mathf.Clamp(ropeJoint.distance, 0, maxCastDistance);
			soundSource.playMoveDown();
        }
    }

    private void HandleRopeUnwrap()
    {
        if (ropePositions.Count <= 1)
            return;

        int anchorIndex = ropePositions.Count - 2;
        var hingeIndex = ropePositions.Count - 1;
        Vector2 anchorPosition = ropePositions[anchorIndex];
        Vector2 hingePosition = ropePositions[hingeIndex];
        Vector2 hingeDir = hingePosition - anchorPosition;
        float hingeAngle = Vector2.Angle(anchorPosition, hingeDir);
        Vector2 playerDir = playerPosition - anchorPosition;
        float playerAngle = Vector2.Angle(anchorPosition, playerDir);

        if (!wrapPointsLookup.ContainsKey(hingePosition))
        {
            Debug.LogError("Not tracking hingePosition (" + hingePosition + ") in the look up dictionary.");
            return;
        }

        if (playerAngle < hingeAngle)
        {
            if(wrapPointsLookup[hingePosition] == 1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }

            wrapPointsLookup[hingePosition] = -1;
        }
        else
        {
            if(wrapPointsLookup[hingePosition] == -1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }

            wrapPointsLookup[hingePosition] = 1;
        }
    }

    private void UnwrapRopePosition(int anchorIndex, int hingeIndex)
    {
        Vector2 newAnchorPosition = ropePositions[anchorIndex];
        wrapPointsLookup.Remove(ropePositions[hingeIndex]);
        ropePositions.RemoveAt(hingeIndex);

        ropeHingeAnchorRb.transform.position = newAnchorPosition;
        distanceSet = false;

        // Set new rope distance joint distance for anchor position if not yet set.
        if (distanceSet)
            return;

        ropeJoint.distance = Vector2.Distance(transform.position, newAnchorPosition);
        distanceSet = true;
    }

    public void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
        wrapPointsLookup.Clear();
    }

    private void UpdateRopePositions()
    {
        if (!ropeAttached)
            return;

        ropeRenderer.positionCount = ropePositions.Count + 1;
        for(int i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) //If not the last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    Vector2 ropePos = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePos;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePos);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePos;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePos);
                            distanceSet = true;
                        }
                    }
                }
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    Vector2 ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }

    private Vector2 GetClosestColliderPointFromRaycast(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {
        var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
         position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)), //position is a float returned by Vector2.Distance
         position => polyCollider.transform.TransformPoint(position)); //position is a Vector3 of the collider in world space

        var orderedDictionary = distanceDictionary.OrderBy(element => element.Key);
        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero; //Returns the closest rope position to the player's current position
    }

    private void WrapRope()
    {
        if(ropePositions.Count > 0)
        {
            //Fire a raycast out from the player's position, in the direction of the player looking at the last rope position in the list 
            //with a raycast distance set to the distance between the player and rope pivot position.
            Vector2 lastRopePoint = ropePositions.Last();
            RaycastHit2D playerToNextHit = Physics2D.Raycast(playerPosition, (lastRopePoint - playerPosition).normalized, Vector2.Distance(playerPosition, lastRopePoint) - 0.1f, ropeLayerMask);

            //If raycast hits something
            if (playerToNextHit)
            {
                PolygonCollider2D colliderWithVerticies = playerToNextHit.collider as PolygonCollider2D; //Cast hit collider to a Polygon2D collider
                if(colliderWithVerticies != null)
                {
                    Vector2 closestPointToHit = GetClosestColliderPointFromRaycast(playerToNextHit, colliderWithVerticies);

                    //Check if position is being wrapped again
                    if (wrapPointsLookup.ContainsKey(closestPointToHit))
                    {
                        ResetRope();
                        return;
                    }

                    ropePositions.Add(closestPointToHit);
                    wrapPointsLookup.Add(closestPointToHit, 0);
                    distanceSet = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
}
