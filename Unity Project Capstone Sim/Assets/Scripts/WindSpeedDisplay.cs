using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindSpeedDisplay : MonoBehaviour {

    public Animator windmillAnimator;
    public TextMeshProUGUI textMeshPro;


    // Update is called once per frame
    void Update() {
        string windSpeedText = "<b>Wind Speed</b>: ";

        // Assuming that a higher speed value means higher wind speed
        if (windmillAnimator.speed > 1.4) {
            // High wind speed
            windSpeedText += "High\n<b>High</b> Energy Output \n<i>Storing excess energy</i>";
        }
        else if (windmillAnimator.speed > 0.4) {
            // Normal wind speed
            windSpeedText += "Normal\n<b>Normal </b>Energy Output";
        }
        else {
            // Low wind speed
            windSpeedText += "Low\n<b>Low</b> Energy Output\n<i>Releasing excess energy</i>";
        }



        // Update the text of the TextMeshProUGUI component
        textMeshPro.text = windSpeedText;


    }
}
