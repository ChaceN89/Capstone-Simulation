using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro

public class HighlightOutlineOnMouseOver : MonoBehaviour {
    private Material[] originalMaterials;
    private Renderer objectRenderer;
    public Material outlineMaterial;
    public GameObject popupPrefab; // Assign your popup prefab in the inspector
    private GameObject currentPopup; // To keep track of the instantiated popup

    private GameManager gameManager; // Reference to the GameManager script to get the active scenario

    private Infomation info;

    private void Start() {
        objectRenderer = GetComponent<Renderer>();
        originalMaterials = objectRenderer.materials;
        gameManager = FindObjectOfType<GameManager>(); // get the game manager
        info = FindObjectOfType<Infomation>(); // get the game manager

    }

    private void OnMouseEnter() {
        if (!Input.GetMouseButton(0)) {
            ChangeMaterialsForObject(outlineMaterial);

            // Instantiate the popup prefab and position it within the Canvas
            CreatePopUp();
        }
    }

    private void CreatePopUp(){
        if (popupPrefab != null) {
            currentPopup = Instantiate(popupPrefab);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>(); // Find the Canvas in the scene
            if (canvas != null) {
                // Set the popup as a child of the canvas with correct positioning
                currentPopup.transform.SetParent(canvas.transform, false);

                // Optional: Adjust position based on mouse or object position
                // Here you might need to convert world position to canvas position if needed

                string scenario = gameManager.GetActiveScenerio();

                TMP_Text text1 = GameObject.Find("Text 1").GetComponent<TMP_Text>();
                TMP_Text text2 = GameObject.Find("Text 2").GetComponent<TMP_Text>();


                if (scenario == "gondola"){
                    // deactivate text 1
                    text1.gameObject.SetActive(false);
                    text2.gameObject.SetActive(true);
                }else{
                    text1.gameObject.SetActive(true);
                    text2.gameObject.SetActive(false);
                    //deactivate text 2
                }
           

                // Set the text of the popup using the GetPopupText method.
                TMP_Text popupText = currentPopup.GetComponentInChildren<TMP_Text>();
                if (popupText != null) {
                    popupText.text = info.GetPopupText(gameObject.name); // Use the GetPopupText function
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


    private void OnMouseExit() {
        objectRenderer.materials = originalMaterials;

        // Destroy the popup when the mouse exits
        if (currentPopup != null) {
            Destroy(currentPopup);
        }
    }

  


}