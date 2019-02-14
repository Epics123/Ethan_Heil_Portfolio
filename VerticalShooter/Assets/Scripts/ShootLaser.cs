using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{

    public GameObject laser;
    public Transform laserSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        GameObject newLaser = Instantiate(laser, laserSpawn.position, laserSpawn.rotation);
    }
}
