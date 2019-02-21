using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for controlling the behavior of the enemy
public class EnemyController : MonoBehaviour
{
    public GameManager gm;
    public GameObject enemyBase;
    public GameObject explosion;
    public GameObject miniExplosion;
    public Transform laserSpawn;
    public GameObject laser;
    public AudioSource hitSound;
    public CameraShake camShake;
    public float enemyHealth = 150f;
    public float shootCooldown = 1.5f;
    public int enemyScore = 500;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        StartCoroutine(EnemyShoot(0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Checks to see if enemy has hit something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy is hit by a laser
        if (collision.gameObject.tag == "Laser")
        {
            //Decraease health and spawn a mini explosion
            enemyHealth -= collision.gameObject.GetComponent<Laser>().laserDamage;
            Instantiate(miniExplosion, transform.position, transform.rotation);

            if (hitSound.enabled == true)
                hitSound.Play();

            //Destroy laser that hit this enemy
            Destroy(collision.gameObject);

            //Increment score and create visual effects when enemy is destroyed
            if(enemyHealth <= 0)
            {
                gm.totalScore += enemyScore;
                gm.UpdateScore();
                camShake.power = 0.2f;
                camShake.shouldShake = true;
                gm.enemyDeathSound.Play();
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(enemyBase);
            }
        }

        //Destroy enemy if it reaches the end of the screen
        if(collision.gameObject.tag == "DestroyZone")
        {
            Destroy(enemyBase);
        }

        //Destroy enemy if it touches the player
        if (collision.gameObject.tag == "Player")
        {
            gm.enemyDeathSound.Play();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(enemyBase);
        }
    }

    //Shoot a laser from the enemy 
    IEnumerator EnemyShoot(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(laser, laserSpawn.position, transform.rotation);
        StartCoroutine(EnemyShoot(shootCooldown));
    }



}
