using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamSelect", menuName = "ScriptableObjects/DamSelect")]
public class DamSelect : ScriptableObject
{
    public bool isClicked = false;

    public void Init(bool clicked)
    {
        isClicked = clicked;
    }

    public void SetClicked(bool val)
    {
        isClicked = val;
    }
}
