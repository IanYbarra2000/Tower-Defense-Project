using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] enemyPrefabs;

    [SerializeField] public Transform spawnPoint;
    [SerializeField] public float timeBetweenWaves = 5f;
    [SerializeField] public float timeBetweenEnemies = 0.2f; 
    [SerializeField] public float countdown = 2f;

    private int waveIndex = 0; //acts as a count for which wave is happening and a count for the amount of total enemy health allowed for the wave
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
        //print("Spawn wave "+waveIndex); 
        for (int i = 0; i < waveIndex; i++)
        {
            int spawnIndex = chooseEnemy(waveIndex-i);//chooses which enemy to spawn
            SpawnEnemy(spawnIndex);
            i += enemyPrefabs[spawnIndex].gameObject.GetComponent<Enemy>().health-1;
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        
    }

    int chooseEnemy(int healthLeft){
        /* 
        Chooses random enemy to spawn so long as it it doesn't
        have more health than the amount that is allowed during
        a particular wave
        */

        int start = Random.Range(0,enemyPrefabs.Length);
        //print("random num ="+start);
        for (int i = start;i>=0;i--){
            if(enemyPrefabs[i].gameObject.GetComponent<Enemy>().health <= healthLeft){
                return i;
            }
        }
        return 0;
    }
    void SpawnEnemy(int index)
    {
        Instantiate(enemyPrefabs[index], spawnPoint.position,spawnPoint.rotation);
    }
}
