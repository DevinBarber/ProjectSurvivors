using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private LayerMask enemyLayer; 
    private LayerMask pillarLayer; 

    private void Awake()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        pillarLayer = LayerMask.NameToLayer("Pillar");
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if(enemy.gameObject.layer == enemyLayer)
        {
            enemy.gameObject.GetComponent<Enemy>().EnemyTakeDamageFromPlayer();
            Destroy(gameObject);
        }
        if (enemy.gameObject.layer == pillarLayer)
        {
            enemy.gameObject.GetComponent<GolemPillar>().PillarTakeDamageFromPlayer();
            Destroy(gameObject);
        }
    }

}
