using System.Collections;
using UnityEngine;

public class FaceMouse : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        faceMouse();
	}

    public void faceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = direction;
    }
}
