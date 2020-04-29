using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeText : MonoBehaviour
{
    public Text textDisplay;
    public string message;
    public float typeSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Type()
    {
        foreach(char letter in message.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
