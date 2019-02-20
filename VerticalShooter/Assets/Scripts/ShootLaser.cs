using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootingMode
{
    NORMAL,
    RAPID,
    SPREAD
}

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
            camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        }
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if(Input.GetKey(KeyCode.Space) && canFire)
        {
            FireLaser();
            canFire = false;
            StartCoroutine(ShootCooldown(cooldown));
        }
    }

    void FireLaser()
    {
        GameObject newLaser;
        switch(shootMode)
        {
            case ShootingMode.NORMAL:
                for(int i = 0; i < laserSounds.Length; i++)
                {
                    laserSounds[i].enabled = false;
                }
                laserSounds[0].playOnAwake = true;
                laserSounds[0].enabled = true;
                cooldown = 0.5f;
                newLaser = Instantiate(laser, laserSpawn.position, laserSpawn.rotation);
                camShake.power = 0f;
                camShake.shouldShake = true;
                break;
            case ShootingMode.RAPID:
                for (int i = 0; i < laserSounds.Length; i++)
                {
                    laserSounds[i].enabled = false;
                }
                laserSounds[1].enabled = true;
                cooldown = 0.05f;
                float zOffset = Random.Range(-rapidAngleRange, rapidAngleRange);
                newLaser = Instantiate(laser, laserSpawn.position, Quaternion.Euler(new Vector3(0, 0, zOffset)));
                camShake.power = 0.1f;
                camShake.shouldShake = true;
                break;
            case ShootingMode.SPREAD:
                for (int i = 0; i < laserSounds.Length; i++)
                {
                    laserSounds[i].enabled = false;
                }
                laserSounds[2].enabled = true;
                cooldown = 0.5f;
                float spread = spreadAngle;
                newLaser = Instantiate(laser, laserSpawn.position, Quaternion.Euler(new Vector3(0, 0, spread)));
                GameObject newLaser2 = Instantiate(laser, laserSpawn.position, Quaternion.Euler(new Vector3(0, 0, -spread)));
                GameObject newLaser3 = Instantiate(laser, laserSpawn.position, Quaternion.Euler(Vector3.zero));
                camShake.power = 0.1f;
                camShake.shouldShake = true;
                break;
        }
        
    }

    IEnumerator ShootCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    public IEnumerator ShootModeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        shootMode = ShootingMode.NORMAL;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ShootMode")
        {
            StartCoroutine(ShootModeTimer(collision.GetComponent<FireModePickup>().shootModeTime));
        }
    }

}
