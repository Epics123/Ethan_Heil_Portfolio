using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour {

    public Camera cam;

    public float maxCamSize;
    public float startCamSize;
    public int speed;

    public bool zoomIn;
    public bool zoomOut;

    // Use this for initialization
    void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        zoomIn = false;
        zoomOut = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (zoomIn)
        {
            zoomOut = false;
            ZoomIn();
        }
        if (zoomOut)
        {
            zoomIn = false;
            ZoomOut();
        }
	}

    void ZoomIn()
    {
        if (cam.orthographicSize >= startCamSize)
        {
            cam.orthographicSize -= Time.deltaTime * speed;
        }
        else
        {
            zoomIn = false;
        }
    }

    void ZoomOut()
    {
        if(cam.orthographicSize <= maxCamSize)
        {
            cam.orthographicSize += Time.deltaTime * speed;
        }

    }
}
