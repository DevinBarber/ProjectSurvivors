using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeteorScript : MonoBehaviour
{
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] GameObject meteorAoEPrefab;
    [SerializeField] PlayerScript player;
    [SerializeField] Camera mainCamera;
    private float camHeight;
    private float camWidth;
    private bool isUsingAbility = false;

    private void Start()
    {
        camHeight = mainCamera.orthographicSize;
        camWidth = mainCamera.orthographicSize * mainCamera.aspect;
    }

    private void FixedUpdate() //Maybe should be fixed update, look at after MAYBE RELOOK AT THIS AT SOME POINT
    {
        if (!isUsingAbility)
        {
            StartCoroutine(PlayerMeteorRoutine());
        }
    }

    private Vector3 GenerateRandomMeteorSpawnPosition()
    {
        Vector3 meteorPos = new Vector3();

        meteorPos.x = Random.Range(-camWidth, camWidth);
        meteorPos.y = Random.Range(-camHeight, camHeight);
        meteorPos.z = 1;

        return meteorPos;
    }

    private IEnumerator PlayerMeteorRoutine()
    {
        isUsingAbility = true;

        yield return new WaitForSeconds(player.GetPlayerFireRate());

        Vector3 spawnLoc = GenerateRandomMeteorSpawnPosition();
        spawnLoc += transform.position;

        GameObject newMeteorPrefab = Instantiate(meteorPrefab, spawnLoc, Quaternion.identity);


        yield return newMeteorPrefab.GetComponent<MeteorScript>().PlayerMeteorFallRoutine();

        GameObject newMeteorAoEPrefab = Instantiate(meteorAoEPrefab, newMeteorPrefab.transform.position, Quaternion.identity);
        Destroy(newMeteorPrefab);
        isUsingAbility = false;

    }
}
