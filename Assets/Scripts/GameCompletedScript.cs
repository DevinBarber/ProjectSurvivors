using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCompletedScript : MonoBehaviour
{
    public TextMeshProUGUI mainText, timeText, levelsText, collectedText, totalText;
    public TextMeshProUGUI timeTextAmount, levelsTextAmount, collectedTextAmount, totalTextAmount;
    public TimerScript timerScript;
    public PlayerScript player;
    public CurrencyManagerScript currencyManagerScript;
    public bool survived;
    private float levelsGained, currencyCollected, totalCurrencyEarned, levelsGainedAmount, currencyCollectedAmount, totalTimeAmount;
    private float timeSurvived;

    void Start()
    {
        timeSurvived = timerScript.timeSurvived;
        levelsGained = player.GetPlayerLevel() - 1;
        currencyCollected = currencyManagerScript.totalAmountCollected;
        totalCurrencyEarned = player.GetPlayerTotalCurrency();
        levelsGainedAmount = currencyManagerScript.totalLevelAmount;
        currencyCollectedAmount = currencyManagerScript.totalValueCollected;
        totalTimeAmount = currencyManagerScript.totalSurvivedAmount;

        if (survived)
        {
            mainText.text = "You Survived";
        }
        else
        {
            mainText.text = "You Died";
        }

        timeText.text = "Time Survived: (" + timerScript.DisplayTime(timeSurvived) + ")";
        levelsText.text = "Levels Gained: (" + levelsGained.ToString() + ")";
        collectedText.text = "Currency Collected: (" + currencyCollected.ToString() + ")";
        totalText.text = "Total Currency Earned: ";

        timeTextAmount.text = totalTimeAmount.ToString();
        levelsTextAmount.text = levelsGainedAmount.ToString();
        collectedTextAmount.text = currencyCollectedAmount.ToString();
        totalTextAmount.text = totalCurrencyEarned.ToString();
    }
}
