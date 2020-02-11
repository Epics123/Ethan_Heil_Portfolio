using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    public int rows;
    public int cols;
    public float tileSize = 1;
    public Vector2 gridLocation;
    public GameObject baseTile;
    public Tile currentTile;

    public int damType = 0;
    public bool forwardSlash, backwardSlash, flat;



    public GameObject goldPrefab;
    public Vector2 goldOffset;
    //public GameObject goldSpawnOne, goldSpawnTwo;


    public Player currentPlayer;


    private void Awake()
    {
       
        GenerateGrid();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        forwardSlash = false;
        backwardSlash = false;
        flat = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(currentPlayer != null)
        //{
            if (Input.mouseScrollDelta.y > 0.15f)
            {
                if (damType < 2)
                {
                    damType++;
                    currentTile.ResetTiles();
                }


                else
                {
                    damType = 0;
                    currentTile.ResetTiles();
                }

            }
            if (Input.mouseScrollDelta.y < -0.15f)
            {
                if (damType > 0)
                {
                    damType--;
                    currentTile.ResetTiles();
                }

                else
                {
                    damType = 2;
                    currentTile.ResetTiles();
                }

            }



            if (damType == 0)
            {
                SetFlat();
            }


            if (damType == 1)
            {
                SetBackwardSlash();

            }


            if (damType == 2)
            {
                SetForwardSlash();

            }
       // }
        
        

    }

    private void GenerateGrid()
    {

        //arrayMap = new Tile[cols, rows];
        int i, j;
        for(i = 0; i < cols; i++)
        {
            for(j = 0; j < rows; j++)
            {
                GameObject tile = Instantiate(baseTile, transform);
                tile.GetComponent<Tile>().SetTileNum(i, j);
                tile.gameObject.name = "" + i + " " + j;
                Vector3 scale;
                scale.x = tileSize;
                scale.y = tileSize;
                scale.z = tileSize;
                
                tile.transform.localScale = scale;


                float posX = i * (0.1253f) * tileSize + gridLocation.x;
                float posY = j * (0.1253f) * -tileSize + gridLocation.y;
                tile.transform.position = new Vector2(posX, posY);

                //arrayMap[i, j] = tile.GetComponent<Tile>();
            }
        }

    }

    public void SetForwardSlash()
    {
        
        backwardSlash = false;
        flat = false;
        forwardSlash = true;
    }

    public void SetBackwardSlash()
    {
      
        forwardSlash = false;
        flat = false;
        backwardSlash = true;
    }


    public void SetFlat()
    {
        
        backwardSlash = false;
        forwardSlash = false;
        flat = true;

    }

   

    public bool CheckForFlood(GameObject tile)
    {
        

        int i;
        for(i = 0; i < rows - 2; i++)
        {
            GameObject lastTile = CheckForConnection(tile);
            if (lastTile.GetComponent<Tile>().tileNum.x == cols - 1)
            {
                Debug.Log("True");
                return true;
            }
            else
            {
                tile = tile.GetComponent<Tile>().bottom;
            }

        }

        

        Debug.Log("False");
        return false;
    }

    private GameObject CheckForConnection(GameObject rootTile)
    {
        rootTile.GetComponent<SpriteRenderer>().color = Color.red;
        rootTile.GetComponent<Tile>().tested = true;

        if (rootTile.GetComponent<Tile>().tileNum.x == cols -1)
        {
            rootTile.GetComponent<Tile>().tested = false;
            return rootTile;
        }



        GameObject top = rootTile.GetComponent<Tile>().top,
                    right = rootTile.GetComponent<Tile>().right,
                    rightTop = rootTile.GetComponent<Tile>().rightTop,
                    rightBot = rootTile.GetComponent<Tile>().rightBot,
                    bot = rootTile.GetComponent<Tile>().bottom,
                    left = rootTile.GetComponent<Tile>().left,
                    leftTop = rootTile.GetComponent<Tile>().leftTop,
                    leftBot = rootTile.GetComponent<Tile>().leftBot,
                    next = rootTile;

        GameObject[] tileList = {right, rightTop, rightBot, top, bot, leftBot, leftTop, left};
      



        if (!top.GetComponent<Tile>().occupied && !right.GetComponent<Tile>().occupied &&
                !bot.GetComponent<Tile>().occupied && !right.GetComponent<Tile>().top.GetComponent<Tile>().occupied &&
                !right.GetComponent<Tile>().bottom.GetComponent<Tile>().occupied)
        {
            rootTile.GetComponent<Tile>().tested = false;
            return rootTile;
        }

       

        int i;
        for(i = 0; i < tileList.Length; i++)
        {
            if(tileList[i] != null && tileList[i].GetComponent<Tile>().occupied && next != null && next.GetComponent<Tile>().tileNum.x < cols - 1 && !tileList[i].GetComponent<Tile>().tested)
            {
                next = CheckForConnection(tileList[i]);
                Debug.Log(next.name);
            }
        }

        if (next == null)
        {
            rootTile.GetComponent<Tile>().tested = false;
            return rootTile;
        }

        rootTile.GetComponent<Tile>().tested = false;
        return next;



    }


    public void SpawnGold(GameObject tileToSpawnAt)
    {

        GameObject gold = Instantiate(goldPrefab, new Vector3(tileToSpawnAt.transform.position.x + goldOffset.x, tileToSpawnAt.transform.position.y + goldOffset.y,-1), tileToSpawnAt.transform.rotation);
        gold.GetComponent<Gold>().SetStartingTile(tileToSpawnAt);

    }


}
