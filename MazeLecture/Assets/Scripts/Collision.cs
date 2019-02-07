using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit Wall");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Leaving Wall");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("In Wall");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
