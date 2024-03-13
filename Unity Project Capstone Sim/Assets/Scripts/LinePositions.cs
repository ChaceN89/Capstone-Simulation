using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePositions : MonoBehaviour
{
    public GameObject object1; // Assign in inspector or through code
    public GameObject object2; // Assign in inspector or through code
    public GameObject object3; // Assign in inspector or through code

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get or add a LineRenderer component
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Setup the LineRenderer
        lineRenderer.positionCount = 3; // Three points
											  // Set the color of the line
  }

    // Update is called once per frame
    void Update()
    {
        if (object1 != null && object2 != null && object3 != null)
        {
            // Update line positions to match the objects
            lineRenderer.SetPosition(0, object1.transform.position);
            lineRenderer.SetPosition(1, object2.transform.position);
            lineRenderer.SetPosition(2, object3.transform.position);
        }
    }
}
