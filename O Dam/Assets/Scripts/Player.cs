using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class Player : ScriptableObject
{
    public string playerName = "New Player";
    public Color playerColor = Color.black;
    public enum State { Idle, Active };
    public State playerState = State.Idle;
    public Inventory playerInventory;
    public bool isPlaying = false;
    public int player = 0;
    public bool placingDam, placingDynamite;

    public void Init(string name, Color color, int state, bool turn)
    {
        this.playerName = name;
        this.playerColor = color;
        this.isPlaying = false;
        this.playerState = (State)state;
        this.playerInventory.Init(10, 10, 10);
    }

    public void SetTurn(bool val)
    {
        isPlaying = val;
    }
}
