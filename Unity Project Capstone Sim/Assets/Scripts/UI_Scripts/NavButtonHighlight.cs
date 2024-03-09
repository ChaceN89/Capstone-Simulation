using TMPro;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;

public class NavButtonHighlight : MonoBehaviour {

    private GameManager gameManager; // Reference to the GameManager script to get the active scenario
    public string buttonName = ""; // the button name - should relate to active scenario

    // colors for highlightingf 
    public Color highlightColor = Color.red;
    public Color afterVisitColor = Color.gray; // a light gray 

    public Color hoverColor = Color.blue; // Color when hovering over the button


    private bool hasBeenActiveOnce = false; // Flag to indicate if the button has been active once

    private Image buttonBackground; // Variable to hold the reference to the button's Image component
    private Color originalBackgroundColor;

    // instead of Image could get a private TextMeshProUGUI buttonText; for the text
    // buttonText.color is the access

    void Start() {
        gameManager = FindObjectOfType<GameManager>(); // get the game manager

        // get the button background and save the original backgours
        buttonBackground = GetComponentInChildren<Image>();
        originalBackgroundColor= buttonBackground.color;
    }

    // This method replaces the Update method to improve performance by reducing unnecessary checks.
    void Update() {

        // if the active scenario matches the buttons name
        if (gameManager.GetActiveScenerio() == buttonName) {
            // set highlight color and set visted button to true
            buttonBackground.color = highlightColor;
            hasBeenActiveOnce = true;

        }
        else {
            // if the button have been visited already set to the afteVisitColor else set to the original colr
            if (hasBeenActiveOnce) {
                // buttonText.color = afterVisitColor;
                buttonBackground.color = afterVisitColor;
            }
            else {
                // buttonText.color = originalColor;
                buttonBackground.color = originalBackgroundColor;
            }
        }
    }


}
