using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject EnemyGO;

    public float maxSpawnRateInSeconds;
    public float spawnDuration;
    // Use this for initialization
    void Start () {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
        spawnDuration += Time.time;   
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x*0.9f, max.x * 0.9f), max.y);
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawninNSeconds;
        if(maxSpawnRateInSeconds > 1f)
        {
            spawninNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawninNSeconds = 1f;
        }
        if (Time.time < spawnDuration)
        {
            Invoke("SpawnEnemy", spawninNSeconds);
        }
    }
    //increases difficulty
    void IncreaseSpawnRate()
    {
        if(maxSpawnRateInSeconds> 1f)
        {
            maxSpawnRateInSeconds--;
        }
        if(maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

}
