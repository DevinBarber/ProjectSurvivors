using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicArmorScript : MonoBehaviour
{
    [SerializeField] GameObject magicArmorPrefab;
    [SerializeField] PlayerScript player;
    private float magicArmorDuration = 5f;
    private int magicArmor = 1;
    private bool isUsingAbility = false;

    private void FixedUpdate() //Maybe should be fixed update, look at after MAYBE RELOOK AT THIS AT SOME POINT
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerFrostNovaRoutine());
        }
    }
    private IEnumerator PlayerFrostNovaRoutine() //NOTE TO FIX FOR ALL ABILITIES ITS WAITING 2 SECONDS AFTER GETTING ABILITY THEN WAITING LONGER *CHECK COOLDOWNS ETC*
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetPlayerFireRate());

        GameObject newMagicArmorPrefab = Instantiate(magicArmorPrefab, transform.position, Quaternion.identity);
        newMagicArmorPrefab.transform.SetParent(this.transform, true);
        player.SetPlayerArmor(player.GetPlayerArmor() + magicArmor);

        yield return new WaitForSeconds(magicArmorDuration);

        Destroy(newMagicArmorPrefab);
        player.SetPlayerArmor(player.GetPlayerArmor() - magicArmor);
        isUsingAbility = false;
    }
}
