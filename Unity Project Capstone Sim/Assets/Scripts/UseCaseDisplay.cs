using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseCaseDisplay : MonoBehaviour {
    const float volume = 3843.968f; // using cone formula 1/3*pi*r^2*h, r = 4", h = 14". converted from cubic inches to ml
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
        // formula source: https://calculator.academy/co2-ppm-calculator/#:~:text=To%20calculate%20the%20CO2%20PPM%2C%20divide%20the%20volume%20of%20CO2,ratio%20into%20parts%20per%20million.
        float depth = GetDepth();
        if(depth < 432){
            return 0.0004f * volume / volume * 1000000f; // (co2 volume / air volume) * 1,000,000 for ppm
        }
        else{
            return 432f / depth * 0.0004f * volume / volume * 1000000f; // at max depth, ppm will be 85% of original
        }
    }

    float GetCO2Bottom() {
        float depth = GetDepth();
        if(depth < 432){
            return 0.0004f * volume / volume * 1000000f; // (co2 volume / air volume) * 1,000,000 for ppm
        }
        else{
            return (2 - (432f / depth)) * 0.0004f * volume / volume * 1000000f; // at max depth, ppm will be 115% of original
        }
    }
}
