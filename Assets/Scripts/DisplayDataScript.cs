using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayDataScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private void Awake()
    {
        currencyText.text = PlayerDataScript.totalPlayerCurrency.ToString();
    }
}
