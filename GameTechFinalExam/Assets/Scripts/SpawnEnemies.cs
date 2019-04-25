using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    public GameManager gm;
    public Transform start;
    public Transform end;
    public float spawnDistance;
    public float spawnDelay;
    public float startDelay;
    public int enemyCount;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 spawnPos;
    int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        startPos = start.position;
        endPos = end.position;
        numEnemies = enemyCount;
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
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
            numEnemies--;

            yield return new WaitForSeconds(spawnDelay);
        }

        if(numEnemies <= 0)
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        gm.loadLevel.LoadNextLevel();
    }
}
