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
            default:
                return "This is an unidentified object. No additional information is available.";
        }
    }
}
