using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    // * Variables *
    public string linkCode; // only effects things with the same link code (among the same object group.)
    public int mask;
    TriggerManager triggerManager;

    SpriteRenderer spr;

    // ** Update Functions **
    private void Awake()
    {
        triggerManager = GameObject.Find("TriggerManager").GetComponent<TriggerManager>();
    }
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == mask)
            triggerManager.Trigger(linkCode);
    }



    // **** Other Functions ****

}
