using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class XpSpawnScript : MonoBehaviour
{
    public ExperiencePointsScript xpPrefab;
    public LevelScalingScript lvlScale;
    public GameObject player;

    //Pooling Variables
    ObjectPool<ExperiencePointsScript> _pool;
    public int ActiveCount;
    public int InactiveCount;
    private int maxPoolSize = 200;
    private int allocatedDefaultPoolSize = 100;

    void Awake()
    {
        _pool = new ObjectPool<ExperiencePointsScript>(CreateXpObject, OnTakeXpFromPool, OnReturnXpToPool, OnDestroyXpToPool, true, allocatedDefaultPoolSize, maxPoolSize);
    }

    // Update is called once per frame
    void Update()
    {
        ActiveCount = _pool.CountActive;
        InactiveCount = _pool.CountInactive;
    }

    private void OnTakeXpFromPool(ExperiencePointsScript xp)
    {
        xp.gameObject.SetActive(true);
    }

    private void OnReturnXpToPool(ExperiencePointsScript xp)
    {
        xp.gameObject.SetActive(false);
    }

    private void OnDestroyXpToPool(ExperiencePointsScript xp)
    {
        Destroy(xp.gameObject);
    }

    private ExperiencePointsScript CreateXpObject()
    {
        var xp = Instantiate(xpPrefab);
        xp.player = player;
        xp.lvlScale = lvlScale;
        xp.Init(PickUpXp);
        return xp;
    }

    public IObjectPool<ExperiencePointsScript> Pool()
    {
        return _pool;
    }
    private void PickUpXp(ExperiencePointsScript xpPickUp)
    {
        _pool.Release(xpPickUp);
    }
}
