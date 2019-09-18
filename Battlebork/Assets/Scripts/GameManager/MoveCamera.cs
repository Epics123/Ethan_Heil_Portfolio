using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform target;
    public Camera cam;
    public GameObject player;

    public Transform[] camTargets;

    public float speed;
    public bool move;
    public int idx;

    Vector3 startPos;
    Vector3 targetPos;

    float timer;
    long count = 0;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        startPos = transform.position;
        timer = 0.0f;
        idx = 0;
        move = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(move == true)
        {
            StartCoroutine("LerpCam");
            count = 0;
        }

        if(transform.position == targetPos)
        {
            StopCoroutine("LerpCam");
            move = false;
            cam.GetComponent<ZoomController>().zoomIn = true;   
            if(count < 1)
            {
                startPos = transform.position;
                timer = 0;
                player.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);
                idx++;
            }
            count++;
            
        }
        
	}

    IEnumerator LerpCam()
    {
        target = camTargets[idx];
        targetPos = target.transform.position;
        yield return new WaitForSeconds(1.0f);
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, targetPos, timer/speed);
    }

    
}
