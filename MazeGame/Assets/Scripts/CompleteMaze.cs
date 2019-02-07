using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Triggers win state or starts the swap to a new level

public class CompleteMaze : MonoBehaviour
{
    public Text winText;
    public GameManager gm;

    //Check if player collied with this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            gm.GetComponent<CameraManager>().SwapCameras(); //Swap levels
            gm.player.transform.position = gm.spawnPoint.transform.position; //reset player position
            gm.floor.GetComponent<SpriteRenderer>().color = gm.originalColor; //reset floor color

            //display win text
            if (gm.win == true)
            {
                gm.floor.GetComponent<SpriteRenderer>().color = gm.originalColor;
                winText.enabled = true;
                gm.timerCount = false;
            }
            
        }
    }
}
