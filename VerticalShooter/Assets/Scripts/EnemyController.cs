using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameManager gm;
    public AudioSource hitSound;
    public CameraShake camShake;
    public float enemyHealth = 150f;
    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            enemyHealth -= collision.gameObject.GetComponent<Laser>().laserDamage;
            hitSound.Play();
            Destroy(collision.gameObject);
            if(enemyHealth <= 0)
            {
                camShake.power = 0.2f;
                camShake.shouldShake = true;
                gm.deathSound.Play();
                Destroy(gameObject);
            }
        }

        if(collision.gameObject.tag == "DestroyZone")
        {
            Destroy(gameObject);
        }
    }


}
