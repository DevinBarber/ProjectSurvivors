using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightningShieldScript : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] GameObject lightningShieldPrefab;
    private int projectileCount = 8;
    private float startAngle = 0f;
    private float endAngle = 360f;
    private bool isUsingAbility = false;

    private void FixedUpdate()
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerLightningShieldRoutine());
        }
    }

    private IEnumerator PlayerLightningShieldRoutine()
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetPlayerFireRate());

        float angleStep = (endAngle - startAngle) / projectileCount;
        float angle = startAngle;

        for (int i = 0; i < projectileCount; i++)
        {
            float projectileDirX = transform.position.x + (Mathf.Sin((angle * Mathf.PI) / 180f) * 2.5f);
            float projectileDirY = transform.position.y + (Mathf.Cos((angle * Mathf.PI) / 180f) * 2.5f);

            Vector3 projectileMoveVector = new Vector3(projectileDirX, projectileDirY, 0);

            GameObject newlightningShieldPrefab = Instantiate(lightningShieldPrefab, projectileMoveVector, transform.rotation);
            newlightningShieldPrefab.transform.SetParent(this.transform, true);

            angle += angleStep;
        }

        yield return new WaitForSeconds(6f);

        isUsingAbility = false;
    }
}
