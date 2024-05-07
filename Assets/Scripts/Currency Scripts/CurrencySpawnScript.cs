using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CurrencySpawnScript : MonoBehaviour
{
    public CurrencyPickUpScript currencyPrefab;
    public CurrencyManagerScript currencyManager;
    public GameObject player;

    //Pooling Variables
    ObjectPool<CurrencyPickUpScript> _pool;
    public int ActiveCount;
    public int InactiveCount;
    private int maxPoolSize = 50;
    private int allocatedDefaultPoolSize = 20;

    void Awake()
    {
        _pool = new ObjectPool<CurrencyPickUpScript>(CreateCurrencyObject, OnTakeCurrencyFromPool, OnReturnCurrencyToPool, OnDestroyCurrencyToPool, true, allocatedDefaultPoolSize, maxPoolSize);
    }

    // Update is called once per frame
    void Update()
    {
        ActiveCount = _pool.CountActive;
        InactiveCount = _pool.CountInactive;
    }

    private void OnTakeCurrencyFromPool(CurrencyPickUpScript currency)
    {
        currency.gameObject.SetActive(true);
    }

    private void OnReturnCurrencyToPool(CurrencyPickUpScript currency)
    {
        currency.gameObject.SetActive(false);
    }

    private void OnDestroyCurrencyToPool(CurrencyPickUpScript currency)
    {
        Destroy(currency.gameObject);
    }

    private CurrencyPickUpScript CreateCurrencyObject()
    {
        var currency = Instantiate(currencyPrefab);
        currency.player = player;
        currency.currencyManager = currencyManager;
        currency.Init(PickUpCurrency);
        return currency;
    }

    public IObjectPool<CurrencyPickUpScript> Pool()
    {
        return _pool;
    }
    private void PickUpCurrency(CurrencyPickUpScript currencyPickUp)
    {
        _pool.Release(currencyPickUp);
    }
}
