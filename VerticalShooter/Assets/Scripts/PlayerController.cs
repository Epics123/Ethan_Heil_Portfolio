using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameManager gm;
    public CameraShake camShake;
    public AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyLaser")
        {
            hitSound.Play();
            gm.LooseLives();
            camShake.power = 0.2f;
            camShake.shouldShake = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            hitSound.Play();
            gm.LooseLives();
            camShake.power = 0.4f;
            camShake.shouldShake = true;
        }
    }
}
