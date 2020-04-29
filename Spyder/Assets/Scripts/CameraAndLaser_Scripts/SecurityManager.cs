using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityManager : MonoBehaviour
{
    /// <summary>
    ///  THERE SHOULD ONLY BE ONE OF THIS OBJECT
    /// </summary>

    // * Variables *
    GameObject[] linkedLasers;


    // ** Update Functions **
    private void Awake()
    {
        linkedLasers = GameObject.FindGameObjectsWithTag("LinkedLaser");
    }
    private void Start()
    {
        
    }


    // **** Other Functions ****
    public void Trigger(string linkCode)
    {
        foreach (GameObject linkedLaser in linkedLasers)
        {
            if (linkedLaser.GetComponent<LinkedLaser>().linkCode == linkCode)
            {
                linkedLaser.GetComponent<LinkedLaser>().TurnOnLaser();
            }
        }
    }


    public void SetLaserColor(string linkCode, Color laserColor)
    {
        foreach (GameObject linkedLaser in linkedLasers)
        {
            if (linkedLaser.GetComponent<LinkedLaser>().linkCode == linkCode)
            {
                linkedLaser.GetComponent<LinkedLaser>().GetLaserColor(laserColor);
            }
        }
    }

}
