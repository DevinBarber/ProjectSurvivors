using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextSpawnScript : MonoBehaviour
{
    public DamagePopUpScript damagePopUpPrefab;

    //Pooling Variables
    ObjectPool<DamagePopUpScript> _pool;
    public int ActiveCount;
    public int InactiveCount;
    private int maxPoolSize = 100;
    private int allocatedDefaultPoolSize = 100;

    void Awake()
    {
        _pool = new ObjectPool<DamagePopUpScript>(CreateDamageTextObject, OnTakeDamageTextFromPool, OnReturnDamageTextToPool, OnDestroyDamageTextToPool, true, allocatedDefaultPoolSize, maxPoolSize);
    }

    // Update is called once per frame
    void Update()
    {
        ActiveCount = _pool.CountActive;
        InactiveCount = _pool.CountInactive;
    }

    private void OnTakeDamageTextFromPool(DamagePopUpScript damageText)
    {
        damageText.gameObject.SetActive(true);
    }

    private void OnReturnDamageTextToPool(DamagePopUpScript damageText)
    {
        damageText.gameObject.SetActive(false);
    }

    private void OnDestroyDamageTextToPool(DamagePopUpScript damageText)
    {
        Destroy(damageText.gameObject);
    }

    private DamagePopUpScript CreateDamageTextObject()
    {
        var damageText = Instantiate(damagePopUpPrefab);
        damageText.Init(RemoveText);
        return damageText;
    }

    public IObjectPool<DamagePopUpScript> Pool()
    {
        return _pool;
    }
    private void RemoveText(DamagePopUpScript damageText)
    {
        _pool.Release(damageText);
    }
}
