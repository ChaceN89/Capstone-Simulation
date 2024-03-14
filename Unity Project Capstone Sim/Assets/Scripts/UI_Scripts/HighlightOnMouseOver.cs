using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro

public class HighlightOutlineOnMouseOver : MonoBehaviour {
    private Material[] originalMaterials;
    private Renderer objectRenderer;
    public Material outlineMaterial;
    public GameObject popupPrefab; // Assign your popup prefab in the inspector
    private GameObject currentPopup; // To keep track of the instantiated popup

    private Infomation info;

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        originalMaterials = objectRenderer.materials;
        info = FindObjectOfType<Infomation>(); // get the game manager

    }

    private void OnMouseEnter() {
        if (!Input.GetMouseButton(0)) {
            ChangeMaterialsForObject(outlineMaterial);

            // Instantiate the popup prefab and position it within the Canvas
            currentPopup = info.CreatePopUp(popupPrefab, currentPopup, gameObject.name);
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

    private void OnMouseExit() {
        objectRenderer.materials = originalMaterials;

        // Destroy the popup when the mouse exits
        if (currentPopup != null) {
            Destroy(currentPopup);
        }
    }
}