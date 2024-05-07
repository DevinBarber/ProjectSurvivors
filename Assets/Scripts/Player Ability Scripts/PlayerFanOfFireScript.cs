using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFanOfFireScript : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] GameObject player;
    [SerializeField] CalculateCircleScript firePositions;
    [SerializeField] Vector3[] firePoints;
    [SerializeField] float castSpeed = 10f;
    private bool isUsingAbility = false;


    private void FixedUpdate() //Maybe should be fixed update, look at after MAYBE RELOOK AT THIS AT SOME POINT
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerFanOfFireRoutine());
        }
    }

    private IEnumerator PlayerFanOfFireRoutine()
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetComponent<PlayerScript>().GetPlayerFireRate());

        firePoints = firePositions.GetVerticePositions();

        for(int i = 0; i < firePoints.Length; i++)
        {
            Vector3 dir = (firePoints[i] - player.transform.position).normalized;
            float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, rotZ);

            GameObject newFireBall = Instantiate(fireballPrefab, firePoints[i], rot);
            Rigidbody2D rb = newFireBall.GetComponent<Rigidbody2D>();

            rb.AddForce(newFireBall.transform.right * castSpeed, ForceMode2D.Impulse);
        }

        isUsingAbility = false;
    }
}
