using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorChange : MonoBehaviour
{
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            gm.floor.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            for (int i = 0; i < gm.doors.Length; i++)
            {
                gm.doors[i].SetActive(true);
                if (gm.doors[i].GetComponent<Tilemap>().color == gm.floor.GetComponent<SpriteRenderer>().color)
                {
                    gm.doors[i].SetActive(false);
                }
            }
        }
        
    }
}
