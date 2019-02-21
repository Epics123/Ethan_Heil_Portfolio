using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameManager gm;
    public GameObject enemy;
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
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWaves()
    {
        while(gm.playerDead == false)
        {
            gm.waveNum = (int)enemyCount - 1;
            gm.UpdateWave();
            yield return new WaitForSeconds(startDelay);
            for (int i = 0; i < enemyCount; i++)
            {
                spawnDistance = Random.Range(1.5f, 8.5f);

                Vector3 temp = Vector3.Normalize(endPos - startPos);
                spawnPos = startPos + (spawnDistance * temp);

                Instantiate(enemy, spawnPos, transform.rotation);

                yield return new WaitForSeconds(spawnDelay);
            }
            yield return new WaitForSeconds(waveDelay);
            enemyCount++;
        }  
    }
}
