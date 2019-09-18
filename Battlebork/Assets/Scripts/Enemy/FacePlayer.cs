using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        facePlayer();
	}

    void facePlayer()
    { 
        if(player != null)
        {
            Vector3 target = player.transform.position;
            Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
            transform.up = direction;
        }
          
    }
}
