using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private GameObject normalEnemy;

    public int numberOfWaves;
    public int numberOfEnemies;
    public int multiplier;

    private int currentWave;
    private int remainingEnemiesToSpawn;
    private bool isSpawning = false;

    void Start()
    {
        normalEnemy = Resources.Load<GameObject>("Prefabs/Enemy Spacecraft");

        currentWave = 1;
        remainingEnemiesToSpawn = numberOfEnemies + (currentWave * multiplier);

        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        if (currentWave != numberOfWaves)
        {
            if (remainingEnemiesToSpawn == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                // start the next wave
                currentWave++;
                remainingEnemiesToSpawn += numberOfEnemies + (currentWave * multiplier);

                Debug.Log("WAVE: Starting Next Wave");
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            Debug.Log("WAVE: All waves completed");
        }
    }

    private IEnumerator SpawnWave()
    {
        if (isSpawning) yield break;

        isSpawning = true;

        while (remainingEnemiesToSpawn > 0)
        {
            SpawnEnemy();
            remainingEnemiesToSpawn--;

            Debug.Log("WAVE: Spawning Enemy");

            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }

        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        float spawnPointX = Random.Range(-1.497f, 1.497f);
        float spawnPointY = 1.049f;

        Instantiate(normalEnemy, new Vector3(spawnPointX, spawnPointY, 1f), Quaternion.identity);
    }
}
