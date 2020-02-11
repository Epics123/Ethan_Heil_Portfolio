using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dynamite", menuName = "ScriptableObjects/Dynamite")]
public class Dynamite : ScriptableObject
{

    public bool isClicked;
    // Start is called before the first frame update
    public void Init(bool clicked)
    {
        isClicked = clicked;
    }

   
    
    public void SetIsClicked(bool clicked)
    {
        isClicked = clicked; 
    }
}
