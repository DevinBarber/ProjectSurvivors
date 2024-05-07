using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneHelixScript : MonoBehaviour
{
    [SerializeField] GameObject arcaneHelixAoEPrefab;
    public CalculateClosestEnemyScript enemiesInRange;
    private LayerMask enemyLayers;

    private Vector3 startPos;
    private float frequency = 7f;
    private float magnitude = 1.5f;
    private float projectileSpeed = 5f;
    public bool either;

    private void Awake()
    {
        enemyLayers = LayerMask.NameToLayer("Enemy");
    }

    private void Start()
    {
        startPos = transform.position;
        StartCoroutine(ProjectileTimerRoutine());
    }

    private void FixedUpdate()
    {
        if (either)
        {
            startPos += transform.right * Time.deltaTime * projectileSpeed;
            transform.position = startPos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        } else
        {
            startPos += transform.right * Time.deltaTime * projectileSpeed;
            transform.position = startPos + transform.up * Mathf.Sin(Time.time * frequency) * -magnitude;
        }
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.layer == enemyLayers)
        {
            Collider2D[] enemies = enemiesInRange.EnemiesInRange(transform.position, 1);
            GameObject newArcaneHelixAoE = Instantiate(arcaneHelixAoEPrefab, transform.position, Quaternion.identity);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().EnemyTakeDamageFromPlayer();
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator ProjectileTimerRoutine()
    {
        yield return new WaitForSeconds(2f);
        Collider2D[] enemies = enemiesInRange.EnemiesInRange(transform.position, 1);
        GameObject newArcaneHelixAoE = Instantiate(arcaneHelixAoEPrefab, transform.position, Quaternion.identity);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().EnemyTakeDamageFromPlayer();
        }

        Destroy(gameObject);
    }

}
