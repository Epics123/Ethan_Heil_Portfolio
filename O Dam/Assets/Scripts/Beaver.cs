using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaver : MonoBehaviour
{
    public Tile startingTile;
    public float movementSpeed;
    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChompChomp()
    {
        moving = true;
        StartCoroutine(EatDams());
        
    }

    IEnumerator EatDams()
    {
        Tile currentTile = startingTile;


        while (currentTile != null)
        {
            transform.position = currentTile.transform.position;

            if (currentTile.occupied)
            {
                currentTile.DestroyDam();
            }
            if (currentTile.GetComponent<Tile>().top != null)
                currentTile = currentTile.top.GetComponent<Tile>();
            else
                break;
            yield return new WaitForSeconds(movementSpeed);
        }

        moving = false;
        Destroy(gameObject);
        
    }
}
