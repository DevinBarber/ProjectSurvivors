using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public bool reachedDestination = false;
    public Vector3 destination = Vector3.zero;

    private void Start()
    {
        destination = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);
        StartCoroutine(PlayerMeteorFallRoutine());
    }

    public IEnumerator PlayerMeteorFallRoutine()
    {
        while(Vector2.Distance(transform.position, destination) > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position,destination, 2f * Time.deltaTime);
            yield return null;
        }
    }
}
