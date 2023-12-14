using UnityEngine;

public class CircleLineRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float radiusMultiplier = 1f;
    public int vertexCount = 100;
    public Color circleColor = Color.white;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.loop = true; // 设置为true以闭合圆环
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    private void Update()
    {
        SetCircle();
    }

    private void SetCircle()
    {
        float radius = CalculateRadius();
        lineRenderer.positionCount = vertexCount;
        Vector3[] positions = new Vector3[vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            float angle = i * (2 * Mathf.PI / vertexCount);
            float xPos = radius * Mathf.Cos(angle);
            float yPos = radius * Mathf.Sin(angle);
            positions[i] = new Vector3(xPos, yPos, 0); // Z轴值设置为0
        }

        lineRenderer.SetPositions(positions);
        lineRenderer.material.color = circleColor;
    }

    private float CalculateRadius()
    {
        Vector3 objectScale = transform.localScale;
        float maxScale = Mathf.Max(objectScale.x, objectScale.y);
        float radius = maxScale * radiusMultiplier;
        return radius;
    }
}