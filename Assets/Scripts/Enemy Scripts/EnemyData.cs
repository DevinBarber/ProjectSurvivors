using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/New Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float health;
    public int damage;
    public float movementSpeed;
    public float autoAttackSpeed;
    public float abilityAttackSpeed;
}