using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScanScript : MonoBehaviour
{
    float updateTimer = 5f;
    bool readyToScan;

    private void Start()
    {
        readyToScan = true;
    }

    private void Update()
    {
        if (readyToScan)
        {
            StartCoroutine(TestUpdate());
        }
    }

    private IEnumerator TestUpdate()
    {
        readyToScan = false;
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
        yield return new WaitForSeconds(updateTimer);
        readyToScan = true;
    }
}
