using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBasicAttackScript : MonoBehaviour
{   
    
    public PlayerScript player;
    private float timeToDisable;

    private void OnEnable()
    {
        //Sets the time to disable the base attack to 0.5 based on players attack speed, can technically just set to 0.5f?
        timeToDisable = player.GetPlayerFireRate() / (player.GetPlayerFireRate()*2);
    }
    private void LateUpdate()
    {
        timeToDisable -= Time.deltaTime;
        if(timeToDisable < 0f)
        {
            this.gameObject.SetActive(false);
        }
    }

}
