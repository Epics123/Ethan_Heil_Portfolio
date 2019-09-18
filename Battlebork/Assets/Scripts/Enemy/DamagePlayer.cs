using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public int livesLost = 1;
    public int numBonesDropped = 1;

    public GameObject boneAmmo;

    bool hit = false;

    //Handle Camera Shake
    public float camShakeAmt = 0.05f;
    public float camShakeLength = 0.1f;
    CameraShake camShake;


    // Use this for initialization
    void Start () {
        camShake = GameManager.gm.GetComponent<CameraShake>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player" && hit != true)
        {

            camShake.Shake(camShakeAmt, camShakeLength);
            
            GameManager.gm.GetComponent<GameManager>().loseLives(livesLost);
            hit = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        for(int i = 0; i < numBonesDropped; i++)
        {
            Instantiate(boneAmmo, transform.position,  Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0,360)));
        }   
    }

}
