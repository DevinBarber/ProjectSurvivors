using UnityEngine;

public class DrawCircleScript : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public int resolution = 6;  
    public float radius = 1f;     

    void Start()
    {
        //DrawCircle();
    }

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
    }
}