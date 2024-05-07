using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class MeteorAoEScript : MonoBehaviour
{
    private LayerMask enemyLayers;
    private Color32 color;

    private void Awake()
    {
        enemyLayers = LayerMask.NameToLayer("Enemy");
    }

    private void Start()
    {
        color = this.GetComponent<SpriteRenderer>().material.color;
        StartCoroutine(PlayerMeteorAoeRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayers)
        {
            collision.gameObject.GetComponent<Enemy>().EnemyTakeDamageFromPlayer();
        }

    }

    private IEnumerator PlayerMeteorAoeRoutine()
    {
        yield return new WaitForSeconds(.1f);

        this.GetComponent<Collider2D>().enabled = false;

        for (float alpha = 255f; alpha >= 0; alpha -= 1f)
        {

            color.a = (byte)alpha;
            this.GetComponent<SpriteRenderer>().material.color = color;

            if(color.a <= 0)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
