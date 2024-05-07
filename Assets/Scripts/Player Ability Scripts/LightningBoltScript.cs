using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltScript : MonoBehaviour
{
    private LayerMask enemyLayers;

    private void Awake()
    {
        enemyLayers = LayerMask.NameToLayer("Enemy");
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.layer == enemyLayers)
        {
            enemy.gameObject.GetComponent<Enemy>().EnemyTakeDamageFromPlayer();
            Destroy(gameObject);
        }
    }

}
