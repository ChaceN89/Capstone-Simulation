using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseCaseDisplay : MonoBehaviour {
    public Transform depthTransform; // Reference to the transform whose depth will be used

    private TextMeshProUGUI textMeshPro;

    void Start() {
        // Get the TextMeshPro component attached to this GameObject
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Update the text initially
        UpdateText();
    }

    void Update() {
        // Update the text every frame
        UpdateText();
    }

    void UpdateText() {
        // Check if the TextMeshPro component is assigned
        if (textMeshPro != null) {
            // Obtain depth from the depthTransform
            float depth = GetDepth(); // For example, use the y position as depth
            // Obtain other values from functions or variables
            float pressure = GetPressure();
            float co2Top = GetCO2Top();
            float co2Bottom = GetCO2Bottom();

            // Update the text with obtained values
            textMeshPro.text = "Depth: " + depth.ToString("F2") + " m \n" +
                               "Pressure: " + pressure.ToString("F2") + " atm\n" +
                               "CO2 in the Top balloon: " + co2Top.ToString("F2") + " ppm\n" +
                               "CO2 in the Bottom balloon: " + co2Bottom.ToString("F2") + " ppm";
        }
    }


    float GetDepth() {
         // Check if the object is below the surface (y position is negative)
        if (depthTransform.position.y < 0)
        {
            // Return the absolute depth
            return Mathf.Abs(depthTransform.position.y);
        }
        else
        {
            // Return 0 if the object is above the surface
            return 0.0f;
        }
    }

    float GetPressure() {
        // Get the depth
        float depth = GetDepth();

        // Constants
        float seaWaterDensity = 1025.0f; // kg/m^3, approximate density of seawater
        float gravity = 9.81f; // m/s^2, acceleration due to gravity
        float atmosphericPressure = 1.0f; // atm, atmospheric pressure at sea level

        // Calculate pressure using hydrostatic pressure formula: P = rho * g * h + P0
        float pressure = seaWaterDensity * gravity * depth + atmosphericPressure;

        // Convert pressure from atm to atmospheres
        pressure /= 101325.0f; // 1 atm = 101325 Pa

        return pressure;
    }

    float GetCO2Top() {
        // Implement your function to get CO2 in the top balloon here
        return 0.0f; // Example value, replace with your logic
    }

    float GetCO2Bottom() {
        // Implement your function to get CO2 in the bottom balloon here
        return 0.0f; // Example value, replace with your logic
    }
}
