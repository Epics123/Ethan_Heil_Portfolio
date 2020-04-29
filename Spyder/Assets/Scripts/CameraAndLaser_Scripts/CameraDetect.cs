using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{
    // * Variables *
    public bool seesPlayer = false;


    // ** Update Functions **
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            seesPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            seesPlayer = false;
        }
    }


    // **** Other Functions ****


}
