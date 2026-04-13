using UnityEngine;

public class SineWaveAnimator : MonoBehaviour
{
    public int numberOfPoints = 100;     // Number of points in the sine wave
    public float waveLength = 2f;        // Length of one wave cycle
    public float amplitude = 1f;         // Height of the wave
    public float speed = 1f;             // Speed of the wave movement
    public float verticalOffset = 0f;    // Y-position offset (prevents underground rendering)
    public float horizontalOffset = 0f;  // X-position offset (centers the wave)

    private Vector3[] points;            // Array to hold the points of the sine wave
    private float timeOffset;            // Offset for wave movement
    private LineRenderer lineRenderer;   // Reference to the LineRenderer

    void Start()
    {
        // Initialize points
        points = new Vector3[numberOfPoints];

        // Get (or add) the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null) 
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configure LineRenderer
        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.useWorldSpace = false; // Use local space for easier positioning

        UpdateWavePoints();
    }

    void Update()
    {
        // Update the wave offset based on time and speed
        timeOffset += Time.deltaTime * speed;
        UpdateWavePoints();
    }

    private void UpdateWavePoints()
    {
        // Update positions of all points
        for (int i = 0; i < numberOfPoints; i++)
        {
                        float x = (i * waveLength / numberOfPoints) - (waveLength * 0.5f) + horizontalOffset;
                        float y = Mathf.Sin((x + timeOffset) * Mathf.PI * 2f / waveLength) * amplitude + verticalOffset;
                        points[i] = new Vector3(x, y, 0f);
                    }
            
                    // Apply the calculated points to the LineRenderer
                    lineRenderer.SetPositions(points);
                }
            }