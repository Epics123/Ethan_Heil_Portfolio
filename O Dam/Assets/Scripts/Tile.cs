using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 tileNum;
    public Color tempColor;
    public Color originalColor;
    public Color occupiedColor;
    public Color toDestroy;
    public Sprite originalSprite,tempSprite, forwardSlashSprite, backwardSlashSprite, flatSprite;
    public bool occupied, tested = false, forwardSlash, backSlash, flat, leftSide, rightSide;
    public Tile[] tilesInDam;
    public int durability = 5;

    //[HideInInspector]
    public GameObject right, rightTop, rightBot, left, leftTop, leftBot, top, bottom, gridHolder;
    TurnManager turnManager;
    GridManager gridManager1;
    


    // Start is called before the first frame update
    void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        
        GetSurroundingTiles();
        gridHolder = GameObject.Find("GridHolder");
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        tilesInDam = new Tile[3];
        gridManager1 = gridHolder.GetComponent<GridManager>();
    }

    private void OnMouseOver()
    {
        GridManager gridManager = gridHolder.GetComponent<GridManager>();

        if (gridManager.currentPlayer != null && gridManager.currentPlayer.isPlaying)
        {
            
                if (gridManager.currentPlayer.placingDam )
                {
                    

                    gridManager.currentTile = gameObject.GetComponent<Tile>();


                    //checks based on the grid size
                    if (tileNum.y > 6 && tileNum.y < gridManager.rows - 2 && tileNum.x > 2 && tileNum.x < gridManager.cols - 2)
                    {

                        //if the current tile is not occupied
                        if (!occupied)
                        {
                            //if the item placed is a forward diagonal   "/"
                            if (gridManager.forwardSlash && gridManager.currentPlayer.playerInventory.diagonalCount > 0)
                            {

                                //checks to make sure the diagonal tiles are not occupied
                                if (!rightTop.GetComponent<Tile>().occupied && !leftBot.GetComponent<Tile>().occupied && !leftBot.GetComponent<Tile>().leftBot.GetComponent<Tile>().occupied)
                                {
                                    gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                                    gameObject.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                    rightTop.GetComponent<SpriteRenderer>().color = tempColor;
                                    rightTop.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                    leftBot.GetComponent<SpriteRenderer>().color = tempColor;
                                    leftBot.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                    leftBot.GetComponent<Tile>().leftBot.GetComponent<SpriteRenderer>().color = tempColor;
                                    leftBot.GetComponent<Tile>().leftBot.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                }

                            }

                            //if the item placed is a bacward diagonal    "\"
                            else if (gridManager.backwardSlash && gridManager.currentPlayer.playerInventory.diagonalCount > 0)
                            {


                                //checks to make sure the diagonaltiles are not occupied
                                if (!rightBot.GetComponent<Tile>().occupied && !rightBot.GetComponent<Tile>().rightBot.GetComponent<Tile>().occupied && !leftTop.GetComponent<Tile>().occupied)
                                {
                                    gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                                    gameObject.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                    rightBot.GetComponent<SpriteRenderer>().color = tempColor;
                                    rightBot.GetComponent<SpriteRenderer>().sprite = tempSprite;
                                
                                    leftTop.GetComponent<SpriteRenderer>().color = tempColor;
                                    leftTop.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                    rightBot.GetComponent<Tile>().rightBot.GetComponent<SpriteRenderer>().color = tempColor;
                                    rightBot.GetComponent<Tile>().rightBot.GetComponent<SpriteRenderer>().sprite = tempSprite;
                                }

                            }

                            //checks to see if the item being placed is a flat    "---"
                            else if (gridManager.flat && gridManager.currentPlayer.playerInventory.straightCount  > 0)
                            {

                                //checks to see if the side tile
                                if (!right.GetComponent<Tile>().occupied && !left.GetComponent<Tile>().occupied && !left.GetComponent<Tile>().left.GetComponent<Tile>().occupied)
                                {
                                    gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                                    gameObject.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                    right.GetComponent<SpriteRenderer>().color = tempColor;
                                    right.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                    left.GetComponent<SpriteRenderer>().color = tempColor;
                                    left.GetComponent<SpriteRenderer>().sprite = tempSprite;

                                    left.GetComponent<Tile>().left.GetComponent<SpriteRenderer>().color = tempColor;
                                    left.GetComponent<Tile>().left.GetComponent<SpriteRenderer>().sprite = tempSprite;
                                }
                            }

                        }

                    }
                }

                if (gridHolder.GetComponent<GridManager>().currentPlayer.placingDynamite)
                {
                    if (occupied)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = toDestroy;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = toDestroy;
                        
                    }
                }



            }
        

    }

    private void OnMouseExit()
    {
        if(gridManager1.currentPlayer != null)
            ResetTiles();
            
    }

    private void OnMouseDown()
    {
       
        
        GridManager gridManager = gridHolder.GetComponent<GridManager>();

        
        
        if (gridManager.currentPlayer != null && gridManager.currentPlayer.isPlaying)
        {
            if (!gridManager.flat && !gridManager.forwardSlash && !gridManager.backwardSlash)
            {
                //gridHolder.GetComponent<GridManager>().SpawnGold(gameObject);
                //gridHolder.GetComponent<GridManager>().CheckForFlood(gameObject);
            }



            if (gridHolder.GetComponent<GridManager>().currentPlayer != null && gridHolder.GetComponent<GridManager>().currentPlayer.placingDam)
            {
                //check to make sure the current tile is not occupied
                if (!occupied)
                {

                    //if the item placed is a forward diagonal   "/"
                    if (gridManager.forwardSlash && gridManager.currentPlayer.playerInventory.diagonalCount > 0)
                    {

                        //checks to make sure the diagonal tiles are not occupied
                        if (!rightTop.GetComponent<Tile>().occupied && !leftBot.GetComponent<Tile>().occupied && !leftBot.GetComponent<Tile>().leftBot.GetComponent<Tile>().occupied)
                        {
                            PlaceForwardSlash(rightTop, leftBot, leftBot.GetComponent<Tile>().leftBot);
                            turnManager.dialogue.SwapText();
                            turnManager.dialogue.ChangeSentence(2);
                            turnManager.dialogue.canType = true;
                            StartCoroutine(turnManager.dialogue.TypeText());
                        }

                    }

                    //if the item placed is a backward diagonal   "\"
                    else if (gridManager.backwardSlash && gridManager.currentPlayer.playerInventory.diagonalCount > 0)
                    {

                        //checks to make sure the diagonal tiles are not occupied
                        if (!rightBot.GetComponent<Tile>().occupied && !rightBot.GetComponent<Tile>().rightBot.GetComponent<Tile>().occupied && !leftTop.GetComponent<Tile>().occupied)
                        {
                            PlaceBackSlash(rightBot, rightBot.GetComponent<Tile>().rightBot, leftTop);
                            turnManager.dialogue.SwapText();
                            turnManager.dialogue.ChangeSentence(2);
                            turnManager.dialogue.canType = true;
                            StartCoroutine(turnManager.dialogue.TypeText());
                        }

                    }

                    //checks to see if the item being placed is a flat
                    if (gridManager.flat && gridManager.currentPlayer.playerInventory.straightCount > 0)
                    {

                        //checks to see if the side tiles are occupied
                        if (!right.GetComponent<Tile>().occupied && !left.GetComponent<Tile>().occupied && !left.GetComponent<Tile>().left.GetComponent<Tile>().occupied)
                        {
                            PlaceFlat(right, left, left.GetComponent<Tile>().left);
                            turnManager.dialogue.SwapText();
                            turnManager.dialogue.ChangeSentence(2);
                            turnManager.dialogue.canType = true;
                            StartCoroutine(turnManager.dialogue.TypeText());
                        }
                    }


                }
            }

            if (gridHolder.GetComponent<GridManager>().currentPlayer != null && gridHolder.GetComponent<GridManager>().currentPlayer.placingDynamite)
            {
                DestroyDam();
            }
        }

        
        

    }

    public void SetTileNum(int x, int y)
    {
        tileNum = new Vector2(x, y);
    }

    private void GetSurroundingTiles()
    {
        if(GameObject.Find("" + (tileNum.x - 1) + " " + (tileNum.y)) != null)
        {
            left = GameObject.Find("" + (tileNum.x - 1) + " " + (tileNum.y));
        }

        if (GameObject.Find("" + (tileNum.x - 1) + " " + (tileNum.y - 1)) != null)
        {
            leftTop = GameObject.Find("" + (tileNum.x - 1) + " " + (tileNum.y - 1));
        }

        if (GameObject.Find("" + (tileNum.x - 1) + " " + (tileNum.y + 1)) != null)
        {
            leftBot = GameObject.Find("" + (tileNum.x - 1) + " " + (tileNum.y + 1));
        }

        if (GameObject.Find("" + (tileNum.x + 1) + " " + (tileNum.y)) != null)
        {
            right = GameObject.Find("" + (tileNum.x + 1) + " " + (tileNum.y));
        }

        if (GameObject.Find("" + (tileNum.x + 1) + " " + (tileNum.y - 1)) != null)
        {
            rightTop = GameObject.Find("" + (tileNum.x + 1) + " " + (tileNum.y - 1));
        }

        if (GameObject.Find("" + (tileNum.x + 1) + " " + (tileNum.y + 1)) != null)
        {
            rightBot = GameObject.Find("" + (tileNum.x + 1) + " " + (tileNum.y + 1));
        }

        if (GameObject.Find("" + (tileNum.x) + " " + (tileNum.y + 1)) != null)
        {
            bottom = GameObject.Find("" + (tileNum.x ) + " " + (tileNum.y + 1));
        }

        if (GameObject.Find("" + (tileNum.x) + " " + (tileNum.y - 1)) != null)
        {
            top = GameObject.Find("" + (tileNum.x) + " " + (tileNum.y - 1));
        }
    }

    public void ResetTiles()
    {
        if (occupied)
        {
            gameObject.GetComponent<SpriteRenderer>().color = occupiedColor;
        }

        if (!occupied)
        {
            gameObject.GetComponent<SpriteRenderer>().color = originalColor;
            gameObject.GetComponent<SpriteRenderer>().sprite = originalSprite;
        }


        //checks to see if the right tile is not null
        if (right != null)
        {

            //checks to make sure the right tile is not occupied
            if (!right.GetComponent<Tile>().occupied)
            {
                right.GetComponent<SpriteRenderer>().color = originalColor;
                right.GetComponent<SpriteRenderer>().sprite = originalSprite;
            }




            //checks to see if the right bottom diagonal is not null
            if (rightBot != null)
            {
                //checks to make sure its not occupied
                if (!rightBot.GetComponent<Tile>().occupied)
                {
                    rightBot.GetComponent<SpriteRenderer>().color = originalColor;
                    rightBot.GetComponent<SpriteRenderer>().sprite = originalSprite;
                }

            }


            // checks to see if the right top diagonal is not null
            if (rightTop != null)
            {
                //checks to make sure its not occupied
                if (!rightTop.GetComponent<Tile>().occupied)
                {
                    rightTop.GetComponent<SpriteRenderer>().color = originalColor;
                    rightTop.GetComponent<SpriteRenderer>().sprite = originalSprite;
                }

            }


            if (tileNum.x < gridHolder.GetComponent<GridManager>().cols - 2)
            {
                // checks to see if the right top 2nd  diagonal is not null
                if (rightBot.GetComponent<Tile>().rightBot != null)
                {
                    //checks to make sure its not occupied
                    if (!rightBot.GetComponent<Tile>().rightBot.GetComponent<Tile>().occupied)
                    {
                        rightBot.GetComponent<Tile>().rightBot.GetComponent<SpriteRenderer>().color = originalColor;
                        rightBot.GetComponent<Tile>().rightBot.GetComponent<SpriteRenderer>().sprite = originalSprite;
                    }

                }
            }


        }


        //checks to see if the left tile is not null
        if (left != null)
        {


            //checks to see if the left tile is not occupied
            if (!left.GetComponent<Tile>().occupied)
            {
                left.GetComponent<SpriteRenderer>().color = originalColor;
                left.GetComponent<SpriteRenderer>().sprite = originalSprite;
            }




            //checks to see if the left bottom diagonal is not null
            if (leftBot != null)
            {

                //checks to see if it is not occupied
                if (!leftBot.GetComponent<Tile>().occupied)
                {
                    leftBot.GetComponent<SpriteRenderer>().color = originalColor;
                    leftBot.GetComponent<SpriteRenderer>().sprite = originalSprite;
                }

            }

            //checks to see if the left top tile is not null
            if (leftTop != null)
            {

                //checks to see if it is not occupied
                if (!leftTop.GetComponent<Tile>().occupied)
                {
                    leftTop.GetComponent<SpriteRenderer>().color = originalColor;
                    leftTop.GetComponent<SpriteRenderer>().sprite = originalSprite;
                }

            }

            if (tileNum.x > 1)
            {
                // checks to see if the left bot 2nd diagonal is not null
                if (leftBot.GetComponent<Tile>().leftBot != null)
                {
                    //checks to make sure its not occupied
                    if (!leftBot.GetComponent<Tile>().leftBot.GetComponent<Tile>().occupied)
                    {
                        leftBot.GetComponent<Tile>().leftBot.GetComponent<SpriteRenderer>().color = originalColor;
                        leftBot.GetComponent<Tile>().leftBot.GetComponent<SpriteRenderer>().sprite = originalSprite;
                    }

                }

                if (left.GetComponent<Tile>().left != null)
                {
                    //checks to see if the 2nd left tile is not occupied
                    if (!left.GetComponent<Tile>().left.GetComponent<Tile>().occupied)
                    {
                        left.GetComponent<Tile>().left.GetComponent<SpriteRenderer>().color = originalColor;
                        left.GetComponent<Tile>().left.GetComponent<SpriteRenderer>().sprite = originalSprite;
                    }

                }

            }

        }

        if (top != null && !top.GetComponent<Tile>().occupied)
        {
            top.GetComponent<SpriteRenderer>().color = originalColor;
            top.GetComponent<SpriteRenderer>().sprite = originalSprite;
        }
        if (bottom != null && !bottom.GetComponent<Tile>().occupied)
        {
            bottom.GetComponent<SpriteRenderer>().color = originalColor;
            bottom.GetComponent<SpriteRenderer>().sprite = originalSprite;
        }
        
    }

    public void DestroyDam()
    {
      

        int i;
        for(i = 0; i < tilesInDam.Length; i++)
        {
            tilesInDam[i].gameObject.GetComponent<SpriteRenderer>().color = originalColor;
            tilesInDam[i].occupied = false;
            tilesInDam[i].tilesInDam = new Tile[3];
        }

        occupied = false;
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        tilesInDam = new Tile[3];

        GridManager gridManager = gridHolder.GetComponent<GridManager>();
        gridManager.currentPlayer.placingDynamite = false;
        gridManager.currentPlayer.playerInventory.dynamiteCount--;
        turnManager.turnNumber++;
    }

    public void DecrementDurability()
    {
        durability--;
        if (durability == 0)
        {
            DestroyDam();
        }            
        else
        {
            int i;
            for (i = 0; i < tilesInDam.Length; i++)
            {
                tilesInDam[i].GetComponent<Tile>().durability = durability;
            }
        }
            
    }


    private void PlaceForwardSlash( GameObject rightTop, GameObject leftBot, GameObject leftBot2)
    {

        GridManager gridManager = gridHolder.GetComponent<GridManager>();
        occupied = true;
        forwardSlash = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = forwardSlashSprite;
        gameObject.GetComponent<SpriteRenderer>().color = occupiedColor;
        tilesInDam[0] = rightTop.GetComponent<Tile>();
        tilesInDam[1] = leftBot.GetComponent<Tile>();
        tilesInDam[2] = leftBot2.GetComponent<Tile>();


        /*****************************************/
        rightTop.GetComponent<Tile>().occupied = true;
        rightTop.GetComponent<Tile>().forwardSlash = true;
        rightTop.GetComponent<SpriteRenderer>().sprite = forwardSlashSprite;
        rightTop.GetComponent<SpriteRenderer>().color = occupiedColor;
        rightTop.GetComponent<Tile>().tilesInDam[0] = this;
        rightTop.GetComponent<Tile>().tilesInDam[1] = leftBot.GetComponent<Tile>();
        rightTop.GetComponent<Tile>().tilesInDam[2] = leftBot2.GetComponent<Tile>();



        /*****************************************/
        leftBot.GetComponent<Tile>().occupied = true;
        leftBot.GetComponent<Tile>().forwardSlash = true;
        leftBot.GetComponent<SpriteRenderer>().sprite = forwardSlashSprite;
        leftBot.GetComponent<SpriteRenderer>().color = occupiedColor;
        leftBot.GetComponent<Tile>().tilesInDam[0] = rightTop.GetComponent<Tile>();
        leftBot.GetComponent<Tile>().tilesInDam[1] = this;
        leftBot.GetComponent<Tile>().tilesInDam[2] = leftBot2.GetComponent<Tile>();




        /*****************************************/
        leftBot2.GetComponent<Tile>().occupied = true;
        leftBot2.GetComponent<Tile>().forwardSlash = true;
        leftBot2.GetComponent<SpriteRenderer>().sprite = forwardSlashSprite;
        leftBot2.GetComponent<SpriteRenderer>().color = occupiedColor;
        leftBot2.GetComponent<Tile>().tilesInDam[0] = rightTop.GetComponent<Tile>();
        leftBot2.GetComponent<Tile>().tilesInDam[1] = leftBot.GetComponent<Tile>();
        leftBot2.GetComponent<Tile>().tilesInDam[2] = this;


        gridManager.currentPlayer.placingDam = false;
        gridManager.currentPlayer.playerInventory.diagonalCount--;
        turnManager.turnNumber++;
    }

    private void PlaceBackSlash( GameObject rightBot, GameObject rightBot2, GameObject leftTop)
    {
        GridManager gridManager = gridHolder.GetComponent<GridManager>();
        occupied = true;
        backSlash = true;
        gameObject.GetComponent<SpriteRenderer>().color = occupiedColor;
        gameObject.GetComponent<SpriteRenderer>().sprite = backwardSlashSprite;
        tilesInDam[0] = rightBot.GetComponent<Tile>();
        tilesInDam[1] = rightBot2.GetComponent<Tile>();
        tilesInDam[2] = leftTop.GetComponent<Tile>();


        /****************************************************/
        rightBot.GetComponent<Tile>().occupied = true;
        rightBot.GetComponent<Tile>().backSlash = true;
        rightBot.GetComponent<SpriteRenderer>().color = occupiedColor;
        rightBot.GetComponent<SpriteRenderer>().sprite = backwardSlashSprite;
        rightBot.GetComponent<Tile>().tilesInDam[0] = this;
        rightBot.GetComponent<Tile>().tilesInDam[1] = rightBot2.GetComponent<Tile>();
        rightBot.GetComponent<Tile>().tilesInDam[2] = leftTop.GetComponent<Tile>();


        /*****************************************************/
        rightBot2.GetComponent<Tile>().occupied = true;
        rightBot2.GetComponent<Tile>().backSlash = true;
        rightBot2.GetComponent<SpriteRenderer>().color = occupiedColor;
        rightBot2.GetComponent<SpriteRenderer>().sprite = backwardSlashSprite;
        rightBot2.GetComponent<Tile>().tilesInDam[0] = rightBot.GetComponent<Tile>();
        rightBot2.GetComponent<Tile>().tilesInDam[1] = this;
        rightBot2.GetComponent<Tile>().tilesInDam[2] = leftTop.GetComponent<Tile>();

        /******************************************************/
        leftTop.GetComponent<Tile>().occupied = true;
        leftTop.GetComponent<Tile>().backSlash = true;
        leftTop.GetComponent<SpriteRenderer>().color = occupiedColor;
        leftTop.GetComponent<SpriteRenderer>().sprite = backwardSlashSprite;
        leftTop.GetComponent<Tile>().tilesInDam[0] = rightBot.GetComponent<Tile>();
        leftTop.GetComponent<Tile>().tilesInDam[1] = rightBot2.GetComponent<Tile>();
        leftTop.GetComponent<Tile>().tilesInDam[2] = this;


        gridManager.currentPlayer.placingDam = false;
        gridManager.currentPlayer.playerInventory.diagonalCount--;
        turnManager.turnNumber++;
    }

    private void PlaceFlat( GameObject right, GameObject left, GameObject left2)
    {

        GridManager gridManager = gridHolder.GetComponent<GridManager>();
        occupied = true;
        flat = true;
        rightSide = true;
        gameObject.GetComponent<SpriteRenderer>().color = occupiedColor;
        gameObject.GetComponent<SpriteRenderer>().sprite = flatSprite;
        tilesInDam[0] = right.GetComponent<Tile>();
        tilesInDam[1] = left.GetComponent<Tile>();
        tilesInDam[2] = left2.GetComponent<Tile>();


        /*************************************************************/
        right.GetComponent<Tile>().occupied = true;
        right.GetComponent<Tile>().flat = true;
        right.GetComponent<Tile>().rightSide = true;
        right.GetComponent<SpriteRenderer>().color = occupiedColor;
        right.GetComponent<SpriteRenderer>().sprite = flatSprite;
        right.GetComponent<Tile>().tilesInDam[0] = this;
        right.GetComponent<Tile>().tilesInDam[1] = left.GetComponent<Tile>();
        right.GetComponent<Tile>().tilesInDam[2] = left.GetComponent<Tile>().left.GetComponent<Tile>();

        /***************************************************************/
        left.GetComponent<Tile>().occupied = true;
        left.GetComponent<Tile>().flat = true;
        left.GetComponent<Tile>().leftSide = true;
        left.GetComponent<SpriteRenderer>().color = occupiedColor;
        left.GetComponent<SpriteRenderer>().sprite = flatSprite;
        left.GetComponent<Tile>().tilesInDam[0] = right.GetComponent<Tile>();
        left.GetComponent<Tile>().tilesInDam[1] = this;
        left.GetComponent<Tile>().tilesInDam[2] = left.GetComponent<Tile>().left.GetComponent<Tile>();


        /***************************************************************/
        left2.GetComponent<Tile>().occupied = true;
        left2.GetComponent<Tile>().flat = true;
        left2.GetComponent<Tile>().leftSide = true;
        left2.GetComponent<SpriteRenderer>().color = occupiedColor;
        left2.GetComponent<SpriteRenderer>().sprite = flatSprite;
        left2.GetComponent<Tile>().tilesInDam[0] = right.GetComponent<Tile>();
        left2.GetComponent<Tile>().tilesInDam[1] = left.GetComponent<Tile>();
        left2.GetComponent<Tile>().tilesInDam[2] = this;

        gridManager.currentPlayer.placingDam = false;
        gridManager.currentPlayer.playerInventory.straightCount--;
        turnManager.turnNumber++;
    }

  
}
