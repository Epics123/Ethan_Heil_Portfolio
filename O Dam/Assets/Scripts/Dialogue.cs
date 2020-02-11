using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text currentText, nextText;
    public string[] sentences;
    public float typeSpeed;
    public bool canType = false;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        currentText.text = "";
        nextText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TypeText()
    {
        if (canType)
        {
            char[] letters = sentences[index].ToCharArray();
            
            for(int i = 0; i < letters.Length; i++)
            {
                currentText.text += letters[i];
                yield return new WaitForSeconds(typeSpeed);
            }
        }  
    }

    public void ChangeSentence(int sentenceIndex)
    {
        if(sentenceIndex < sentences.Length)
        {
            index = sentenceIndex;
            ClearText();
            StartCoroutine(TypeText());
        }
    }

    public void ClearText()
    {
        currentText.text = "";
        nextText.text = "";
    }

    public void SwapText()
    {
        ClearText();

        Text temp = currentText;
        currentText = nextText;
        nextText = temp;
    }
}
