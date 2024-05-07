using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidgolem : Enemy
{
    [SerializeField] GameObject pillarPrefab;
    private bool withinAttackRange;
    private bool isAttacking = false;
    private float attackRange = 6f;
    private float attackChargeUpTime = 1f;
    private float attackDuration = 0.5f;

    protected override void OnEnable()
    {
        base.OnEnable();
        isAttacking = false;
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
        if (isAttacking)
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
        Vector2 currentPlayerLocation;
        enemyCanUseAbility = false;
        isAttacking = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(attackChargeUpTime);

        currentPlayerLocation = player.transform.position;
        yield return new WaitForSeconds(attackDuration);
        GameObject newPillar = Instantiate(pillarPrefab, currentPlayerLocation, Quaternion.identity);
        newPillar.GetComponent<GolemPillar>().pillarDamage = enemyDamage;
        newPillar.GetComponent<GolemPillar>().player = player;
        newPillar.GetComponent<GolemPillar>().damageTextPool = damageTextPool;
        currentPlayerLocation = player.transform.position;
        yield return new WaitForSeconds(attackDuration);
        GameObject newPillar1 = Instantiate(pillarPrefab, currentPlayerLocation, Quaternion.identity);
        newPillar1.GetComponent<GolemPillar>().pillarDamage = enemyDamage;
        newPillar1.GetComponent<GolemPillar>().player = player;
        newPillar1.GetComponent<GolemPillar>().damageTextPool = damageTextPool;
        currentPlayerLocation = player.transform.position;
        yield return new WaitForSeconds(attackDuration);
        GameObject newPillar2 = Instantiate(pillarPrefab, currentPlayerLocation, Quaternion.identity);
        newPillar2.GetComponent<GolemPillar>().pillarDamage = enemyDamage;
        newPillar2.GetComponent<GolemPillar>().player = player;
        newPillar2.GetComponent<GolemPillar>().damageTextPool = damageTextPool;
        currentlySmoothingMovement = true;
        isAttacking = false;

        yield return new WaitForSeconds(enemyAbilityAttackSpeed);
        enemyCanUseAbility = true;
    }
}
