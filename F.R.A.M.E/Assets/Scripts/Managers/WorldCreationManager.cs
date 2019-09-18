using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreationManager : MonoBehaviour
{
    List<string[,]> gameBoardsList = new List<string[,]>();
    string[][,] gameBoards = new string[][,]
    {
        ///*
        //board 1
        new string[,]
        {
            {"A", "A", "A", "A"},
            {"A", "S", "S", "S"},
            {"A", "DV", "A", "A"},
            {"A", "S", "S", "A"},
            {"A", "S", "A", "A"},
            {"A", "S", "A", "S"},
            {"A", "S", "A", "S"},
            {"A", "S", "A", "S"},
            {"A", "S", "A", "W"}
        },
		//board 2
		new string[,]
        {
            {"A", "A", "A", "A"},
            {"A", "S", "A", "S"},
            {"A", "S", "S", "S"},
            {"A", "S", "W", "S"},
            {"A", "A", "A", "A"},
            {"A", "S", "A", "S"},
            {"A", "DV", "A", "A"},
            {"A", "S", "A", "S"},
            {"A", "A", "A", "A"}
        },
		//board 3
		new string[,]
        {
            {"A", "S", "S", "A"},
            {"A", "A", "S", "A"},
            {"A", "S", "S", "A"},
            {"A", "A", "DV", "A"},
            {"A", "S", "S", "A"},
            {"A", "A", "A", "A"},
            {"A", "S", "S", "A"},
            {"A", "S", "W", "A"},
            {"A", "S", "S", "A"}
        },
		//board 4
        new string[,]
        {
            {"A", "A", "A", "A"},
            {"A", "S", "A", "S"},
            {"A", "A", "W", "S"},
            {"A", "S", "S", "S"},
            {"A", "A", "DV", "A"},
            {"S", "S", "S", "A"},
            {"A", "S", "A", "A"},
            {"A", "S", "A", "A"},
            {"A", "S", "A", "A"}
        },
        //board 5
        new string[,]
        {
            {"A", "A", "A", "A"},
            {"A", "S", "S", "S"},
            {"A", "A", "S", "A"},
            {"S", "A", "A", "A"},
            {"A", "A", "S", "A"},
            {"S", "DH", "S", "A"},
            {"S", "A", "S", "A"},
            {"S", "A", "W", "S"},
            {"S", "A", "A", "S"}
        },
		//board 6
        new string[,]
        {
            {"A", "A", "A", "A"},
            {"S", "S", "S", "A"},
            {"A", "A", "A", "A"},
            {"A", "S", "S", "S"},
            {"A", "DV", "A", "A"},
            {"S", "S", "S", "W"},
            {"A", "A", "A", "A"},
            {"A", "S", "S", "S"},
            {"A", "A", "A", "A"}
        },
		//board 7
        new string[,]
        {
            {"A", "A", "A", "A"},
            {"W", "S", "S", "S"},
            {"A", "A", "A", "A"},
            {"S", "S", "S", "A"},
            {"A", "A", "A", "A"},
            {"A", "S", "S", "S"},
            {"A", "A", "DV", "A"},
            {"S", "S", "S", "A"},
            {"A", "A", "A", "A"}
        },
		//board 8
        new string[,]
        {
            {"A", "W", "A", "A"},
            {"S", "S", "S", "S"},
            {"A", "S", "A", "A"},
            {"A", "DV", "A", "A"},
            {"A", "S", "S", "A"},
            {"A", "A", "A", "A"},
            {"S", "S", "S", "A"},
            {"S", "A", "A", "A"},
            {"S", "S", "S", "A"}
        },
		//board 9
        new string[,]
        {
            {"A", "A", "A", "A"},
            {"S", "S", "S", "A"},
            {"A", "A", "S", "A"},
            {"A", "A", "DV", "A"},
            {"S", "A", "S", "S"},
            {"A", "A", "A", "A"},
            {"S", "A", "A", "A"},
            {"A", "S", "S", "A"},
            {"A", "A", "W", "S"}
        },
        //board 10
        new string[,]
        {
            {"A", "A", "A", "A"},
            {"A", "A", "A", "A"},
            {"A", "S", "S", "A"},
            {"A", "S", "S", "A"},
            {"A", "S", "S", "A"},
            {"A", "S", "S", "A"},
            {"A", "S", "S", "A"},
            {"A", "S", "S", "A"},
            {"W", "S", "S", "A"}
        },
        //board 11
        new string[,]
        {
            {"A", "A", "S", "W"},
            {"S", "A", "S", "A"},
            {"A", "A", "S", "A"},
            {"DH", "S", "S", "S"},
            {"A", "A", "A", "A"},
            {"S", "S", "S", "A"},
            {"A", "S", "A", "A"},
            {"A", "S", "A", "S"},
            {"A", "S", "A", "A"}
        },
        //board 12
        new string[,]
        {
            {"A", "A", "A", "A"},
            {"S", "S", "W", "S"},
            {"A", "A", "A", "A"},
            {"DH", "S", "S", "A"},
            {"A", "A", "A", "A"},
            {"S", "A", "S", "S"},
            {"A", "A", "A", "A"},
            {"A", "S", "S", "A"},
            {"A", "A", "A", "A"}
        },
        //*/
        /*
        //board 13
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"S" ,"S" , "S", "S"},
            {"S" ,"S" , "S", "A"},
            {"S" ,"S" , "A", "A"},
            {"S" ,"A" , "A", "A"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"A" , "S", "S"},
            {"A" ,"S" , "S", "S"},
            {"S" ,"S" , "S", "S"},
        },
        //board 14
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"S" ,"S" , "S", "S"},
            {"S" ,"S" , "S", "S"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"A" , "A", "A"},
            {"S" ,"S" , "S", "S"},
            {"S" ,"S" , "S", "S"},
            {"S" ,"S" , "S", "S"},
        },
        //board 15
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"A" , "S", "A"},
            {"A" ,"S" , "A", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"S" ,"S" , "S", "S"},
        },
        //board 16
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"S" ,"A" , "A", "S"},
            {"S" ,"A" , "A", "S"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"A" , "A", "A"},
            {"S" ,"A" , "A", "S"},
            {"S" ,"A" , "A", "S"},
            {"S" ,"S" , "S", "S"},
        },
        //board 17
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"S" ,"A" , "A", "S"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"A" , "A", "A"},
            {"S" ,"A" , "A", "S"},
            {"S" ,"S" , "S", "S"},
        },
        //board 18
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"A" ,"A" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "A", "A"},
            {"A" ,"A" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "A", "A"},
            {"S" ,"S" , "S", "S"},
        },
        //board 19
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"S" ,"S" , "A", "A"},
            {"S" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"A" , "S", "A"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"S" , "A", "A"},
            {"A" ,"S" , "A", "A"},
            {"S" ,"S" , "S", "S"},
        },
        //board 20
        new string[,]{
            {"S" ,"S" , "S", "S"},
            {"S" ,"A" , "A", "A"},
            {"A" ,"A" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "S", "S"},
            {"A" ,"S" , "A", "A"},
            {"A" ,"A" , "A", "A"},
            {"S" ,"S" , "S", "S"},
        },
        //board 21
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"S" ,"A" , "A", "S"},
            {"S" ,"A" , "A", "A"},
            {"A" ,"A" , "S", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"S" , "A", "A"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"A" , "S", "S"},
            {"S" ,"S" , "S", "S"},
        },
        //board 22
        new string[,]{
            {"S" ,"S" , "S", "S"},
            {"A" ,"A" , "A", "A"},
            {"S" ,"S" , "A", "S"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"A" , "A", "A"},
            {"S" ,"A" , "S", "S"},
            {"A" ,"A" , "A", "A"},
            {"S" ,"S" , "S", "S"},
        },
        //board 23
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"S" ,"A" , "A", "A"},
            {"S" ,"A" , "S", "S"},
            {"A" ,"A" , "A", "S"},
            {"A" ,"S" , "A", "A"},
            {"A" ,"S" , "S", "A"},
            {"A" ,"A" , "A", "A"},
            {"S" ,"A" , "A", "S"},
            {"S" ,"S" , "S", "S"},
        },
        //board 24
        new string[,]
        {
            {"S" ,"S" , "S", "S"},
            {"S" ,"A" , "A", "A"},
            {"S" ,"A" , "S", "A"},
            {"S" ,"A" , "S", "S"},
            {"A" ,"A" , "A", "A"},
            {"A" ,"S" , "A", "S"},
            {"S" ,"S" , "A", "S"},
            {"S" ,"A" , "A", "S"},
            {"S" ,"S" , "S", "S"},
        },
        */
    };

    public GameObject[] Doors;
    public GameObject[] Pieces;
    public GameObject Button;
    string[,] creationBoard;
    GameObject[,] currentBoard;
    void Start()
    {
        for (int i = 0; i < gameBoards.Length; i++)
        {
            gameBoardsList.Add(gameBoards[i]);
        }
        CreateLevel();
    }

    void CreateLevel()
    {
        int x;
        int y;
        int buttonWorld = Random.Range(0, 9);
        int index = 0;
        GameObject tempPeice;
        string word;
        int boardPeice;
        int subboardLength = 4;
        int boardHeight = 9;
        float scale;
        for (int i = 0; i < 3; i++)
        {
            scale = Pieces[i].transform.localScale.x;
            for (int j = 0; j < 3; j++)
            {
                boardPeice = Random.Range(0, gameBoardsList.Count - 1);
                creationBoard = gameBoardsList[boardPeice];
                gameBoardsList.RemoveAt(boardPeice);
                index++;
                for (x = 0; x < subboardLength; x++)
                {
                    for (y = 0; y < boardHeight; y++)
                    {

                        word = creationBoard[y, x];
                        if (word == "S")
                        {
                            tempPeice = Instantiate(Pieces[i], new Vector2((j * 12 * scale) + x * 3 * scale, -(y * 3 * scale)), new Quaternion(0, 0, 0, 0));
                            tempPeice.name = "World" + i + "Subboard" + j + "Row" + y + "Column" + x;
                            tempPeice.layer = i + 9;
                            tempPeice.tag = "Solid";
                        }
                        if (word == "W" && buttonWorld == index)
                        {
                            tempPeice = Instantiate(Button, new Vector2((j * 12 * scale) + x * 3 * scale, -(y * 3 * scale)), new Quaternion(0, 0, 0, 0));
                            tempPeice.name = "Switch";
                            tempPeice.layer = i + 9;
                        }
                        
                        if (word.Substring(0,1) == "D")
                        {
                            tempPeice = Instantiate(Doors[i], new Vector2((j * 12 * scale) + x * 3 * scale, -(y * 3 * scale)), new Quaternion(0, 0, 0, 0));
                            tempPeice.name = "World" + i + "Door" + j;
                            tempPeice.layer = i + 9;
                            tempPeice.tag = "Door";
                            if (word.Substring(1, 1) == "H")
                            {
                                tempPeice.transform.Rotate(new Vector3(0, 0, 90));
                            }
                        }
                        
                    }
                }
            }
            int endpointnum = 0;
            for (int endpointx = -1; endpointx < subboardLength * 3 + 1; endpointx++)
            {
                for (int endpointy = -1; endpointy < boardHeight + 1; endpointy++)
                {
                    if (endpointy == -1 || endpointx == -1 || endpointx == subboardLength * 3 || endpointy == boardHeight)
                    {
                        tempPeice = Instantiate(Pieces[i], new Vector2(endpointx * 3 * scale, -(endpointy * 3 * scale)), new Quaternion(0, 0, 0, 0));
                        tempPeice.name = "World" + i + "Peice" + endpointnum;
                        tempPeice.layer = i + 9;
                        tempPeice.tag = "Solid";
                        endpointnum++;
                    }
                }
            }
        }
    }
}