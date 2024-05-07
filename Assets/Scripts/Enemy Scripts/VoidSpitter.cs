using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoidSpitter : Enemy
{
    [SerializeField] GameObject spitPrefab;
    private bool withinAttackRange;
    private float attackRange = 6f;
    private float projectileSpeed = 2f;

    protected override void OnEnable()
    {
        base.OnEnable();
        withinAttackRange = false;
    }

    protected override void Update()
    {
        base.Update();

        CheckIfInRangeOfPlayer();

        if (withinAttackRange)
        {
            enemyMovementSpeed = 0;
        } else
        {
            enemyMovementSpeed = enemyData.movementSpeed;
        }

        if (enemyCanUseAbility && withinAttackRange && !isBeingKnockedBack)
        {
            StartCoroutine(EnemyAbilityCoroutine());
        }
    }

    private void CheckIfInRangeOfPlayer()
    {
        if(distanceToTarget <= attackRange)
        {
            withinAttackRange = true; 
        } else
        {
            withinAttackRange = false;
        }
    }

    protected override IEnumerator EnemyAbilityCoroutine()
    {
        enemyCanUseAbility = false;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        GameObject newSpitShot = Instantiate(spitPrefab, transform.position, Quaternion.identity);
        newSpitShot.GetComponent<SpitShot>().spitShotDamage = enemyDamage;
        Rigidbody2D rb = newSpitShot.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(enemyAbilityAttackSpeed);
        enemyCanUseAbility = true;
    }
}
