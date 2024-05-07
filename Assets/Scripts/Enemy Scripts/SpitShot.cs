using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitShot : MonoBehaviour
{
    public int spitShotDamage;

    private void Update()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col == col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerScript>().TakeDamageFromEnemy(spitShotDamage);
            Destroy(gameObject);
        }
    }
}
