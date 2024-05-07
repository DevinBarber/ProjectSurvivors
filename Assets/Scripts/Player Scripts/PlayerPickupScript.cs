using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupScript : MonoBehaviour
{
    private float playerPickupRadius = 3f;
    private CircleCollider2D circleCollider;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = playerPickupRadius;
    }

    public float GetPlayerPickupRange()
    {
        return playerPickupRadius;
    }
    public void SetPlayerPickupRange(float nPlayerPickupRange)
    {
        //Current + ((inc/100) * base) or Current + ((inc/100) * current)
        playerPickupRadius = playerPickupRadius + ((nPlayerPickupRange / 100) * playerPickupRadius);
        circleCollider.radius = playerPickupRadius;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col == col.CompareTag("ExperienceObject"))
        {
            col.gameObject.GetComponent<ExperiencePointsScript>().playerCollected = true;
        }

        if (col == col.CompareTag("CurrencyObject"))
        {
            col.gameObject.GetComponent<CurrencyPickUpScript>().playerCollected = true;
        }
    }
}
