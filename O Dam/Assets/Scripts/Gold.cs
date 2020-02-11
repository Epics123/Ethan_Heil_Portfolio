using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public GameObject currentTile;
    public int tilesToMove;
    public bool moving;
    public float movementDelay;
    public TurnManager turnManager;

    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            Move();
    }
    

    public void SetStartingTile(GameObject startingTile)
    {

        currentTile = startingTile;

    }

    public void Move()
    {

        StartCoroutine(MoveGold());
    }

    IEnumerator MoveGold()
    {


        moving = false;
       


        int i;
        for(i = 0; i < tilesToMove; i++)
        {

            GameObject topTile = currentTile.GetComponent<Tile>().top,
                       topRightTile = currentTile.GetComponent<Tile>().rightTop,
                       rightTile = currentTile.GetComponent<Tile>().right,
                       bottomRightTile = currentTile.GetComponent<Tile>().rightBot,
                       bottomTile = currentTile.GetComponent<Tile>().bottom,
                       bottomLeftTile = currentTile.GetComponent<Tile>().leftBot,
                       leftTile = currentTile.GetComponent<Tile>().left,
                       leftTopTile = currentTile.GetComponent<Tile>().leftTop;



            

            if (bottomTile != null && !bottomTile.GetComponent<Tile>().occupied)
            {
                transform.position = bottomTile.transform.position;
                currentTile = bottomTile;
            }

            
            
            else if (bottomTile.GetComponent<Tile>().occupied && bottomTile.GetComponent<Tile>().flat)
            {
                if (bottomTile.GetComponent<Tile>().leftSide)
                {
                    bottomTile.GetComponent<Tile>().DecrementDurability();
                    
                    transform.position = leftTile.transform.position;
                    currentTile = leftTile;
                }
                else
                {
                    transform.position = rightTile.transform.position;
                    currentTile = rightTile;
                    bottomTile.GetComponent<Tile>().DecrementDurability();
                }
            }

            else if (bottomTile.GetComponent<Tile>().backSlash)
            {
                if (!rightTile.GetComponent<Tile>().occupied)
                {
                    while (i < tilesToMove)
                    {
                        if (rightTile.GetComponent<Tile>().occupied)
                        {
                            if (!bottomTile.GetComponent<Tile>().occupied)
                            {
                                transform.position = bottomTile.transform.position;
                                currentTile = bottomTile;
                                i++;
                                rightTile.GetComponent<Tile>().DecrementDurability();
                            }
                            else
                            {
                                Sink();
                            }
                        }
                        else
                        {
                            transform.position = rightTile.transform.position;
                            currentTile = rightTile;
                            i++;
                        }
                        

                        topTile = currentTile.GetComponent<Tile>().top;
                        topRightTile = currentTile.GetComponent<Tile>().rightTop;
                        rightTile = currentTile.GetComponent<Tile>().right;
                        bottomRightTile = currentTile.GetComponent<Tile>().rightBot;
                        bottomTile = currentTile.GetComponent<Tile>().bottom;
                        bottomLeftTile = currentTile.GetComponent<Tile>().leftBot;
                        leftTile = currentTile.GetComponent<Tile>().left;
                        leftTopTile = currentTile.GetComponent<Tile>().leftTop;

                        yield return new WaitForSeconds(movementDelay);
                        
                        
                    }
                    bottomTile.GetComponent<Tile>().DecrementDurability();
                }
                else
                {
                    if (!leftTile.GetComponent<Tile>().occupied)
                    {
                        transform.position = leftTile.transform.position;
                        currentTile = leftTile;
                        bottomTile.GetComponent<Tile>().DecrementDurability();
                    }
                }
                
                
            }

            else  if (bottomTile.GetComponent<Tile>().forwardSlash)
            {
                if(!leftTile.GetComponent<Tile>().occupied)
                {
                    while (i < tilesToMove)
                    {
                        if (leftTile.GetComponent<Tile>().occupied)
                        {
                            if (!bottomTile.GetComponent<Tile>().occupied)
                            {
                                transform.position = bottomTile.transform.position;
                                currentTile = bottomTile;
                                i++;
                                leftTile.GetComponent<Tile>().DecrementDurability();
                            }
                            else
                                Sink();
                        }
                        else
                        {
                            transform.position = leftTile.transform.position;
                            currentTile = leftTile;
                            i++;
                        }


                        topTile = currentTile.GetComponent<Tile>().top;
                        topRightTile = currentTile.GetComponent<Tile>().rightTop;
                        rightTile = currentTile.GetComponent<Tile>().right;
                        bottomRightTile = currentTile.GetComponent<Tile>().rightBot;
                        bottomTile = currentTile.GetComponent<Tile>().bottom;
                        bottomLeftTile = currentTile.GetComponent<Tile>().leftBot;
                        leftTile = currentTile.GetComponent<Tile>().left;
                        leftTopTile = currentTile.GetComponent<Tile>().leftTop;


                        yield return new WaitForSeconds(movementDelay);
                        

                    }
                    bottomTile.GetComponent<Tile>().DecrementDurability();
                }
                else
                {
                    if (!rightTile.GetComponent<Tile>().occupied)
                    {
                        transform.position = rightTile.transform.position;
                        currentTile = rightTile;
                        bottomTile.GetComponent<Tile>().DecrementDurability();
                    }
                }


            }

            yield return new WaitForSeconds(movementDelay);

        }

        
    }

    private void Sink()
    {
        Destroy(gameObject);
    }


}
