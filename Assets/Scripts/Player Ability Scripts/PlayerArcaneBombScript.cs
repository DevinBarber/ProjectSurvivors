using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcaneBombScript : MonoBehaviour
{
    [SerializeField] GameObject arcaneBombPrefab;
    [SerializeField] GameObject arcaneBombAoEPrefab;
    [SerializeField] PlayerScript player;
    [SerializeField] CalculateClosestEnemyScript calculateEnemiesInRange;
    [SerializeField] float arcaneBombRadius = 2f; //Needs to match prefab not sure how to do yet
    private float arcaneBombSetOffTimer = 3f; 
    private bool isUsingAbility = false;

    private void FixedUpdate() //Maybe should be fixed update, look at after MAYBE RELOOK AT THIS AT SOME POINT
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerArcaneBombRoutine());
        }
    }
    private IEnumerator PlayerArcaneBombRoutine()
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetPlayerFireRate());

        GameObject newArcaneBombPrefab = Instantiate(arcaneBombPrefab, transform.position, Quaternion.identity);
        Vector3 arcaneBombPos = newArcaneBombPrefab.transform.position;

        yield return new WaitForSeconds(arcaneBombSetOffTimer);

        Destroy(newArcaneBombPrefab);
        Collider2D[] enemies = calculateEnemiesInRange.EnemiesInRange(arcaneBombPos, arcaneBombRadius);

        GameObject newArcaneBombAoEPrefab = Instantiate(arcaneBombAoEPrefab, arcaneBombPos, Quaternion.identity);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().EnemyTakeDamageFromPlayer();
        }

        isUsingAbility = false;
    }
}
