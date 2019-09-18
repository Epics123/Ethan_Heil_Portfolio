using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour {

    public int score;
    public int health;
    public GameObject id;

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bone")
        {
            health -= collision.GetComponent<ProjectileController>().damage;
            if(health <= 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().gameScore += score;
                gameObject.GetComponentInChildren<IDController>().UpdateChallenge();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }

}
