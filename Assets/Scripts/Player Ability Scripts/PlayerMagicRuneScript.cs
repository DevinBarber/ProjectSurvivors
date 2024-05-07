using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicRuneScript : MonoBehaviour
{
    [SerializeField] GameObject magicRunePrefab;
    [SerializeField] PlayerScript player;
    [SerializeField] float magicRuneDamage = 5f;
    private Vector3 offset = new Vector3(0, -0.5f, 0);
    private float magicRuneDuration = 8f;
    private float playerBaseDamage;
    private bool isUsingAbility = false;

    private void Start()
    {
        playerBaseDamage = player.GetPlayerAbilityPower();
    }

    private void FixedUpdate() //Maybe should be fixed update, look at after MAYBE RELOOK AT THIS AT SOME POINT
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerFrostNovaRoutine());
        }
    }
    private IEnumerator PlayerFrostNovaRoutine()
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetPlayerFireRate());

        GameObject newMagicRunePrefab = Instantiate(magicRunePrefab, transform.position + offset, Quaternion.identity);
        newMagicRunePrefab.GetComponent<MagicRuneScript>().SetParameters(magicRuneDuration, magicRuneDamage, player);

        yield return new WaitForSeconds(magicRuneDuration);

        player.SetPlayerAbilityPower(playerBaseDamage);

        isUsingAbility = false;
    }
}
