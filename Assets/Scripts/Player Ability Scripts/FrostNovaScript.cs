using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostNovaScript : MonoBehaviour
{
    public float frostNovaDuration;
    private void Update()
    {
        Destroy(gameObject, frostNovaDuration);
    }

}
