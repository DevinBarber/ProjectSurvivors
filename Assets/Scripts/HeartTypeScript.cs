using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartTypeScript : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private Image currentHeartType;

    void Awake()
    {
        currentHeartType = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                currentHeartType.sprite = emptyHeart;
                break;
            case HeartStatus.Full:
                currentHeartType.sprite = fullHeart;
                break;
        }
    }
}
public enum HeartStatus
{
    Empty = 0,
    Full = 1
}
