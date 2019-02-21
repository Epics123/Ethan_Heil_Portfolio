using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for spawning enemies dinamically
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

    //Controls the dynamic spawning of enemies
    IEnumerator SpawnWaves()
    {
        while(gm.playerDead == false)
        {
            //Update UI
            gm.waveNum = (int)enemyCount - 1;
            gm.UpdateWave();
            yield return new WaitForSeconds(startDelay);

            //Spawn enemies at a random point between startPos and endPos
            for (int i = 0; i < enemyCount; i++)
            {
                spawnDistance = Random.Range(1.5f, 8.5f);

                Vector3 temp = Vector3.Normalize(endPos - startPos);
                spawnPos = startPos + (spawnDistance * temp);

                Instantiate(enemy, spawnPos, transform.rotation);

                yield return new WaitForSeconds(spawnDelay);
            }
            //Wait for new wave and increase the amount of enemies
            yield return new WaitForSeconds(waveDelay);
            enemyCount++;
        }  
    }
}
