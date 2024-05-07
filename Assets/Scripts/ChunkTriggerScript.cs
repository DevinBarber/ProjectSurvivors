using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTriggerScript : MonoBehaviour
{
    public GameObject targetChunk;
    public GameObject rightObj;
    public GameObject leftObj;
    public GameObject upObj;
    public GameObject downObj;
    public GameObject upRightObj;
    public GameObject upLeftObj;
    public GameObject downLeftObj;
    public GameObject downRightObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == collision.CompareTag("Player"))
        {
            ChunkGeneratorScript.instance.currentChunk = targetChunk;
            ChunkGeneratorScript.instance.rightObj = rightObj;
            ChunkGeneratorScript.instance.leftObj = leftObj;
            ChunkGeneratorScript.instance.upObj = upObj;
            ChunkGeneratorScript.instance.downObj = downObj;
            ChunkGeneratorScript.instance.upRightObj = upRightObj;
            ChunkGeneratorScript.instance.upLeftObj = upLeftObj;
            ChunkGeneratorScript.instance.downLeftObj = downLeftObj;
            ChunkGeneratorScript.instance.downRightObj = downRightObj;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == collision.CompareTag("Player"))
        {
            if(ChunkGeneratorScript.instance.currentChunk == targetChunk)
            {
                ChunkGeneratorScript.instance.currentChunk = null;
            }
        }
    }
}
