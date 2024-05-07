using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBombAoEScript : MonoBehaviour
{
    private Color32 color;

    private void Start()
    {
        color = this.GetComponent<SpriteRenderer>().material.color;
        StartCoroutine(PlayerArcaneBombAoERoutine());
    }

    private IEnumerator PlayerArcaneBombAoERoutine()
    {
        yield return new WaitForSeconds(.1f);

        for (float alpha = 255f; alpha >= 0; alpha -= 1f)
        {

            color.a = (byte)alpha;
            this.GetComponent<SpriteRenderer>().material.color = color;

            if (color.a <= 0)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
