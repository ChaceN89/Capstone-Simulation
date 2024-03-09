using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro

public class HighlightOutlineOnMouseOver : MonoBehaviour {
    private Material originalMaterial;
    private Material[] originalMaterials;
    private Renderer objectRenderer;
    public Material outlineMaterial;
    public GameObject popupPrefab; // Assign your popup prefab in the inspector
    private GameObject currentPopup; // To keep track of the instantiated popup

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        originalMaterials = objectRenderer.materials;
    }

    private void OnMouseEnter() {
        if (!Input.GetMouseButton(0)) {
            ChangeMaterialsForObject(outlineMaterial);
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

    private void ChangeMaterialsForObject(Material newMaterial) {
        // Check if there are multiple materials
        if (objectRenderer.materials.Length > 1) {
            Material[] newMaterials = new Material[objectRenderer.materials.Length];
            for (int i = 0; i < newMaterials.Length; i++) {
                // Set each material to the new material
                newMaterials[i] = newMaterial;
            }
            objectRenderer.materials = newMaterials;
        }
        else {
            // If there is only one material, just set it directly
            objectRenderer.material = newMaterial;
        }
    }

    // private void ChangeMaterialsForObject(Material mat){
    //     objectRenderer.material = mat;
    // }

    private void OnMouseExit() {
        // ChangeMaterialsForObject(originalMaterial);
        objectRenderer.materials = originalMaterials;

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


            // for the gondola infomation 
            case "Gondola Connection":
            case "Gondola Connection.001":
            case "Gondola Connection.002":
            case "Gondola Connection.003":
            case "Gondola Connection.004":
            case "Gondola Connection.005":
            case "Gondola Connection.006":
            case "Gondola Connection.007":
            case "Gondola Connection.008":
            case "Gondola Connection.009":
            case "Gondola Connection.010":
            case "Gondola Connection.011":
            case "Gondola Connection.012":
            case "Gondola Connection.013":
                return "PlaceHolder description for all the gondolas.";

            case "Sherical Enclosure":
            case "Sherical Enclosure.001":
            case "Sherical Enclosure.002":
            case "Sherical Enclosure.003":
            case "Sherical Enclosure.004":
            case "Sherical Enclosure.005":
            case "Sherical Enclosure.006":
            case "Sherical Enclosure.007":
            case "Sherical Enclosure.008":
            case "Sherical Enclosure.009":
            case "Sherical Enclosure.010":
            case "Sherical Enclosure.011":
            case "Sherical Enclosure.012":
            case "Sherical Enclosure.013":
                return "placeholder for Spherical Enclosure";

            case "Inner Enclosure":
            case "Inner Enclosure.001":
            case "Inner Enclosure.002":
            case "Inner Enclosure.003":
            case "Inner Enclosure.004":
            case "Inner Enclosure.005":
            case "Inner Enclosure.006":
            case "Inner Enclosure.007":
            case "Inner Enclosure.008":
            case "Inner Enclosure.009":
            case "Inner Enclosure.010":
            case "Inner Enclosure.011":
            case "Inner Enclosure.012":
            case "Inner Enclosure.013":
                return "placeholder for Inner Enclosure";

            case "Inner Mechanisms":
            case "Inner Mechanisms.001":
            case "Inner Mechanisms.002":
            case "Inner Mechanisms.003":
            case "Inner Mechanisms.004":
            case "Inner Mechanisms.005":
            case "Inner Mechanisms.006":
            case "Inner Mechanisms.007":
            case "Inner Mechanisms.008":
            case "Inner Mechanisms.009":
            case "Inner Mechanisms.010":
            case "Inner Mechanisms.011":
            case "Inner Mechanisms.012":
            case "Inner Mechanisms.013":
                return "placeholder text for Inner Mechanisms";

            case "BalloonTop":
            case "BalloonTop.001":
            case "BalloonTop.002":
            case "BalloonTop.003":
            case "BalloonTop.004":
            case "BalloonTop.005":
            case "BalloonTop.006":
            case "BalloonTop.007":
            case "BalloonTop.008":
            case "BalloonTop.009":
            case "BalloonTop.010":
            case "BalloonTop.011":
            case "BalloonTop.012":
            case "BalloonTop.013":
            case "BalloonBottom":
            case "BalloonBottom.001":
            case "BalloonBottom.002":
            case "BalloonBottom.003":
            case "BalloonBottom.004":
            case "BalloonBottom.005":
            case "BalloonBottom.006":
            case "BalloonBottom.007":
            case "BalloonBottom.008":
            case "BalloonBottom.009":
            case "BalloonBottom.010":
            case "BalloonBottom.011":
            case "BalloonBottom.012":
            case "BalloonBottom.013":
                return "placeholder text for Balloons as storage";

            case "Funnels":
            case "Funnels.001":
            case "Funnels.002":
            case "Funnels.003":
            case "Funnels.004":
            case "Funnels.005":
            case "Funnels.006":
            case "Funnels.007":
            case "Funnels.008":
            case "Funnels.009":
            case "Funnels.010":
            case "Funnels.011":
            case "Funnels.012":
            case "Funnels.013":
                return "placeholder text for Funnels";

            // non gondola components 
        case "Driving Rack Bottom":
            return "placeholder for Bottom Platform";
        
        case "bullwheel 2 Bottom":
            return "placeholder for bullwheel 2 Bottom";
        
        case "bullwheel 3 Bottom":
            return "placeholder for bullwheel 3 Bottom";
        
        case "bullwheel Driver Bottom":
            return "placeholder for bullwheel Driver Bottom";
        
        case "Storage Rack Bottom":
            return "placeholder for Storage Rack Bottom";
        
        case "C02 Storage":
            return "placeholder for CO2 Storage";
        
        case "Compressor":
            return "placeholder for Compressor";
        
        case "Motor":
            return "placeholder for Motor";
        
        case "Wiring":
            return "placeholder for Wiring";
        
        case "Driving Rack":
            return "placeholder for Driving Rack";
        
        case "bullwheel 2":
            return "placeholder for bullwheel 2";
        
        case "bullwheel 3":
            return "placeholder for bullwheel 3";
        
        case "bullwheel Driver":
            return "placeholder for bullwheel Driver";
        
        case "Cable":
            return "placeholder for Cable";
        
        case "Storage Rack":
            return "placeholder for Storage Rack";
        

            // for the P use Case infomation 

            default:
                return "";

        }
    }




}
