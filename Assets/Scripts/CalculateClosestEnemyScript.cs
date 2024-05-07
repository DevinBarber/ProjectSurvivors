using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateClosestEnemyScript : MonoBehaviour
{
    public float closestEnemyRadius = 5f;
    [SerializeField] LayerMask enemyLayer;

    public Transform FindClosestEnemy()
    {
        Transform closestTarget = null;
        float closestEnemyDistanceSqr = Mathf.Infinity;
        Collider2D[] enemiesWithinRange = Physics2D.OverlapCircleAll(transform.position, closestEnemyRadius, enemyLayer);

       for(int i = 0; i < enemiesWithinRange.Length; i++)
        {
            Transform target = enemiesWithinRange[i].transform;
            Vector3 dirToTarget = target.position - transform.position;
            float distance = dirToTarget.sqrMagnitude;

            if(distance < closestEnemyDistanceSqr)
            {
                closestEnemyDistanceSqr = distance;
                closestTarget = target;
            }
        }

       return closestTarget;

    }

    public Collider2D[] EnemiesInRange(Vector3 pos, float radius)
    {
        Collider2D[] enemiesWithinRange = Physics2D.OverlapCircleAll(pos, radius, enemyLayer);

        return enemiesWithinRange;
    }



    public Vector3 completeCircle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees* Mathf.Deg2Rad));
    }

}
