using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] EnemyPoolManager enemyPoolManager;
    [SerializeField] TimerScript timer;
    public DamageTextSpawnScript damageTextSpawn;
    public PlayerScript player;
    private float camHeight;
    private float camWidth;
    private float maxSpawnWidth;
    private float maxSpawnHeight;

    private bool canSpawnEnemies = false;
    private bool spawningEnemies = false;

    void Start()
    {
        camHeight = mainCamera.orthographicSize;
        camWidth = mainCamera.orthographicSize * mainCamera.aspect;
        maxSpawnWidth = camWidth + (camWidth / 2f);
        maxSpawnHeight = camHeight + (camHeight / 2f);
    }

    private void Update()
    {
        CheckIfOkayToSpawnEnemies();

        if (canSpawnEnemies && enemyPoolManager.totalActiveCount < enemyPoolManager.totalMinPoolSize)
        {
            //Spawn Wave or Something 
        }

        if (canSpawnEnemies && !spawningEnemies)
        {
            SpawningSystem(timer.currentTimeInterval);
        }
    }

    private void CheckIfOkayToSpawnEnemies()
    {
        if (enemyPoolManager.totalActiveCount >= enemyPoolManager.totalMaxPoolSize)
        {
            canSpawnEnemies = false;
        }
        else
        {
            canSpawnEnemies = true;
        }
    }

    public void UpdateInterval()
    {
        spawningEnemies = false;
    }

    private void SpawnEnemy(string enemyName)
    {
        if (enemyName == "Voidling")
        {
            enemyPoolManager.enemyHolder = enemyPoolManager.voidlingHolder;
            enemyPoolManager.nextEnemyToSpawn = enemyPoolManager.voidlingPrefab;
            enemyPoolManager.voidlingPool.Get();
        }
        else if (enemyName == "Voidbat")
        {
            enemyPoolManager.enemyHolder = enemyPoolManager.voidbatHolder;
            enemyPoolManager.nextEnemyToSpawn = enemyPoolManager.voidbatPrefab;
            enemyPoolManager.voidbatPool.Get();
        }
        else if (enemyName == "Voidspitter")
        {
            enemyPoolManager.enemyHolder = enemyPoolManager.voidspitterHolder;
            enemyPoolManager.nextEnemyToSpawn = enemyPoolManager.voidspitterPrefab;
            enemyPoolManager.voidspitterPool.Get();
        }
        else if (enemyName == "Voidbeast")
        {
            enemyPoolManager.enemyHolder = enemyPoolManager.voidbeastHolder;
            enemyPoolManager.nextEnemyToSpawn = enemyPoolManager.voidbeastPrefab;
            enemyPoolManager.voidbeastPool.Get();
        }else if (enemyName == "Voidgolem")
        {
            enemyPoolManager.enemyHolder = enemyPoolManager.voidgolemHolder;
            enemyPoolManager.nextEnemyToSpawn = enemyPoolManager.voidgolemPrefab;
            enemyPoolManager.voidgolemPool.Get();
        }else if (enemyName == "Voidterror")
        {
            enemyPoolManager.enemyHolder = enemyPoolManager.voidterrorHolder;
            enemyPoolManager.nextEnemyToSpawn = enemyPoolManager.voidterrorPrefab;
            enemyPoolManager.voidterrorPool.Get();
        }
    }

    private IEnumerator SpawnEnemiesCoroutine(string enemyName, int minAmountToSpawn, int maxAmountToSpawn, float spawnCooldown)
    {
        while (true)
        {
            int spawnAmount;

            if (maxAmountToSpawn == 1)
            {
                spawnAmount = 1;
            }
            else
            {
                spawnAmount = Random.Range(minAmountToSpawn, maxAmountToSpawn + 1);
            }

            for (int i = 0; i < spawnAmount; i++)
            {
                SpawnEnemy(enemyName);
            }

            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private void SpawningSystem(int currentInterval)
    {
        spawningEnemies = true;
        StopAllCoroutines();

        switch (currentInterval)
        {
            case 0:
                StartCoroutine(SpawnEnemiesCoroutine("Voidling", 5, 10, 6));
                break;
            case 1:
                StartCoroutine(SpawnEnemiesCoroutine("Voidbat", 1, 2, 4));
                break;
            case 2:
                StartCoroutine(SpawnEnemiesCoroutine("Voidspitter", 2, 2, 5));
                break;
            case 3:
                StartCoroutine(SpawnEnemiesCoroutine("Voidbeast", 1, 3, 4));
                break;
            default:
                canSpawnEnemies = false;
                break;
        }
    }

    public void ResetEnemyStatsAndPosition(Enemy enemy)
    {
        Vector3 spawnPos = GenerateRandomEnemySpawnPosition();
        enemy.transform.position = spawnPos;
    }

    public Vector3 GenerateRandomEnemySpawnPosition()
    {
        Vector3 ePos = new Vector3();
        float temp = Random.value > 0.5f ? -1f : 1f;
        if (Random.value > 0.5f)
        {
            ePos.x = Random.Range(-maxSpawnWidth, maxSpawnWidth);
            ePos.y = Random.Range(camHeight, maxSpawnHeight) * temp;
        }
        else
        {
            ePos.y = Random.Range(-maxSpawnHeight, maxSpawnHeight);
            ePos.x = Random.Range(camWidth, maxSpawnWidth) * temp;
        }

        ePos.z = 1;

        return ePos += player.transform.position;
    }

}
