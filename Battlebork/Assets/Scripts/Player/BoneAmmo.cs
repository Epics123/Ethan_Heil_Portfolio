using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneAmmo : MonoBehaviour {

    bool hit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && hit != true)
        {
            hit = true;
            collision.GetComponent<PlayerAblilities>().boneCount++;
            collision.GetComponent<PlayerAblilities>().count--;
            Destroy(gameObject);
        }
    }


}
