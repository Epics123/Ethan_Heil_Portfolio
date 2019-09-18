using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public GameObject[] teleportLocs;
    public Animator anim;

    float timer;

    private void Awake()
    {
        teleportLocs = GameObject.FindGameObjectsWithTag("TP");
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        timer = 0;
        int rand = Random.Range(0, teleportLocs.Length);
        transform.position = teleportLocs[rand].transform.position;
        StartCoroutine("Teleport");
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer <= 0.5)
        {
            anim.SetBool("teleporting", false);
        }
        if(timer >= 0.5)
        {
            anim.SetBool("teleporting", true);
        }
        if(timer >= 1)
        {
            timer = 0;
        }
    }

    IEnumerator Teleport()
    {
        int rand = Random.Range(0, teleportLocs.Length);
        transform.position = teleportLocs[rand].transform.position;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Teleport");
    }
}
