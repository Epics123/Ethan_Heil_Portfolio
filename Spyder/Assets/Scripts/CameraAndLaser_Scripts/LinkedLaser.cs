using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedLaser : MonoBehaviour
{
    // * Variables *
    KillBeam actualLaser;
    SpriteRenderer indicator;
    SpriteRenderer beam;

    public string linkCode; // This must match the camera that triggers it

    //public Color laserColor;

    // ** Update Functions **
    private void Awake()
    {
        actualLaser = gameObject.transform.Find("LaserBeam").GetComponent<KillBeam>();
        indicator = gameObject.transform.Find("Indicator").GetComponent<SpriteRenderer>();
        beam = gameObject.transform.Find("LaserBeam").GetComponent<SpriteRenderer>();
    }

    // **** Other Functions ****
    public void TurnOnLaser()
    {
        if (actualLaser.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            actualLaser.EnableDisable();
        }
    }

    public void GetLaserColor(Color color)
    {
        beam.color = color;
        indicator.color = color;
    }
}
