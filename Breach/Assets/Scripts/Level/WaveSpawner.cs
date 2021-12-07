using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    public float timeBetweenWaves;
    public Wave[] waves;

    private float initialCountdown = 5f;
    private float countdown = 0f;
    private int waveNumber = 0;

    int numberOfWaves = 0;

    private void Awake()
    {
        numberOfWaves = waves.Length;
    }

    private void Update()
    {
        if (countdown <= 0f && waveNumber < numberOfWaves)
        {
            countdown = timeBetweenWaves;
            StartCoroutine(SpawnWave());
        }

        if (waveNumber >= numberOfWaves)
        { 
            //Last wave has spawned, notify LevelManager to check for winner
            LevelManager.instance.CheckForWinner();
        }

    }

    private IEnumerator SpawnWave()
    {
        var wave = waves[waveNumber];

        var numberOfEnemies = wave.GetNumberOfEnemies();
        var secondsBetweenEnemies = wave.GetSecondsBetweenEnemies();

        for (int i = 0; i < numberOfEnemies; i++)
        {
            yield return new WaitForSeconds(secondsBetweenEnemies);
            SpawnEnemy(wave.GetEnemy(i));
        }

        yield return new WaitForSeconds(timeBetweenWaves);
        waveNumber++;
        countdown = 0f;
    }

    private void SpawnEnemy(Transform enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public int GetTotalWaves()
    { 
        return numberOfWaves;
    }

    public int GetCurrentWave()
    {
        if (waveNumber + 1 > numberOfWaves)
            return numberOfWaves;
        else
            return waveNumber + 1;
    }
}
