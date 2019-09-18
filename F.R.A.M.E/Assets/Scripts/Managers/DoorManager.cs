using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {

    public CapsuleCollider2D[] capsuls;
    public GameObject[] players;
    public Sprite doorClose;
    public Sprite doorOpen;
    public bool activated = false;
    public bool cooldown = false;
    int doorLayer;



    // Use this for initialization
    void Start () {
        capsuls = new CapsuleCollider2D[4];
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            capsuls[i] = players[i].GetComponent<CapsuleCollider2D>();
        }
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
        CheckCollision();
	}

    void CheckCollision()
    {
        if (capsuls[0].IsTouching(GetComponent<BoxCollider2D>()) && !capsuls[3].gameObject.GetComponent<TaggingManager>().tagged)
        {
            StartCoroutine(CloseDoor());
        }
        if (capsuls[1].IsTouching(GetComponent<BoxCollider2D>()) && !capsuls[3].gameObject.GetComponent<TaggingManager>().tagged)
        {
            StartCoroutine(CloseDoor());
        }
        if (capsuls[2].IsTouching(GetComponent<BoxCollider2D>()) && !capsuls[3].gameObject.GetComponent<TaggingManager>().tagged)
        {
            StartCoroutine(CloseDoor());
        }
        if (capsuls[3].IsTouching(GetComponent<BoxCollider2D>()) && !capsuls[3].gameObject.GetComponent<TaggingManager>().tagged)
        {
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(3.0f);
        GetComponent<SpriteRenderer>().sprite = doorOpen;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().sprite = doorClose;
        GetComponent<BoxCollider2D>().isTrigger = false;
        StartCoroutine(OpenDoor());
    }
}
