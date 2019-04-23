using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World2Obsticle : MonoBehaviour
{
    public GameObject player;
    public bool visable = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.layer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(visable == true)
        {
            gameObject.layer = 8;
        }
        else
        {
            gameObject.layer = 10;
        }
    }

    public IEnumerator HideObject()
    {
        yield return new WaitForSeconds(12);
        visable = false;
    }
}
