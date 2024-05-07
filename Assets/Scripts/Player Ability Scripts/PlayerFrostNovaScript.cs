using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrostNovaScript : MonoBehaviour
{
    [SerializeField] GameObject frostNovaPrefab;
    [SerializeField] PlayerScript player;
    [SerializeField] CalculateClosestEnemyScript calculateEnemiesInRange;
    [SerializeField] float frostNovaRadius = 3f;
    //[SerializeField] float frostNovaSlowPercent = 50f;
    [SerializeField] float frostNovaDuration = 3f;
    private Vector3 offset = new Vector3(0f,-0.5f,0f);
    private bool isUsingAbility = false;

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

        Collider2D[] enemies = calculateEnemiesInRange.EnemiesInRange(transform.position, frostNovaRadius);

        for(int i = 0; i < enemies.Length; i++)
        {
            //enemies[i].GetComponent<Enemy>().EnemySlowMovementSpeed(frostNovaSlowPercent); 
            GameObject newFrostNovaPrefab = Instantiate(frostNovaPrefab, enemies[i].transform.position + offset, Quaternion.identity);
            newFrostNovaPrefab.transform.SetParent(enemies[i].transform, true);
            newFrostNovaPrefab.GetComponent<FrostNovaScript>().frostNovaDuration = frostNovaDuration;
        }

        yield return new WaitForSeconds(frostNovaDuration);

        for (int i = 0; i < enemies.Length; i++)
        {
            //enemies[i].GetComponent<Enemy>().EnemyResetMovementSpeed();
        }

        isUsingAbility = false;
    }
}
