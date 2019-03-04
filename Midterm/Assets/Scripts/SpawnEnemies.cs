using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject enemy;
    public GameManager gm;
    public Transform start;
    public Transform end;
    public float spawnDistance;
    public float spawnDelay;
    public float startDelay;
    public float waveDelay;
    public float enemyCount;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 spawnPos;
    

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        startPos = start.position;
        endPos = end.position;
        gm.numEnemies = enemyCount;
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.numEnemies <= 0)
        {
            gm.numEnemies = enemyCount;
            StartCoroutine(EnemySpawn());
        }
    }


    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(startDelay);

        //Spawn enemies at a random point between startPos and endPos
        for (int i = 0; i < enemyCount; i++)
        {
            spawnDistance = Random.Range(1f, 16f);

            Vector3 temp = Vector3.Normalize(endPos - startPos);
            spawnPos = startPos + (spawnDistance * temp);

            Instantiate(enemy, spawnPos, transform.rotation);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
