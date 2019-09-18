using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTagStarter : MonoBehaviour
{
    public GameObject[] Players;
	

	void Start ()
    {
        int index = Random.Range(0, Players.Length);
        Players[index].GetComponent<TaggingManager>().tagged = true;
        Players[index].GetComponent<SpriteRenderer>().color = Color.red;
    }
	
	void Update ()
    {
        	
	}
}
