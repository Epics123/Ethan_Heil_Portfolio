using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            enemyHealth -= collision.gameObject.GetComponent<Laser>().laserDamage;
            Instantiate(miniExplosion, transform.position, transform.rotation);

            if (hitSound.enabled == true)
                hitSound.Play();

            Destroy(collision.gameObject);
            if(enemyHealth <= 0)
            {
                camShake.power = 0.2f;
                camShake.shouldShake = true;
                gm.deathSound.Play();
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(enemyBase);
            }
        }

        if(collision.gameObject.tag == "DestroyZone")
        {
            Destroy(enemyBase);
        }
    }

    IEnumerator EnemyShoot(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(laser, laserSpawn.position, transform.rotation);
        StartCoroutine(EnemyShoot(shootCooldown));
    }


}
