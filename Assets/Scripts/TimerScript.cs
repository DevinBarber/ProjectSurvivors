using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] EnemySpawnManager spawnManager;
    public TextMeshProUGUI timerText;
    private float timeStart = 1800f;
    private float timeToIncrement = 120f;
    public float timeSurvived = 0f;
    public int currentTimeInterval = 0;
    private bool isIncrementing = false;

    // Update is called once per frame
    void Update()
    {
        if(timeStart > 0)
        {
            timeStart -= Time.deltaTime;
            timeSurvived += Time.deltaTime;
        }else
        {
            timeStart = 0;
            timeSurvived = 1800f;
        }

        timerText.text = DisplayTime(timeStart);

        if (!isIncrementing)
        {
            StartCoroutine(IncrementTimeInterval());
        }

    }

    private IEnumerator IncrementTimeInterval()
    {
        isIncrementing = true;

        yield return new WaitForSeconds(timeToIncrement);
        currentTimeInterval++;
        spawnManager.UpdateInterval();
        isIncrementing = false;
    }

    public string DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        } else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60f);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
