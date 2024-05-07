using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightningBoltScript : MonoBehaviour
{
    [SerializeField] GameObject lightingBoltPrefab;
    [SerializeField] GameObject player;
    [SerializeField] CalculateClosestEnemyScript calculateClosestEnemyScript;
    [SerializeField] float castSpeed = 10f;
    private bool isUsingAbility = false;

    private void FixedUpdate() //Maybe should be fixed update, look at after MAYBE RELOOK AT THIS AT SOME POINT
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerLightningBoltRoutine());
        }
    }
    private IEnumerator PlayerLightningBoltRoutine()
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetComponent<PlayerScript>().GetPlayerFireRate());

        Transform closestEnemy = calculateClosestEnemyScript.FindClosestEnemy();

        if(closestEnemy != null)
        {
            Vector3 dir = (closestEnemy.position - player.transform.position).normalized;
            float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, rotZ);

            GameObject newLightingBoltPrefab = Instantiate(lightingBoltPrefab, player.transform.position, rot);
            Rigidbody2D rb = newLightingBoltPrefab.GetComponent<Rigidbody2D>();

            rb.AddForce(newLightingBoltPrefab.transform.right * castSpeed, ForceMode2D.Impulse);
        }

        isUsingAbility = false;
    }
}
