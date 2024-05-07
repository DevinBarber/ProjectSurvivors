using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CalculateClosestEnemyScript))]
public class CalculateClosestEnemyScriptEditor : Editor
{
    private void OnSceneGUI()
    {
        CalculateClosestEnemyScript fow = (CalculateClosestEnemyScript)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.right, 360, fow.closestEnemyRadius);
    }
}
