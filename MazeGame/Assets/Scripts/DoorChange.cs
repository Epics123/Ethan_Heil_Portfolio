using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Changes which doors can be passed through and the color of the floor

public class DoorChange : MonoBehaviour
{
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    //Check if player collied with this object 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            gm.floor.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color; //Set floor color to color of this object
            for (int i = 0; i < gm.doors.Length; i++)
            {
                gm.doors[i].SetActive(true); //Set all doors to active
                if (gm.doors[i].GetComponent<Tilemap>().color == gm.floor.GetComponent<SpriteRenderer>().color)
                {
                    gm.doors[i].SetActive(false); //Deactivate all doors that are the same color as this object
                }
            }
        }
        
    }
}
