using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject EnemyGO;

    public float maxSpawnRateInSeconds;
	// Use this for initialization
	void Start () {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawninNSeconds;
        if(maxSpawnRateInSeconds > 1f)
        {
            spawninNSeconds = Random.RandomRange(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawninNSeconds = 1f;
        }
        Invoke("SpawnEnemy", spawninNSeconds);
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
