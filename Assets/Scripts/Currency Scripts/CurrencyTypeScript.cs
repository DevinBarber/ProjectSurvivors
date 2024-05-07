using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency", menuName = "ScriptableObject/New Currency")]

public class CurrencyTypeScript : ScriptableObject
{
    //Base Currency Ammounts - Scriptable Object
    [SerializeField]
    private float droppedAmount;
    [SerializeField]
    private float levelAmount;
    [SerializeField]
    private float timeSurvivedAmount;

    public float GetDroppedAmount()
    {
        return droppedAmount;
    }
    public float GetLevelAmount()
    {
        return levelAmount;
    }
    public float GetTimeSurvivedAmount()
    {
        return timeSurvivedAmount;
    }

}
