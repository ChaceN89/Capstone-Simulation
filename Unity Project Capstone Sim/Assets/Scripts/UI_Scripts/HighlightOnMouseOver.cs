using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro

public class HighlightOutlineOnMouseOver : MonoBehaviour {
    private Color originalColor;
    private Renderer objectRenderer;
    public Material outlineMaterial;
    public GameObject popupPrefab; // Assign your popup prefab in the inspector
    private GameObject currentPopup; // To keep track of the instantiated popup

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    private void OnMouseEnter() {
        if (!Input.GetMouseButton(0)) {
            objectRenderer.material = outlineMaterial;
            Debug.Log("Mouse over: " + gameObject.name);

            // Instantiate the popup prefab and position it within the Canvas
            if (popupPrefab != null) {
                currentPopup = Instantiate(popupPrefab);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>(); // Find the Canvas in the scene
                if (canvas != null) {
                    // Set the popup as a child of the canvas with correct positioning
                    currentPopup.transform.SetParent(canvas.transform, false);

                    // Optional: Adjust position based on mouse or object position
                    // Here you might need to convert world position to canvas position if needed

                    // Set the text of the popup using the GetPopupText method.
                    TMP_Text popupText = currentPopup.GetComponentInChildren<TMP_Text>();
                    if (popupText != null) {
                        popupText.text = GetPopupText(gameObject.name); // Use the GetPopupText function
                    }
                }
            }
        }
    }

    private void OnMouseExit() {
        objectRenderer.material.color = originalColor;

        // Destroy the popup when the mouse exits
        if (currentPopup != null) {
            Destroy(currentPopup);
        }
    }

    private string GetPopupText(string objectName) {
        switch (objectName) {
        case "Box":
            return "This is a Box. Boxes are used for storing and transporting items.";
        case "Lid":
            return "This is a Lid. Lids are used to cover something, like a box, to protect its contents or to keep it contained.";
   

        case "Ardunio":
            return "This is an Arduino. An open-source electronics platform based on easy-to-use hardware and software.";

        case "Balloon Top":
        case "Balloon Bottom":
            return "This is the Balloon. It serves as the base for the balloon structure and may hold various components. Also has a Clamp to keep it in place.";


        case "Funnel Top":
        case "Funnel Bottom":
            return "This is the Funnel. It's part of a structure that directs flow into the balloon. I attaches to the valve using hosing to dirct the flow";

        case "Air Valve Top":
        case "Air Valve Bottom":
            return "This is the Air Valve. It allows for the control of airflow into or out of the balloon.";

        case "C02 Sensors Top":
        case "CO2 Sensor Bottom":
            return "This is the CO2 Sensor. It's used for measuring carbon dioxide levels at the base of the balloon.";
    
        case "Battery case":
            return "This is the Battery case. It stores and protects the battery, providing power to the system.";
        case "Battery":
            return "This is the Battery. It provides electrical power to all components in the setup.";

        case "Gasket":
            return "This is the Gasket. It's used to seal the interface between two parts, preventing leaks.";

        case "BreadBoards":
            return "These are Breadboards. They are used for prototyping electronic circuits without soldering.";
        
        case "Depth Sensor":
            return "This is a Depth Sensor. It's typically used to measure the distance to a surface or object.";
    
        case "Mini Bread Board Back":
            return "This is the back of a Mini Bread Board. It's used for creating small and compact electronic circuits.";
  
        case "Mini Bread Boards Side":
            return "These are Mini Bread Boards located on the side. They are used for additional circuitry on the sides of the device.";
        case "MicroSD Card Module ":
            return "This is a MicroSD Card Module. It's used to read from and write to MicroSD cards, often used for data storage. Has an SD card";

        case "Relay":
            return "This is a Relay. It's an electrically operated switch used to control a circuit by a separate low-power signal.";
        case "Valve bracket":
            return "This is a Valve bracket. It's used to mount and secure valves in place.";
        case "Hose Fitting":
        case "Hose Fitting.001":
            return "This is a Hose Fitting. Hose fittings are used to securely connect hoses to other components.";
        case "Valve":
            return "This is a Valve. Valves are used to control the flow of fluids by opening, closing, or partially obstructing passageways.";
        
      
        default:
            return "";
    
        }
    }




}
