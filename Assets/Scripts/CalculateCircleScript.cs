using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class CalculateCircleScript : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public int resolution = 6;
    public float radius = 1f;
    private Vector3[] verticePos;
    private bool arraySet = false;

    private void Update()
    {
        DrawCircle();
    }

    void DrawCircle()
    {
        circleRenderer.loop = true;
        circleRenderer.positionCount = resolution;

        float angle = 0f;

        for (int i = 0; i < resolution; i++)
        {
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            circleRenderer.SetPosition(i, new Vector3(x, y, 0f));

            angle += 2f * Mathf.PI / resolution;
        }

        if (!arraySet)
        {
            verticePos = new Vector3[circleRenderer.positionCount];
            arraySet = true;
        }
    }

    public Vector3[] GetVerticePositions()
    {
        circleRenderer.GetPositions(verticePos);

        for(int i = 0; i < verticePos.Length; i++)
        {
            Transform thisPos = this.transform;
            Vector3 tempPoint = verticePos[i];

            Vector3 newPoint = thisPos.TransformPoint(tempPoint);
            verticePos[i] = newPoint;
        }

        return verticePos;
    }

}