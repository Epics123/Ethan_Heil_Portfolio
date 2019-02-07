using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteMaze : MonoBehaviour
{
    public Text winText;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            gm.GetComponent<CameraManager>().SwapCameras();
            gm.player.transform.position = gm.spawnPoint.transform.position;
            gm.floor.GetComponent<SpriteRenderer>().color = gm.originalColor;
            if (gm.win == true)
            {
                gm.floor.GetComponent<SpriteRenderer>().color = gm.originalColor;
                winText.enabled = true;
                gm.timerCount = false;
            }
            
        }
    }
}
