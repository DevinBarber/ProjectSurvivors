using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcaneHelixScript : MonoBehaviour
{
    [SerializeField] GameObject arcaneHelixPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] PlayerScript player;
    [SerializeField] CalculateClosestEnemyScript calculateEnemiesInRange;
    private bool isUsingAbility = false;

    private void FixedUpdate() //Maybe should be fixed update, look at after MAYBE RELOOK AT THIS AT SOME POINT
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerArcaneHelixRoutine());
        }
    }
    private IEnumerator PlayerArcaneHelixRoutine()
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetPlayerFireRate());

        GameObject newArcaneHelixPrefab1 = Instantiate(arcaneHelixPrefab, firePoint.position, firePoint.rotation);
        newArcaneHelixPrefab1.GetComponent<ArcaneHelixScript>().enemiesInRange = calculateEnemiesInRange;
        newArcaneHelixPrefab1.GetComponent<ArcaneHelixScript>().either = true;
        GameObject newArcaneHelixPrefab2 = Instantiate(arcaneHelixPrefab, firePoint.position, firePoint.rotation);
        newArcaneHelixPrefab2.GetComponent<ArcaneHelixScript>().enemiesInRange = calculateEnemiesInRange;
        newArcaneHelixPrefab2.GetComponent<ArcaneHelixScript>().either = false;



        isUsingAbility = false;
    }
}
