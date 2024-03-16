using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindSpeedDisplay : MonoBehaviour {

    public Animator windmillAnimator;
    public TextMeshProUGUI textMeshPro;


    // Update is called once per frame
    void Update() {
        string windSpeedText = "Wind Speed:\n";

        // Assuming that a higher speed value means higher wind speed
        if (windmillAnimator.speed > 1) {
            // High wind speed
            windSpeedText += "High";
        }
        else if (windmillAnimator.speed > 0.3) {
            // Normal wind speed
            windSpeedText += "Normal";
        }
        else {
            // Low wind speed
            windSpeedText += "Low";
        }

        // Update the text of the TextMeshProUGUI component
        textMeshPro.text = windSpeedText;


    }
}
