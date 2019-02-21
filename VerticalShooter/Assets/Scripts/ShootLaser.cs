using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum to control shooting mode
public enum ShootingMode
{
    NORMAL,
    RAPID,
    SPREAD
}

//Script to control laser shooting behavior
public class ShootLaser : MonoBehaviour
{

    public GameManager gm;
    public GameObject laser;
    public Transform laserSpawn;
    public CameraShake camShake;
    public AudioSource[] laserSounds;

    public float cooldown = 0.25f;
    public float rapidAngleRange = 7f;
    public float spreadAngle = 15f;

    bool canFire = true;

    public ShootingMode shootMode = ShootingMode.NORMAL;

    private void Awake()
    {
        if(camShake == null)
        {
            camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        }
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    //Check for player input
    void CheckInput()
    {
        if(Input.GetKey(KeyCode.Space) && canFire)
        {
            FireLaser();
            canFire = false;
            StartCoroutine(ShootCooldown(cooldown));
        }
    }

    //Shoots laser
    void FireLaser()
    {
        GameObject newLaser;
        //Check which mode to shoot in
        switch(shootMode)
        {
            //Normal mode
            case ShootingMode.NORMAL:
                laserSounds[0].Play();
                cooldown = 0.3f;

                newLaser = Instantiate(laser, laserSpawn.position, laserSpawn.rotation);

                camShake.power = 0f;
                camShake.shouldShake = true;
                break;
            //Rapid fire
            case ShootingMode.RAPID:
                laserSounds[1].Play();
                cooldown = 0.05f;

                float zOffset = Random.Range(-rapidAngleRange, rapidAngleRange);
                newLaser = Instantiate(laser, laserSpawn.position, Quaternion.Euler(new Vector3(0, 0, zOffset)));

                camShake.power = 0.1f;
                camShake.shouldShake = true;
                break;
            //Spread Shot
            case ShootingMode.SPREAD:
                laserSounds[2].Play();
                cooldown = 0.3f;

                float spread = spreadAngle;

                newLaser = Instantiate(laser, laserSpawn.position, Quaternion.Euler(new Vector3(0, 0, spread)));
                GameObject newLaser2 = Instantiate(laser, laserSpawn.position, Quaternion.Euler(new Vector3(0, 0, -spread)));
                GameObject newLaser3 = Instantiate(laser, laserSpawn.position, Quaternion.Euler(Vector3.zero));

                camShake.power = 0.1f;
                camShake.shouldShake = true;
                break;
        }
        
    }

    //Delays the next shot for the specefied amount of time
    IEnumerator ShootCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    //Returns the shooting mode to normal after the specefied amount of time
    public IEnumerator ShootModeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        shootMode = ShootingMode.NORMAL;
    }

    //Checks if player hit the shooting mode item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ShootMode")
        {
            //Change shoot mode time based on current shooting mode
            if(collision.gameObject.GetComponent<FireModePickup>().shootMode == ShootingMode.RAPID)
            {
                StartCoroutine(ShootModeTimer(collision.GetComponent<FireModePickup>().shootModeTime));
            }
            if (collision.gameObject.GetComponent<FireModePickup>().shootMode == ShootingMode.SPREAD)
            {
                StartCoroutine(ShootModeTimer(collision.GetComponent<FireModePickup>().shootModeTime * 2));
            }

        }
    }

}
