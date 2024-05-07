using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrostboltScript : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject frostboltPrefab;
    private int projectileCount = 1;
    private float startAngle = 60f;
    private float endAngle = 120f;
    private bool isUsingAbility = false;

    private void FixedUpdate()
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerFrostboltRoutine());
        }
    }

    private IEnumerator PlayerFrostboltRoutine()
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetPlayerFireRate());

        float angleStep = (endAngle - startAngle) / projectileCount;
        float angle = startAngle;

        for(int i = 0; i < projectileCount + 1; i++)
        {
            float projectileDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirY = firePoint.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 projectileMoveVector = new Vector3(projectileDirX, projectileDirY, 0);
            Vector2 projectileDirection = (projectileMoveVector -  firePoint.position).normalized;

            GameObject newFrostboltPrefab = Instantiate(frostboltPrefab, firePoint.position, firePoint.rotation);
            newFrostboltPrefab.GetComponent<FrostboltScript>().SetMoveDireciton(projectileDirection);
            angle += angleStep;
        }

        isUsingAbility = false;
    }
}
