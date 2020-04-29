using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    /// <summary>
    /// THERE SHOULD ONLY BE ONE OF THIS
    /// </summary>


    // * Variables *
    [SerializeField] GameObject[] popUpUI;

    // ** Update Functions **
    private void Awake()
    {
        popUpUI = GameObject.FindGameObjectsWithTag("PopUp");
    }


    // **** Other Functions ****
    public void Trigger(string linkCode)
    {
        foreach (GameObject popUp in popUpUI)
        {
            if (linkCode == popUp.GetComponent<PopUpPanel>().linkCode)
            {
                popUp.GetComponent<PopUpPanel>().TurnOn();
            }
        }
    }
}
