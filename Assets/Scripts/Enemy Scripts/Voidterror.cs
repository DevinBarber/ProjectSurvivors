using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidterror : Enemy
{
    [SerializeField] GameObject fearPrefab;
    private bool withinAttackRange;
    private float attackRange = 3f;
    private float fearDuration = 8f;

    protected override void OnEnable()
    {
        base.OnEnable();
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
        GameObject newFearAbility = Instantiate(fearPrefab, transform.position, Quaternion.identity);
        newFearAbility.GetComponent<TerrorScream>().fearDuration = fearDuration;
        newFearAbility.GetComponent<TerrorScream>().playerMovement = player.GetComponent<PlayerMovementScript>();
        newFearAbility.GetComponent<TerrorScream>().playerAttack = player.GetComponent<PlayerAttackScript>();
        yield return new WaitForSeconds(enemyAbilityAttackSpeed);
        killEnemy(this);
    }
}
