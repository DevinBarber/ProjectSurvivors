using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarsUIScript : MonoBehaviour
{
    public Slider slider;

    //Sets Max Health of Player Health Bar *Max value of slider*
    public void SetSliderMaxHealth(float pHealth)
    {
        slider.maxValue = pHealth;
        slider.value = pHealth;
    }

    //Sets the Health Bar to the current health when taking damage *Current value of slider*
    public void SetSliderHealth(float pHealth)
    {
        slider.value = pHealth;
    }
}
