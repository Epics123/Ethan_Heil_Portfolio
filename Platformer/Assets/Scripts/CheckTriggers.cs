using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggers : MonoBehaviour
{

    public GameObject loopCenter;
    public bool inLoop = false;
    public bool startLoop = false;

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
        if (collision.tag == "Loop")
        {
            if (inLoop == true)
            {
                inLoop = false;
            }
            else
            {
                inLoop = true;
                loopCenter = collision.gameObject.GetComponent<LoopMovement>().center;
            }
        }

        if(collision.tag == "LoopMid")
        {
            if(startLoop == true)
            {
                startLoop = false;
            }
            else
            {
                startLoop = true;
            }
        }
    }
}
