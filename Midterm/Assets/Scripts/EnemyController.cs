using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameManager gm;

    bool mouseOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseDown()
    {
        if(mouseOver)
        {
            gm.numEnemies--;
            Destroy(gameObject);
        }
    }
}
