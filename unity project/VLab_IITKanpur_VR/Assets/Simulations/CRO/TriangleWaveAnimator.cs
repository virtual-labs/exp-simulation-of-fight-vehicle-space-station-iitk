using UnityEngine;

public class TriangleWaveAnimator : MonoBehaviour
{
    public int numberOfPoints = 100;
    public float waveLength = 2f;
    public float amplitude = 1f;
    public float speed = 1f;
    public float verticalOffset = 0f;
    public float horizontalOffset = 0f;

    private Vector3[] points;
    private float timeOffset;
    private LineRenderer lineRenderer;

    void Start()
    {
        points = new Vector3[numberOfPoints];

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.useWorldSpace = false;

        UpdateWavePoints();
    }

    void Update()
    {
        timeOffset += Time.deltaTime * speed;
        UpdateWavePoints();
    }

    private void UpdateWavePoints()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            float x = (i * waveLength / numberOfPoints) - (waveLength * 0.5f) + horizontalOffset;
            float phase = (x + timeOffset) / waveLength;
            float y = 2f * Mathf.Abs(2f * (phase - Mathf.Floor(phase + 0.5f))) - 1f;
            y *= amplitude;
            y += verticalOffset;

            points[i] = new Vector3(x, y, 0f);
        }

        lineRenderer.SetPositions(points);
    }
}