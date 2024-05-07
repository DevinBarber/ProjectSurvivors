using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostboltScript : MonoBehaviour
{
    private LayerMask enemyLayers;
    private Vector2 moveDir;
    private float projectileSpeed = 10f;

    private void Awake()
    {
        enemyLayers = LayerMask.NameToLayer("Enemy");
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDir * projectileSpeed * Time.deltaTime);
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

    public void SetMoveDireciton(Vector2 dir)
    {
        moveDir = dir;
    }
}
