using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningShieldScript : MonoBehaviour
{
    private LayerMask enemyLayers;
    private float projectileSpeed = 50f;

    private void Awake()
    {
        enemyLayers = LayerMask.NameToLayer("Enemy");
    }

    private void FixedUpdate()
    {
        transform.RotateAround(transform.parent.position, Vector3.back, projectileSpeed * Time.deltaTime);
        Destroy(gameObject, 6f);
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.layer == enemyLayers)
        {
            enemy.gameObject.GetComponent<Enemy>().EnemyTakeDamageFromPlayer();
        }
    }
}
