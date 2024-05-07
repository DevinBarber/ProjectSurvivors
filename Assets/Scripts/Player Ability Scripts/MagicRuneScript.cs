using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRuneScript : MonoBehaviour
{
    public PlayerScript player;
    public float runeDuration;
    public float damageIncrease;

    private void Update()
    {
        Destroy(gameObject, runeDuration);
    }

    public void SetParameters(float rDur, float dInc, PlayerScript p)
    {
        runeDuration = rDur;
        damageIncrease = dInc;
        player = p;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.SetPlayerAbilityPower(player.GetPlayerAbilityPower() + damageIncrease);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SetPlayerAbilityPower(player.GetPlayerAbilityPower() - damageIncrease);
        }
    }
}
