using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changes the player's firing mode when they pick up this object
public class FireModePickup : MonoBehaviour
{

    public ShootingMode shootMode;
    public int modeSelect;
    public int lifespan = 4;
    public float shootModeTime = 5f;
    public float movementSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //Randomly choose a firing mode when created
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
        StartCoroutine(DelayedDestroy(lifespan));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -movementSpeed * Time.deltaTime, 0)); //Move object down the screen
    }

    //Checks if this item hit something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<ShootLaser>().shootMode = shootMode;//Set new shooting mode  
            Destroy(gameObject);
        }
    }

    //Destroy this object after a given amount of time
    IEnumerator DelayedDestroy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
