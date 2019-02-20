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

    public GameObject laser;
    public Transform laserSpawn;
    public CameraShake camShake;
    public float cooldown = 0.25f;
    public float rapidAngleRange = 7;

    bool canFire = true;

     public ShootingMode shootMode = ShootingMode.NORMAL;

    private void Awake()
    {
        if(camShake == null)
        {
            camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        }
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
                newLaser = Instantiate(laser, laserSpawn.position, laserSpawn.rotation);
                camShake.power = 0f;
                camShake.shouldShake = true;
                break;
            case ShootingMode.RAPID:
                cooldown = 0.1f;
                float zOffset = Random.Range(-rapidAngleRange, rapidAngleRange);
                newLaser = Instantiate(laser, laserSpawn.position, Quaternion.Euler(new Vector3(0, 0, zOffset)));
                camShake.power = 0.1f;
                camShake.shouldShake = true;
                break;
            case ShootingMode.SPREAD:
                break;
        }
        
    }

    IEnumerator ShootCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    
}
