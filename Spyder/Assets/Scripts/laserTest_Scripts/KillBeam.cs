using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBeam : MonoBehaviour
{
    // * Variables *
    SpriteRenderer spr;
    BoxCollider2D bxCl;

    public GameObject laserSpriteLight;
    public GameObject laserLight;
    public bool laserOn;


    // ** Update Functions **
    private void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        bxCl = gameObject.GetComponent<BoxCollider2D>();
        if (laserOn)
            spr.enabled = true;
        else
        {
            spr.enabled = false;    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<KillPlayer>().GetKilled();
        }
    }

    // **** Other Functions ****
    public bool EnableDisable() // Turns on & off the laser
    {
		bool result = false;
        if (laserSpriteLight.activeSelf)
        {
            if (laserOn)
                spr.enabled = false;
            laserSpriteLight.SetActive(false);
            laserLight.SetActive(false);
            bxCl.enabled = false;
			result = false;
        } 
        else
        {
            if (laserOn)
                spr.enabled = true;
            laserSpriteLight.SetActive(true);
            laserLight.SetActive(true);
            bxCl.enabled = true;
			result = true;
        }

		return result;
    }

}
