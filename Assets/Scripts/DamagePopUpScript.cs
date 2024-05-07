using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamagePopUpScript : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Action<DamagePopUpScript> removeText;
    private float textMoveSpeed = 0.35f;
    float dissapearSpeed = 8f;
    private float dissapearTimer;
    private Color startColor;
    private Color critColor = new Color32(183, 59, 0, 255);
    private Color normalColor = new Color32(255, 255, 255, 255);

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(float playerDamage, bool wasCriticalStrike)
    {
        textMesh.text = playerDamage.ToString();

        if(!wasCriticalStrike) //Not a critical strike
        {
            textMesh.fontSize = 26;
            startColor = normalColor;
        } else
        {
            textMesh.fontSize = 32;
            startColor = critColor;
        }

        textMesh.color = startColor;
        dissapearTimer = 1f;
    }

    private void Update()
    {
        transform.position += new Vector3(0, textMoveSpeed, 0) * Time.deltaTime;
        dissapearTimer -= Time.deltaTime;

        if(dissapearTimer < 0)
        {
            startColor.a -= dissapearSpeed * Time.deltaTime;
            textMesh.color = startColor;

            if(startColor.a < 0)
            {
                removeText(this);
            }
        }
    }

    public void Init(Action<DamagePopUpScript> _removeText)
    {
        removeText = _removeText;
    }
}
