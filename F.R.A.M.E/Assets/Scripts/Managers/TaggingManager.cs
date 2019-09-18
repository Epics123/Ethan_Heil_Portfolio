using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaggingManager : MonoBehaviour {

	public GameObject[] otherPlayers;
    CapsuleCollider2D capsule2D;
    public bool tagged = false;
	void Start ()
    {
        capsule2D = GetComponent<CapsuleCollider2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < otherPlayers.Length; i++)
        {
            if (capsule2D.IsTouching(otherPlayers[i].GetComponent<CapsuleCollider2D>()) && otherPlayers[i].GetComponent<TaggingManager>().tagged)
            {
                this.tagged = true;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
		
	}
}
