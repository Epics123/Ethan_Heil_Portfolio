using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleCheckCollision : MonoBehaviour
{
    public World2Obsticle collisionCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Orb")
        {
            collisionCheck.visable = true;
            collisionCheck.StartCoroutine(collisionCheck.HideObject());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Orb")
        {
            collisionCheck.visable = false;
        }
    }

}
