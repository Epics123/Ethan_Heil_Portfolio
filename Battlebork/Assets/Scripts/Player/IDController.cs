using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDController : MonoBehaviour {

    public GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void UpdateChallenge()
    {
        if (gameObject.tag == gm.GetWave().challenge.enemyToKill)
        {
            gm.GetWave().challenge.numToKill--;
        }

    }

  
}
