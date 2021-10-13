using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform enemyPrefab;

    [SerializeField] public Transform spawnPoint;
    [SerializeField] public float timeBetweenWaves = 5f;
    [SerializeField] public float timeBetweenEnemies = 0.2f; 
    [SerializeField] public float countdown = 2f;

    private int waveIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position,spawnPoint.rotation);
    }
}
