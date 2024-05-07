using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickUpScript : MonoBehaviour
{
    public GameObject player;
    public CurrencyManagerScript currencyManager;
    private Vector3 playerPos;
    public bool playerCollected;
    [SerializeField] private float speed = 8f;
    private float currencyAmount;
    private Action<CurrencyPickUpScript> pickedUpCurrency;

    private void Start()
    {
        currencyAmount = currencyManager.dropAmount;
    }

    void OnEnable()
    {
        playerCollected = false;
    }

    // Update is called once per frame
    void Update() 
    {
        playerPos = player.transform.position;

        if (playerCollected)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, speed * Time.deltaTime);

            if (this.transform.position == playerPos)
            {
                currencyManager.AddToCurrency(currencyAmount);
                pickedUpCurrency(this);
            }
        }
    }

    public void Init(Action<CurrencyPickUpScript> _pickedUpCurrency)
    {
        pickedUpCurrency = _pickedUpCurrency;
    }
}
