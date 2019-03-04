using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpTrigger : MonoBehaviour
{
    public Image background;
    public Text text;
    public LayerMask player;

    bool dialogueActive = false;

    // Start is called before the first frame update
    void Start()
    {
        background.enabled = false;
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(dialogueActive == false)
            {
                StartCoroutine(DisplayDialogue());
            }   
        }
    }

    IEnumerator DisplayDialogue()
    {
        background.enabled = true;
        text.enabled = true;
        dialogueActive = true;

        yield return new WaitForSeconds(3f);

        background.enabled = false;
        text.enabled = false;
        dialogueActive = false;
    }
}
