using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    #region DEFINE
    public GameObject[] enemyPrefabs;
    public GameObject enemyTwoPrefabs;
    public GameObject powerUp;
    public GameObject powerUpTwo;
    private int enemyCount;
    public int waveNumber = 1;
    private GameManager gameManager;

    private float enemySpawnX = 44.0f;
    private float[] enemySpawnY = { -20.0f, -17.5f, -15.0f, -12.5f, -10.0f, -7.5f, -5.0f, -2.5f, 0.0f, 2.5f, 5.0f, 7.5f, 10.0f, 12.5f, 15.0f, 17.5f, 20.0f };
    private float enemyTwoSpawnY = 20.0f;
    private float minEnemySpawnZ = 250.0f;
    private float maxEnemySpawnZ = 350.0f;


    private float powerupSpawnX = 48.0f;
    private float powerupSpawnY = 24.0f;
    private float powerupSpawnZ = 3;

    private int level = 0;
    public TextMeshProUGUI levelText;

    #endregion

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {

        enemyCount = FindObjectsOfType<Enemy>().Length + FindObjectsOfType<Enemy2>().Length;

        if (enemyCount == 0 && gameManager.isGameActive == true) 
        {
            SpawningEnemies(waveNumber);
            waveNumber++;
            SpawningPowerup();
            SpawningPowerupTwo();
            level++;
        }

        Levelling();
    }
    void SpawningEnemies(int waveNumber)
    {
        for (int i = 0; i < waveNumber; i++)
        {
            int randomY = Random.Range(0, enemySpawnY.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-enemySpawnX, enemySpawnX), enemySpawnY[randomY], Random.Range(minEnemySpawnZ, maxEnemySpawnZ));
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
        }
        Vector3 spawnPosTwo = new Vector3(Random.Range(-enemySpawnX, enemySpawnX), Random.Range(-enemyTwoSpawnY, enemyTwoSpawnY), Random.Range(minEnemySpawnZ, maxEnemySpawnZ));
        if (waveNumber % 5 == 0)
        {
            Instantiate(enemyTwoPrefabs, spawnPosTwo, enemyTwoPrefabs.transform.rotation);
        }
    }
    void SpawningPowerup()
    {
        if (level >= 4 && level % 2 == 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-powerupSpawnX, powerupSpawnX), Random.Range(-powerupSpawnY, powerupSpawnY), powerupSpawnZ);
            Instantiate(powerUp, spawnPos, powerUp.transform.rotation);
        }
    }
    void SpawningPowerupTwo()
    {
        if (level >= 4 && level % 2 == 1)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-powerupSpawnX, powerupSpawnX), Random.Range(-powerupSpawnY, powerupSpawnY), powerupSpawnZ);
            Instantiate(powerUpTwo, spawnPos, powerUpTwo.transform.rotation);
        }
    }

    void Levelling()
    {
        levelText.text = "Level: " + level;
    }
}
