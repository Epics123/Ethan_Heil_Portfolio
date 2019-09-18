using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING, STOPPED}

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int enemyCount1;
        public int enemyCount2;
        public int enemyCount3;
        public float rate1;
        public float rate2;
        public float rate3;
        public Challenge challenge;
    }

    [System.Serializable]
    public class Challenge
    {
        public string name;
        public string enemyToKill;
        public int numToKill;
    }

    public const int MAX_ENEMIES = 10;

    public int totalToKill;

    public Wave[] waves;
    public int nextWave = 0;

    public Transform[] spawnPoints;

    public GameManager gm;
    public Camera cam;
    public MoveCamera mc;

    public GameObject challengeText;
    public GameObject player;


    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

	// Use this for initialization
	void Start () {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mc = GameObject.FindGameObjectWithTag("CameraObj").GetComponent<MoveCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
        challengeText = GameObject.FindGameObjectWithTag("ChallengeText");
        waveCountdown = timeBetweenWaves;
        challengeText.GetComponent<VisableChallenge>().playAnim();

    }
	
	// Update is called once per frame
	void Update () { 

        if(nextWave <= 4)
        {
            for(int i = 4; i < 16; i++)
            {
                spawnPoints[i].gameObject.SetActive(false);
            }
        }
        if(nextWave > 4 && nextWave <= 8)
        {
            for(int i = 0; i < 4; i++)
            {
                spawnPoints[i].gameObject.SetActive(false);
            }
            for (int i = 4; i < 8; i++)
            {
                spawnPoints[i].gameObject.SetActive(true);
            }
        }
        if (nextWave > 8 && nextWave <= 12)
        {
            for (int i = 4; i < 8; i++)
            {
                spawnPoints[i].gameObject.SetActive(false);
            }
            for (int i = 8; i < 12; i++)
            {
                spawnPoints[i].gameObject.SetActive(true);
            }
        }
        if (nextWave > 16)
        {
            for (int i = 8; i < 12; i++)
            {
                spawnPoints[i].gameObject.SetActive(false);
            }
            for (int i = 12; i < 16; i++)
            {
                spawnPoints[i].gameObject.SetActive(true);
            }
        }

        if (state == SpawnState.COUNTING)
        {
            totalToKill = GetCurrentWave().challenge.numToKill;
            challengeText.GetComponent<Text>().enabled = true;
            gm.showCountdown();
        }


        if(state == SpawnState.SPAWNING)
        {
            
            gm.hideCountdown();
            if (PlayerIsAlive())
            {
                if (CompleteChallenge())
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    for(int i = 0; i < enemies.Length; i++)
                    {
                        Destroy(enemies[i]);
                    }
                    WaveCompleted();
                    
                }
               
            }
            
        }

	    if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {    
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
	}

    IEnumerator ChangeMap()
    {
        if ((nextWave != 0 && nextWave != 12 && (nextWave % 4) == 0) || nextWave == 14)
        {
            waveCountdown = timeBetweenWaves * 4;
            yield return new WaitForSeconds(5.0f);
            //state = SpawnState.STOPPED;
            GameObject[] bones = GameObject.FindGameObjectsWithTag("Ammo");
            for (int i = 0; i < bones.Length; i++)
            {
                Destroy(bones[i]);
            }
            cam.GetComponent<ZoomController>().zoomOut = true;
            mc.move = true;
        }
    }

    void WaveCompleted()
    {
        //Debug.Log("Wave Completed!");
        GetCurrentWave().enemyCount1 = 0;
        GetCurrentWave().enemyCount2 = 0;
        GetCurrentWave().enemyCount3 = 0;

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        challengeText.GetComponent<VisableChallenge>().playAnim();



        if (nextWave + 1 > waves.Length - 1)
        {
            state = SpawnState.STOPPED;
            //Debug.Log("All Waves Complete!");
        }
        else
        {
            StartCoroutine("ChangeMap");
            nextWave++;
        }

        
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    bool PlayerIsAlive()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        if (state != SpawnState.STOPPED)
        {
            //Debug.Log("Spawning Wave: " + _wave.name);
            state = SpawnState.SPAWNING;

            if(_wave.enemyCount1 > 0)
            {
                for (int i = 0; i < _wave.enemyCount1; i++)
                {
                    if (PlayerIsAlive())
                    {
                        SpawnEnemy(_wave.enemies[0]);
                        yield return new WaitForSeconds(1f / _wave.rate1);
                    }
                }
            }

            if (_wave.enemyCount2 > 0)
            {
                for (int i = 0; i < _wave.enemyCount2; i++)
                {
                    if (PlayerIsAlive())
                    {
                        SpawnEnemy(_wave.enemies[1]);
                        yield return new WaitForSeconds(1f / _wave.rate2);
                    }
                }
            }

            if (_wave.enemyCount3 > 0)
            {
                for (int i = 0; i < _wave.enemyCount3; i++)
                {
                    if (PlayerIsAlive())
                    {
                        SpawnEnemy(_wave.enemies[2]);
                        yield return new WaitForSeconds(1f / _wave.rate3);
                    }
                }
            }



            state = SpawnState.WAITING;
        }
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("Spawning Enemy: " + _enemy.name);

        if(nextWave <= 4)
        {
            Transform _sp = spawnPoints[Random.Range(0, 3)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }
        if(nextWave > 4 && nextWave <= 8)
        {
            Transform _sp = spawnPoints[Random.Range(4, 7)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }
        if (nextWave > 8 && nextWave <= 16)
        {
            Transform _sp = spawnPoints[Random.Range(8, 11)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }
        if (nextWave > 16 && nextWave <= 20)
        {
            Transform _sp = spawnPoints[Random.Range(12, 15)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }


    }

    bool CompleteChallenge()
    {
        if(GetCurrentWave().challenge.numToKill <= 0)
        {
           return true;
        }
        else
        {
            return false;
        }
    }

    public Wave GetCurrentWave()
    {
        return waves[nextWave];
    }

    public int GetTotalToKill()
    {
        return totalToKill;
    }

}
