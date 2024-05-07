using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Voidbeast : Enemy
{
    private bool withinAttackRange;
    private bool isCharging = false;
    private float attackRange = 7f;
    private float attackChargeUpTime = 0.5f;
    private float attackChargeSpeed = 10f;
    private float attackDuration = 1f;

    protected override void OnEnable()
    {
        base.OnEnable();
        isCharging = false;
        withinAttackRange = false;
    }

    protected override void Update()
    {
        base.Update();

        CheckIfInRangeOfPlayer();

        if (enemyCanUseAbility && withinAttackRange && !isBeingKnockedBack)
        {
            StartCoroutine(EnemyAbilityCoroutine());
        } 
    }

    protected override void FixedUpdate()
    {
        if (isCharging)
        {
            return;
        }

        if (!isBeingKnockedBack)
        {
            if (!withinAttackRange || (withinAttackRange && !enemyCanUseAbility))
            {
                ChaseTarget(enemyMovementSpeed, directionToTarget);
            }  
        }
    }

    private void CheckIfInRangeOfPlayer()
    {
        if (distanceToTarget <= attackRange)
        {
            withinAttackRange = true;
        }
        else
        {
            withinAttackRange = false;
        }
    }

    protected override IEnumerator EnemyAbilityCoroutine()
    {
        enemyCanUseAbility = false;
        isCharging = true;
        rb.velocity = Vector2.zero;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(attackChargeUpTime);

        rb.velocity = new Vector2(direction.x * attackChargeSpeed, direction.y * attackChargeSpeed);
        yield return new WaitForSeconds(attackDuration);
        currentlySmoothingMovement = true;
        isCharging = false;

        yield return new WaitForSeconds(enemyAbilityAttackSpeed);
        enemyCanUseAbility = true;
    }
}
