using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkGeneratorScript : MonoBehaviour
{
    public static ChunkGeneratorScript instance;

    //public List<GameObject> terrainChunks;
    public GameObject terrainChunk;
    public PlayerScript player;
    public PlayerMovementScript playerMovement;
    public float checkRadius;
    private Vector3 noTerrainPos;
    public LayerMask terrainMask;
    private Vector2 moveDir;
    public GameObject currentChunk;

    public GameObject rightObj;
    public GameObject leftObj;
    public GameObject upObj;
    public GameObject downObj;
    public GameObject upRightObj;
    public GameObject upLeftObj;
    public GameObject downLeftObj;
    public GameObject downRightObj;

    public List<GameObject> spawnedChunks;
    private GameObject latestChunk;
    public float maxDistance; //Must be greater than the l && w of the chunk
    float distance;
    float optCooldown;
    float optDuration;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = playerMovement.GetPlayerMoveDirection();
        CheckForChunk();
    }

    private void CheckForChunk()
    {
        if(!currentChunk)
        {
            return;
        }

        if(moveDir.x != 0 || moveDir.y != 0)
        {
            if (!Physics2D.OverlapCircle(rightObj.transform.position, checkRadius, terrainMask))
            {
                noTerrainPos = rightObj.transform.position;
                SpawnChunk();
            }
            if (!Physics2D.OverlapCircle(leftObj.transform.position, checkRadius, terrainMask))
            {
                noTerrainPos = leftObj.transform.position;
                SpawnChunk();
            }
            if (!Physics2D.OverlapCircle(upObj.transform.position, checkRadius, terrainMask))
            {
                noTerrainPos = upObj.transform.position;
                SpawnChunk();
            }
            if (!Physics2D.OverlapCircle(downObj.transform.position, checkRadius, terrainMask))
            {
                noTerrainPos = downObj.transform.position;
                SpawnChunk();
            }
            if (!Physics2D.OverlapCircle(upRightObj.transform.position, checkRadius, terrainMask))
            {
                noTerrainPos = upRightObj.transform.position;
                SpawnChunk();
            }
            if (!Physics2D.OverlapCircle(downRightObj.transform.position, checkRadius, terrainMask))
            {
                noTerrainPos = downRightObj.transform.position;
                SpawnChunk();
            }
            if (!Physics2D.OverlapCircle(upLeftObj.transform.position, checkRadius, terrainMask))
            {
                noTerrainPos = upLeftObj.transform.position;
                SpawnChunk();
            }
            if (!Physics2D.OverlapCircle(downLeftObj.transform.position, checkRadius, terrainMask))
            {
                noTerrainPos = downLeftObj.transform.position;
                SpawnChunk();
            }
        }

        ChunkOptimization();
    }

    private void SpawnChunk()
    {
        latestChunk = Instantiate(terrainChunk, noTerrainPos, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    private void ChunkOptimization()
    {
        optCooldown -= Time.deltaTime;

        if(optCooldown <= 0f)
        {
            optCooldown = optDuration;
        } else
        {
            return;
        }

        foreach(GameObject chunk in spawnedChunks)
        {
            distance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(distance > maxDistance)
            {
                chunk.SetActive(false);
            } else
            {
                chunk.SetActive(true);
            }
        }
    }
}
