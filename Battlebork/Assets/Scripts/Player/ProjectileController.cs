using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed = 3.0f;
    public int damage = 100;

    float timer = 0f;


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        timer += 1.0f * Time.deltaTime;
        gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed * Time.deltaTime);
        if (timer >= 2)
        {
            Destroy(gameObject);
        }
	}

   

 
}
