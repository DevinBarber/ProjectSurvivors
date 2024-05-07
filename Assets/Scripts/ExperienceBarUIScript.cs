using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarUIScript : MonoBehaviour
{
    public Slider slider;

    //Sets Max Expereince needed of Player to level *Max value of slider*
    public void SetSliderMaxExperience(float pExperience)
    {
        slider.maxValue = pExperience;
    }

    //Sets the Experience Bar to the current xp when gaining xp *Current value of slider*
    public void SetSliderExperience(float pExperience)
    {
        slider.value = pExperience;
    }
}
