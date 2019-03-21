using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorKey : MonoBehaviour
{
    public GameObject door;
    public GameManager gm;
    public string color;

    bool changed = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(changed == false)
            {
                gm.doorMessage.enabled = true;
                gm.StartCoroutine(gm.HideMessage());
                if (color == "Orange")
                {
                    gm.doorMessage.text = gm.doorMessage.text.Insert(0, color);
                }
                else if (color == "Blue")
                {
                    gm.doorMessage.text = gm.doorMessage.text.Insert(0, color);
                }
                else if (color == "Purple")
                {
                    gm.doorMessage.text = gm.doorMessage.text.Insert(0, color);
                }
                else if (color == "Green")
                {
                    gm.doorMessage.text = gm.doorMessage.text.Insert(0, color);
                }
                changed = true;
            }
            

            door.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    
}
