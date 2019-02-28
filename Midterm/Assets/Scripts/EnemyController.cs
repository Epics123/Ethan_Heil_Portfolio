using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    bool mouseOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseDown()
    {
        if(mouseOver)
        {
            Destroy(gameObject);
        }
    }
}
