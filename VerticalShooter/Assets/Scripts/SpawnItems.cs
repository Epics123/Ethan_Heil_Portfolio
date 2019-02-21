using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject item;
    public GameManager gm;
    public Transform start;
    public Transform end;
    public float spawnDistance;
    public float spawnDelay = 10;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        startPos = start.position;
        endPos = end.position;

        StartCoroutine(DelayStart(spawnDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnItem(float time)
    {
        spawnDistance = Random.Range(0.5f, 9.5f);

        Vector3 temp = Vector3.Normalize(endPos - startPos);
        spawnPos = startPos + (spawnDistance * temp);

        Instantiate(item, spawnPos, transform.rotation);

        yield return new WaitForSeconds(time);

        if(gm.playerDead == false)
        {
            StartCoroutine(SpawnItem(spawnDelay));
        }
    }

    IEnumerator DelayStart(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnItem(spawnDelay));
    }
}
