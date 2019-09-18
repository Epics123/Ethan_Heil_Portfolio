using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAblilities : MonoBehaviour {

    public int boneCount;
    public int count = 0;

    public GameObject projectile;
    public GameObject projectileSpawn;
    public GameObject gm;

    public Animator anim;


    // Use this for initialization
    void Start () {
        boneCount = 12;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Throw", true);
            shootBones();
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Throw", false);
        }

    }


    void shootBones()
    {
        gameObject.GetComponent<PlayerFaceMouse>().faceMouse();
        gameObject.GetComponentInChildren<FaceMouse>().faceMouse();
        if (count <= 11)
        {
            boneCount--;
            count++;
            Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
        }
        
    }

    
}
