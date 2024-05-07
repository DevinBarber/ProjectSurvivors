using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneHelixAoEScript : MonoBehaviour
{
    private Color32 color;

    private void Start()
    {
        color = this.GetComponent<SpriteRenderer>().material.color;
        StartCoroutine(ArcaneHelixAoERoutine());
    }

    private IEnumerator ArcaneHelixAoERoutine()
    {
        yield return new WaitForSeconds(.1f);


        for (float alpha = 255f; alpha >= 0; alpha -= 1f) //If 1 is changed the object doesnt get destroyed, fix at somepoint
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
