using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class EnemyPoolManager : MonoBehaviour
{
    public Enemy voidlingPrefab;
    public Enemy voidbatPrefab;
    public Enemy voidspitterPrefab;
    public Enemy voidbeastPrefab;
    public Enemy voidgolemPrefab;
    public Enemy voidterrorPrefab;
    public Enemy nextEnemyToSpawn;
    public ObjectPool<Enemy> voidlingPool;
    public ObjectPool<Enemy> voidbatPool;
    public ObjectPool<Enemy> voidspitterPool;
    public ObjectPool<Enemy> voidbeastPool;
    public ObjectPool<Enemy> voidgolemPool;
    public ObjectPool<Enemy> voidterrorPool;
    public int totalMaxPoolSize = 600;
    public int totalMinPoolSize = 20;
    private int maxPoolSize = 100;
    private int allocatedDefaultPoolSize = 100;
    public int totalActiveCount;
    public int totalInactiveCount;

    [SerializeField] EnemySpawnManager spawnManager;
    [SerializeField] XpSpawnScript xpPool;
    [SerializeField] CurrencySpawnScript currencyPool;
    [SerializeField] CurrencyManagerScript currencyManager;

    public GameObject voidlingHolder;
    public GameObject voidbatHolder;
    public GameObject voidspitterHolder;
    public GameObject voidbeastHolder;
    public GameObject voidgolemHolder;
    public GameObject voidterrorHolder;
    public GameObject enemyHolder;

    private void Awake()
    {
        voidlingPool = new ObjectPool<Enemy>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemyToPool, true, allocatedDefaultPoolSize, maxPoolSize);
        voidbatPool = new ObjectPool<Enemy>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemyToPool, true, allocatedDefaultPoolSize, maxPoolSize);
        voidspitterPool = new ObjectPool<Enemy>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemyToPool, true, allocatedDefaultPoolSize, maxPoolSize);
        voidbeastPool = new ObjectPool<Enemy>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemyToPool, true, allocatedDefaultPoolSize, maxPoolSize);
        voidgolemPool = new ObjectPool<Enemy>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemyToPool, true, allocatedDefaultPoolSize, maxPoolSize);
        voidterrorPool = new ObjectPool<Enemy>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, OnDestroyEnemyToPool, true, allocatedDefaultPoolSize, maxPoolSize);
    }

    private void Update()
    {
        totalActiveCount = voidbatPool.CountActive + voidlingPool.CountActive + voidspitterPool.CountActive + voidbeastPool.CountActive + voidgolemPool.CountActive + voidterrorPool.CountActive;
        totalInactiveCount = voidbatPool.CountInactive + voidlingPool.CountInactive + voidspitterPool.CountInactive + voidbeastPool.CountInactive + voidgolemPool.CountInactive + voidterrorPool.CountInactive;
    }

    private void OnTakeEnemyFromPool(Enemy enemy)
    {
        spawnManager.ResetEnemyStatsAndPosition(enemy);
        enemy.gameObject.SetActive(true);
    }

    private void OnReturnEnemyToPool(Enemy enemy)
    {
        Vector3 xpDropOffset = new Vector3(enemy.transform.position.x + Random.Range(-0.5f, 0.5f), enemy.transform.position.y + Random.Range(-0.5f, 0.5f), enemy.transform.position.z);
        Vector3 currencyOffset = new Vector3(enemy.transform.position.x + Random.Range(-0.5f, 0.5f), enemy.transform.position.y + Random.Range(-0.5f, 0.5f), enemy.transform.position.z);

        if (xpPool != null && xpPool.isActiveAndEnabled)
        {
            xpPool.Pool().Get().transform.position = xpDropOffset;
        }

        if (currencyPool != null && currencyPool.isActiveAndEnabled && currencyManager.ChanceToSpawnCurrency() <= .10f)
        {
            currencyPool.Pool().Get().transform.position = currencyOffset;
        }

        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemyToPool(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    private Enemy CreateEnemy()
    {
        Vector3 enemySpawnPos = spawnManager.GenerateRandomEnemySpawnPosition();
        var enemy = Instantiate(nextEnemyToSpawn, enemySpawnPos, Quaternion.identity, enemyHolder.transform);
        enemy.player = spawnManager.player;
        //enemy.aiTarget = spawnManager.player.transform;
        enemy.damageTextPool = spawnManager.damageTextSpawn;
        enemy.spawnManager = spawnManager;
        enemy.Init(KillEnemy);
        return enemy;
    }

    private void KillEnemy(Enemy enemy)
    {
        if(enemy.enemyName == "Voidling")
        {
            voidlingPool.Release(enemy);
        } 
        else if(enemy.enemyName == "Voidbat")
        {
            voidbatPool.Release(enemy);
        }
        else if (enemy.enemyName == "Voidspitter")
        {
            voidspitterPool.Release(enemy);
        }
        else if (enemy.enemyName == "Voidbeast")
        {
            voidbeastPool.Release(enemy);
        }else if (enemy.enemyName == "Voidgolem")
        {
            voidgolemPool.Release(enemy);
        }else if (enemy.enemyName == "Voidterror")
        {
            voidterrorPool.Release(enemy);
        }
    }

}
