using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public Transform restartPoint;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = restartPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CheckRestart()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}
