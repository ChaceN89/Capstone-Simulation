using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowBottomDepthText : MonoBehaviour {

    public Transform depthTransform; // Reference to the transform whose depth will be used

    private TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update


    void Start() {
        // Assign the TextMeshProUGUI component
        textMeshPro = GetComponent<TextMeshProUGUI>();
        // Disable the text element by default
        if (textMeshPro != null) {
            textMeshPro.enabled = false;
        }
    }

    void Update() {
        if (textMeshPro != null) {
            // Obtain depth from the depthTransform
            float depth = GetDepth(); // For example, use the y position as depth
            // Display the text if depth is greater than 450, otherwise disable the TextMeshPro component
            if (depth > 450) {
                textMeshPro.enabled = true;
            } else {
                // Disable the TextMeshPro component if the depth is not greater than 450
                textMeshPro.enabled = false;
            }

        }
    }


    float GetDepth() {
        // Check if the object is below the surface (y position is negative)
        if (depthTransform.position.y < 0) {
            // Return the absolute depth
            return Mathf.Abs(depthTransform.position.y);
        }
        else {
            // Return 0 if the object is above the surface
            return 0.0f;
        }
    }
}
