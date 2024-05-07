using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManagerScript : MonoBehaviour
{
    public CurrencyTypeScript currencyTypeScript;
    public PlayerScript player;
    public TextMeshProUGUI currencyText;

    private float timeSurvivedAmount;
    public float totalSurvivedAmount;

    private float levelAmount;
    public float totalLevelAmount;

    //Total Currency
    private float totalCurrencyAmountPerRun;

    //Picked Up Currency Values
    public float totalAmountCollected;
    public float totalValueCollected;
    public float dropAmount;

    private float everyMinute = 60f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timeSurvivedAmount = currencyTypeScript.GetTimeSurvivedAmount();
        levelAmount = currencyTypeScript.GetLevelAmount();
        dropAmount = currencyTypeScript.GetDroppedAmount();
        timer = everyMinute;
        UpdateUI();
    }

    private void Update()
    {
        if(Time.time > timer)
        {
            CalculateAmountPerMinute();
            timer = Time.time + everyMinute;
        }
    }

    public void AddToCurrency(float amount)
    {
        totalCurrencyAmountPerRun += amount;
        totalValueCollected += amount;
        totalAmountCollected += 1;
        UpdateUI();
    }

    public float ChanceToSpawnCurrency()
    {
        float randomValue = Random.value;
        return randomValue; 
    }

    public void CalculateAmountPerMinute()
    {
        totalCurrencyAmountPerRun += timeSurvivedAmount;
        totalSurvivedAmount += timeSurvivedAmount;
        //UpdateUI();
    }
    public void CalculateAmountPerLevel()
    {
        totalCurrencyAmountPerRun += levelAmount;
        totalLevelAmount += levelAmount;
        //UpdateUI();
    }

    private void UpdateUI()
    {
        currencyText.text = totalValueCollected.ToString();
    }

    public void AddTotalCurrencyToPlayer()
    {
        player.SetPlayerTotalCurrency(totalCurrencyAmountPerRun);
    }
}
