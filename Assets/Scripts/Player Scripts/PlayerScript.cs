using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    private int playerStartHealth = 3; //CHANGE ONCE READ FROM TXT FILE
    private float playerMovementSpeed = 5f;
    private int playerMaxHealth = 0;
    private int playerCurrentHealth = 0;
    private float playerHealthPerSecond = 0.25f; //USE PLS
    private int playerArmor = 0;
    private float playerAbilityPower = 5f;
    private float playerFireRate = 1f; //Current - ((inc/100) * base) OR Current - ((inc/100) * current)
    private float playerProjectileSpeed = 10f;
    private int playerCriticalStrikeChance = 10; //Percentage
    private float playerCritDamageMultiplier = 1.5f;
    private float basePlayerCritDamageMultiplier = 1;
    private float playerExperience = 0f;
    private int playerLevel = 1;
    private float playerTotalCurrency = 0f;
    private bool playerIsDead = false;
    //Current + ((inc/100) * base) or Current + ((inc/100) * current)
    //Status effect damage

    private float enemyPushbackRadius = 5f;
    private float enemyPushbackForce = 10f;

    public HealthUIScript hearts;
    public GameManagerScript gameManager;
    public CalculateClosestEnemyScript enemiesWithinRange;

    private void Awake()
    {
        playerMaxHealth = playerStartHealth;
        playerCurrentHealth = playerStartHealth; 
    }

    void Update()
    {
        if(playerCurrentHealth <= 0 && !playerIsDead)
        {
            PlayerDied();
        }
    }

    public void TakeDamageFromEnemy(int damage)
    {
        int totalDamage = damage - playerArmor;

        if(totalDamage <= 0)
        {
            totalDamage = 0;
        }

        PushbackEnemies();
        playerCurrentHealth -= totalDamage;
        hearts.DrawHearts();
    }

    private void PushbackEnemies()
    {
        Collider2D[] enemies = enemiesWithinRange.EnemiesInRange(transform.position, enemyPushbackRadius);

        for(int i = 0; i < enemies.Length; i++)
        {
            Vector2 direction = (enemies[i].transform.position - transform.position).normalized;
            StartCoroutine(enemies[i].GetComponent<Enemy>().KnockbackEnemy(direction, enemyPushbackForce));
        }
    }

    public float CalculatePlayerDamage()
    {
        float playerDamage;

        bool didPlayerCrit = DidPlayerCriticallyStrike();

        if (didPlayerCrit)
        {
            playerDamage = (playerAbilityPower * playerCritDamageMultiplier);
        } else
        {
            playerDamage = playerAbilityPower;
        }

        return playerDamage;
    }

    public bool DidPlayerCriticallyStrike()
    {
        bool didYouCrit;

        int temp = Random.Range(1, 101); //Returns a number between 1-100

        if (temp <= playerCriticalStrikeChance)
        {
            didYouCrit = true;
        }
        else
        {
            didYouCrit = false;
        }

        return didYouCrit;
    }

    private void PlayerDied()
    {
        gameManager.GameOver();
        playerIsDead = true;
    }

    #region "Getters and Setters - Player Stats"
    public int GetPlayerStartHealth()
    {
        return playerStartHealth;
    }
    public void SetPlayerStartHealth(int nPlayerStartHealth)
    {
        playerStartHealth = nPlayerStartHealth;
    }
    public float GetPlayerMovementSpeed()
    {
        return playerMovementSpeed;
    }
    public void SetPlayerMovementSpeed(float percentInc)
    {
        playerMovementSpeed = playerMovementSpeed + ((percentInc / 100) * playerMovementSpeed); //Might need to change last float to base later on
    }
    public int GetPlayerMaxHealth()
    {
        return playerMaxHealth;
    }
    public void SetPlayerMaxHealth(int nPlayerMaxHealth)
    {
        playerMaxHealth = nPlayerMaxHealth;
    }
    public int GetPlayerCurrentHealth()
    {
        return playerCurrentHealth; 
    }
    public void SetPlayerCurrentHealth(int nPlayerCurrentHealth)
    {
        playerCurrentHealth = nPlayerCurrentHealth;
    }
    public float GetPlayerHealthPerSecond()
    {
        return playerHealthPerSecond;
    }
    public void SetPlayerHealthPerSecond(float nPlayerHealthPerSecond)
    {
        playerHealthPerSecond = nPlayerHealthPerSecond;
    }
    public int GetPlayerArmor()
    {
        return playerArmor;
    }
    public void SetPlayerArmor(int nPlayerArmor)
    {
        playerArmor = nPlayerArmor;
    }
    public float GetPlayerAbilityPower()
    {
        return playerAbilityPower;
    }
    public void SetPlayerAbilityPower(float inc)
    {
        playerAbilityPower = playerAbilityPower + ((inc / 100) * playerAbilityPower);

    }
    public float GetPlayerFireRate()
    {
        return playerFireRate;
    }
    public void SetPlayerFireRate(float inc)
    {
        //Current - ((inc/100) * base) OR Current - ((inc/100) * current)
        playerFireRate = playerFireRate - ((inc / 100) * playerFireRate);
    }
    public float GetPlayerProjectileSpeed()
    {
        return playerProjectileSpeed;
    }
    public void SetPlayerProjectileSpeed(float inc)
    {
        playerProjectileSpeed += playerProjectileSpeed;
    }
    public int GetPlayerCriticalStrikeChance()
    {
        return playerCriticalStrikeChance;
    }
    public void SetPlayerCriticalStrikeChance(int nPlayerCriticalStrikeChance)
    {
        playerCriticalStrikeChance += nPlayerCriticalStrikeChance;
    }
    public float GetPlayerCritDamageMultiplayer()
    {
        return playerCritDamageMultiplier;
    }
    public void SetPlayerCritDamageMultiplayer(float inc)
    {
        playerCritDamageMultiplier = playerCritDamageMultiplier + ((inc / 100) * basePlayerCritDamageMultiplier);
    }
    public float GetPlayerExperience()
    {
        return playerExperience;
    }
    public void SetPlayerExperience(float nPlayerExperience)
    {
        playerExperience = nPlayerExperience;
    }
    public int GetPlayerLevel()
    {
        return playerLevel;
    }
    public void SetPlayerLevel(int nPlayerLevel)
    {
        playerLevel = nPlayerLevel;
    }
    public float GetPlayerTotalCurrency()
    {
        return playerTotalCurrency;
    }
    public void SetPlayerTotalCurrency(float nPlayerTotalCurrency)
    {
        playerTotalCurrency = nPlayerTotalCurrency;
    }

    #endregion
}
