using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Zombie Spawning Settings")]
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public GameObject targetObject;
    public int zombiesPerWave = 5;
    public int numberOfWaves = 3;
    public float spawnInterval = 1.0f;
    public float waveInterval = 5.0f;

    private int currentWave = 0;
    private int zombiesSpawnedInWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (currentWave = 0; currentWave < numberOfWaves; currentWave++)
        {
            yield return StartCoroutine(SpawnZombiesInWave());

            if (currentWave < numberOfWaves - 1)
            {
                yield return new WaitForSeconds(waveInterval);
            }
        }
    }

    private IEnumerator SpawnZombiesInWave()
    {
        for (zombiesSpawnedInWave = 0; zombiesSpawnedInWave < zombiesPerWave; zombiesSpawnedInWave++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnZombie()
    {
        if (zombiePrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject zombieObj = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        ZombieController controller = zombieObj.GetComponent<ZombieController>();
        if (controller != null && targetObject != null)
        {
            controller.targetCube = targetObject.transform;
        }
    }
}
