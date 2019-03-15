using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIElement : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float smoothTime;
    bool hide = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideElement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 desiredPos = Vector3.zero;
        if (hide == false)
        {
            desiredPos = end.position;
        }
        else
        {
            desiredPos = start.position;
        }

        
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime / smoothTime);
        transform.position = smoothPos;
    }

    IEnumerator HideElement()
    {
        yield return new WaitForSeconds(4f);
        hide = true;
    }
}
