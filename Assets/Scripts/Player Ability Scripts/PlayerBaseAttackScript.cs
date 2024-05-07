using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseAttackScript : MonoBehaviour
{
    public PlayerScript player;
    private float nextAttackTime = 0f;
    public GameObject baseAttack;
    public Vector2 basicAttackSize = new Vector2(1f,1f);
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            BaseAttack();
        }
    }

    private void BaseAttack()
    {
        baseAttack.SetActive(true);
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(baseAttack.transform.position, basicAttackSize, 0f, enemyLayers);
        EnemiesHit(hitEnemies);

        nextAttackTime = Time.time + 1f / player.GetPlayerFireRate();
    }

    private void EnemiesHit(Collider2D[] hitEnemies)
    {
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().EnemyTakeDamageFromPlayer();
        }
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(baseAttack.transform.position, basicAttackSize);
    }*/

}