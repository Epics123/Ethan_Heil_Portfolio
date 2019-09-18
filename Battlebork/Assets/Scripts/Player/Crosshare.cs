using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshare : MonoBehaviour {

    private void Awake()
    {
        Cursor.visible = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        gameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        gameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
    }
}
