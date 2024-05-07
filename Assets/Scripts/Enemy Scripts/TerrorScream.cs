using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorScream : MonoBehaviour
{
    public PlayerMovementScript playerMovement;
    public PlayerAttackScript playerAttack;
    public float fearDuration;
    public float targetScale = 4f;
    public float timeToLerp = 0.75f;
    float scaleModifier = 1;
    void Start()
    {
        StartCoroutine(ScaleLerp(targetScale, timeToLerp));
    }

    IEnumerator ScaleLerp(float endValue,float duration)
    {
        float time = 0;
        float startValue = scaleModifier;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            transform.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = startScale * endValue;
        scaleModifier = endValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == collision.CompareTag("Player"))
        {
            playerMovement.StartCoroutine(playerMovement.FearPlayerFromVoidTerror(fearDuration));
            playerAttack.StartCoroutine(playerAttack.FearPlayerFromVoidTerrorAA(fearDuration));
            Destroy(gameObject);
        }
    }
}
