using UnityEngine;
using UnityEngine.UI; // Necessary for working with UI elements

public class ToggleObjectVisibility : MonoBehaviour {
    public Toggle visibilityToggle; // Assign your UI Toggle here
    public GameObject objectToControl; // Assign your GameObject here

    void Start() {
        // Ensure the object's visibility matches the toggle's initial state
        objectToControl.GetComponent<Renderer>().enabled = visibilityToggle.isOn;

        // Add a listener to call the ToggleChanged method whenever the toggle's state changes
        visibilityToggle.onValueChanged.AddListener(ToggleChanged);
    }

    void ToggleChanged(bool isVisible) {
        // Enable or disable the Renderer component based on the toggle's state
        objectToControl.GetComponent<Renderer>().enabled = isVisible;
    }
}
