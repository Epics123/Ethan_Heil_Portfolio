using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
	public Transform currentTarget;
	public Transform player;

	public float minCamSize = 5f;
	public float maxCamSize = 11f;
	public float zoomTime = 2f;

	[SerializeField]
	public float leftBoundX;

	[SerializeField]
	public float rightBoundX;

	[SerializeField]
	public float topBoundY;

	[SerializeField]
	public float bottomBoundY;

	[SerializeField]
	private float height = -10.0f;

	[SerializeField]
	private float followSpeed = 2.0f;

	private float stepWidth;
	private bool zoom = false;

	private void Start()
	{
		zoom = true;
		player.GetComponent<PlayerMovement>().zoom = true;
		StartCoroutine(DisableZoom());
	}

	private void FixedUpdate()
	{
		ClampedDampedMove();
	}

	private void LateUpdate()
	{
		if (zoom)
		{
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, maxCamSize, zoomTime * Time.deltaTime);
		}
		else
		{
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, minCamSize, zoomTime * Time.deltaTime);
		}
	}

	private void ClampedDampedMove()
	{
		stepWidth = followSpeed * Time.deltaTime *
			Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(currentTarget.position.x, currentTarget.position.y));

		transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, stepWidth);

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBoundX, rightBoundX), 
										 Mathf.Clamp(transform.position.y, bottomBoundY, topBoundY),
										 height);
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		//Draw boundry lines in editor
		Gizmos.DrawLine(new Vector2(leftBoundX, topBoundY), new Vector2(rightBoundX, topBoundY));
		Gizmos.DrawLine(new Vector2(rightBoundX, topBoundY), new Vector2(rightBoundX, bottomBoundY));
		Gizmos.DrawLine(new Vector2(rightBoundX, bottomBoundY), new Vector2(leftBoundX, bottomBoundY));
		Gizmos.DrawLine(new Vector2(leftBoundX, bottomBoundY), new Vector2(leftBoundX, topBoundY));
	}

	IEnumerator DisableZoom()
	{
		yield return new WaitForSeconds(zoomTime * 2);

		zoom = false;
		currentTarget = player;
		player.GetComponent<PlayerMovement>().zoom = false;
	}

}
