using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModePickup : MonoBehaviour
{

    public ShootingMode shootMode;
    public int modeSelect;
    public float shootModeTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        modeSelect = Random.Range(0, 2);
        switch (modeSelect)
        {
            case 0:
                shootMode = ShootingMode.RAPID;
                break;
            case 1:
                shootMode = ShootingMode.SPREAD;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<ShootLaser>().shootMode = shootMode;   
        }
        Destroy(gameObject);
    }

}
