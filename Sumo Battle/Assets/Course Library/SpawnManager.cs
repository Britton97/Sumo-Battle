using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int spawnAmount = 3;
    public int alive;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(spawnAmount);
        alive = spawnAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive <= 0)
        {
            spawnAmount++;
            SpawnEnemyWave(spawnAmount);
        }
    }

    private void SpawnEnemyWave(int waveNum)
    {
        alive = spawnAmount;
        for (int i = 0; i < waveNum; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPoint(), Quaternion.identity);
        }
    }

    private Vector3 GenerateSpawnPoint()
    {
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);
        return new Vector3(x, 0, z);
    }

    public void FellOff()
    {
        alive -= 1;
    }
}
