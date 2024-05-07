using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPillar : MonoBehaviour
{
    public Collider2D fullCol;
    public Collider2D baseCol;
    public int pillarDamage;
    public float pillarHealth;
    public PlayerScript player;
    public DamageTextSpawnScript damageTextPool;
    private Vector3 damageTextOffset = new Vector3(0, 1, 0);
    Color currentPillarAlpha;
    private int objectsInTrigger;
    private bool noLongerDamagePlayer;


    private void OnEnable()
    {
        baseCol.enabled = false;
        fullCol.enabled = true;
    }

    private void Start()
    {
        noLongerDamagePlayer = false;
        objectsInTrigger = 0;
        currentPillarAlpha = GetComponent<SpriteRenderer>().color;
        pillarHealth = 25f;
        StartCoroutine(TiggerToColliderTimer());
    }

    private void Update()
    {
       if(pillarHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        objectsInTrigger++;

        if (col == col.CompareTag("Player") && !noLongerDamagePlayer)
        {
            col.gameObject.GetComponent<PlayerScript>().TakeDamageFromEnemy(pillarDamage);
        }

        currentPillarAlpha.a = 0.75f;
        this.GetComponent<SpriteRenderer>().color = currentPillarAlpha;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsInTrigger--;

        if (objectsInTrigger == 0)
        {
            currentPillarAlpha.a = 1f;
            this.GetComponent<SpriteRenderer>().color = currentPillarAlpha;
        }
    }

    public IEnumerator TiggerToColliderTimer()
    {
        yield return new WaitForSeconds(0.5f);
        noLongerDamagePlayer = true;
        baseCol.enabled = true;
        
    }

    public void PillarTakeDamageFromPlayer()
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

        pillarHealth -= playerDamage;

        if (damageTextPool != null && damageTextPool.isActiveAndEnabled)
        {
            var damagePopUp = damageTextPool.Pool().Get();
            damagePopUp.transform.position = this.transform.position + damageTextOffset;
            damagePopUp.Setup(playerDamage, didPlayerCrit);
        }
    }
}
