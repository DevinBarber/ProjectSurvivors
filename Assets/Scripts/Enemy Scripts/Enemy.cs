using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public PlayerScript player;
    public DamageTextSpawnScript damageTextPool;
    public EnemySpawnManager spawnManager;
    public string enemyName;
    public float enemyHealth;
    public int enemyDamage;
    public float enemyMovementSpeed;
    public float enemyAutoAttackSpeed;
    public float enemyAbilityAttackSpeed;
    public bool enemyCanAutoAttack;
    public bool enemyCanUseAbility;
    public bool killedByPlayer;
    public bool isBeingKnockedBack;
    private float distanceToRespawnEnemy = 20f;
    public float distanceToTarget;
    public Vector2 directionToTarget;
    public Rigidbody2D rb;

    private float smoothDampVelocity = 0.0f;
    private float smoothDampTime = 1.2f;
    protected bool currentlySmoothingMovement;

    private Vector3 damageTextOffset = new Vector3(0, 1, 0);
    public Action<Enemy> killEnemy;

    protected virtual void Start()
    {
        enemyName = enemyData.enemyName;
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        SetupEnemy();
    }

    protected virtual void Update()
    {

        distanceToTarget = Vector2.Distance(transform.position, player.transform.position);
        directionToTarget = (player.transform.position - transform.position).normalized;

        if (distanceToTarget >= distanceToRespawnEnemy)
        {
            spawnManager.ResetEnemyStatsAndPosition(this);
        }

        if (enemyHealth <= 0) 
        {
            killEnemy(this);
        }

        if (currentlySmoothingMovement)
        {
            enemyMovementSpeed = Mathf.SmoothDamp(enemyMovementSpeed, enemyData.movementSpeed, ref smoothDampVelocity, smoothDampTime);

            if(enemyMovementSpeed == enemyData.movementSpeed)
            {
                currentlySmoothingMovement = false;
            }
        }
    }

    protected virtual void FixedUpdate()
    {

        if (!isBeingKnockedBack)
        {
            ChaseTarget(enemyMovementSpeed, directionToTarget);
        }
    }

    protected virtual IEnumerator EnemyAbilityCoroutine()
    {
        yield return null;
    }

    private void SetupEnemy()
    {
        enemyHealth = enemyData.health;
        enemyDamage = enemyData.damage;
        enemyMovementSpeed = enemyData.movementSpeed;
        enemyAutoAttackSpeed = enemyData.autoAttackSpeed;
        enemyAbilityAttackSpeed = enemyData.abilityAttackSpeed;
        enemyCanAutoAttack = true;
        enemyCanUseAbility = true;
        isBeingKnockedBack = false;
        currentlySmoothingMovement = false;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject == col.gameObject.CompareTag("Player"))
        {
            if (enemyCanAutoAttack)
            {
                StartCoroutine(EnemyAutoAttackPlayer(enemyDamage, enemyAutoAttackSpeed));
            }
        }
    }

    public IEnumerator KnockbackEnemy(Vector2 direction, float enemyKnockbackForce)
    {
        isBeingKnockedBack = true;
        enemyMovementSpeed = 0;
        rb.AddForce(direction * enemyKnockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        currentlySmoothingMovement = true;
        isBeingKnockedBack = false;
    }

    public IEnumerator EnemyAutoAttackPlayer(int eDamage, float eAttackSpeed)
    {
        enemyCanAutoAttack = false;
        player.TakeDamageFromEnemy(eDamage);
        yield return new WaitForSeconds(eAttackSpeed);
        enemyCanAutoAttack = true;
    }

    public void EnemyTakeDamageFromPlayer()
    {
        float playerDamage = player.CalculatePlayerDamage();
        bool didPlayerCrit;

        if (playerDamage > player.GetPlayerAbilityPower())
        {
            didPlayerCrit = true;
        }
        else
        {
            didPlayerCrit = false;
        }

        enemyHealth -= playerDamage;

        if (damageTextPool != null && damageTextPool.isActiveAndEnabled)
        {
            var damagePopUp = damageTextPool.Pool().Get();
            damagePopUp.transform.position = this.transform.position + damageTextOffset;
            damagePopUp.Setup(playerDamage, didPlayerCrit);
        }
    }

    public void ChaseTarget(float movementSpeed, Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
    }

    public void Init(Action<Enemy> _killEnemy)
    {
        killEnemy = _killEnemy;
    }
}
