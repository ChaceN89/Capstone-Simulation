using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindSpeedDisplay : MonoBehaviour {

    public Animator windmillAnimator;
    public TextMeshProUGUI textMeshPro;


    // Update is called once per frame
    void Update() {
        string windSpeedText = "Wind Speed: ";

        // Assuming that a higher speed value means higher wind speed
        if (windmillAnimator.speed > 1.4) {
            // High wind speed
            windSpeedText += "High\nHigh Energy Output \n-storing excess energy.";
        }
        else if (windmillAnimator.speed > 0.4) {
            // Normal wind speed
            windSpeedText += "Normal\nNormal Energy Output.";
        }
        else {
            // Low wind speed
            windSpeedText += "Low\nLow Energy Output \n-releasing excess energy.";
        }



        // Update the text of the TextMeshProUGUI component
        textMeshPro.text = windSpeedText;


    }
}
