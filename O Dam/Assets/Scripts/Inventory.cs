using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory")]
public class Inventory : ScriptableObject
{
    public int straightCount, diagonalCount, dynamiteCount;
    public DamSelect damSelect;
    public Dynamite dynamite;

    public void Init(int numStraight, int numDiagonal, int numDynamite)
    {
        straightCount = numStraight;
        diagonalCount = numDiagonal;
        dynamiteCount = numDynamite;

        damSelect.Init(false);
        dynamite.Init(false);
    }

    public int GetTotalDam()
    {
        return straightCount + diagonalCount;
    }

    public int GetTotalDynamite()
    {
        return dynamiteCount;
    }

    public void DecreaseStraight()
    {
        if (straightCount > 0)
            straightCount--;
    }

    public void DecreaseDiag()
    {
        if (diagonalCount > 0)
            diagonalCount--;
    }

    public void DecreaseDynamite()
    {
        if (dynamiteCount > 0)
            dynamiteCount--;
    }

    public void IncreaseStraight()
    {
        straightCount++;
    }

    public void IncreaseDiag()
    {
        diagonalCount++;
    }

    public void IncreaseDynamite()
    {
        dynamiteCount++;
    }

    public IEnumerator FirstFill()
    {
        yield return null;
    }
}
