using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // * Variables *
    public Transform pos;
    Vector3 start;

    public SpriteRenderer spr;
    public PlayerMovement plyerMove; // NOTE: this has to be changed if the move script is changed. Might also cause other complications
    public Rigidbody2D rgdbd;
    CircleCollider2D bxCldr;

    public GameObject explosion;

    bool dying = false;
    public float maxPause; // in seconds
    float currentTime = 0f;
    public RopeSystem ropeSystem;


    // ** Update Functions **
    private void Start()
    {
        start = pos.position;

        bxCldr = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        DramaticPause();
    }

    // **** Other Functions ****
    public void GetKilled ()
    {
        Vector3 currentPos = transform.position;
        Quaternion rot = transform.rotation;
        Instantiate(explosion, currentPos, rot);

        dying = true;
    }

    void DramaticPause()
    {
        if (dying)
        {
            currentTime += Time.deltaTime;

            if (currentTime <= maxPause)
            {
                spr.enabled = false;
                bxCldr.enabled = false;
                plyerMove.enabled = false;
                rgdbd.velocity = new Vector2(0, 0);

            }
            else
            {
                spr.enabled = true;
                bxCldr.enabled = true;
                plyerMove.enabled = true;

                pos.position = start;
                dying = false;

                currentTime = 0;
            }
            ropeSystem.ResetRope();
            pos.position = start;
        } 
    }

}
