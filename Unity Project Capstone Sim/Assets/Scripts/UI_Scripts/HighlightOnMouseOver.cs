using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro

public class HighlightOutlineOnMouseOver : MonoBehaviour {
    private Renderer[] originalRenderers;
    private Material[][] originalMaterials;
    public Material outlineMaterial;
    public GameObject popupPrefab;
    private GameObject currentPopup;

    private GameManager gameManager;
    private Infomation info; // Assuming this is correctly implemented elsewhere

    private void Start() {
        originalRenderers = GetComponentsInChildren<Renderer>(true);
        originalMaterials = new Material[originalRenderers.Length][];
        for (int i = 0; i < originalRenderers.Length; i++) {
            originalMaterials[i] = originalRenderers[i].materials;
        }
        gameManager = FindObjectOfType<GameManager>();
        info = FindObjectOfType<Infomation>();
    }

    private void OnMouseEnter() {
        if (!Input.GetMouseButton(0)) {
            foreach (Renderer renderer in originalRenderers) {
                ChangeMaterialsForObject(renderer, outlineMaterial);
            }
            CreatePopUp();
        }
    }

    private void ChangeMaterialsForObject(Renderer renderer, Material newMaterial) {
        if (renderer.materials.Length > 1) {
            Material[] newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < newMaterials.Length; i++) {
                newMaterials[i] = newMaterial;
            }
            renderer.materials = newMaterials;
        } else {
            renderer.material = newMaterial;
        }
    }

    private void OnMouseExit() {
        for (int i = 0; i < originalRenderers.Length; i++) {
            originalRenderers[i].materials = originalMaterials[i];
        }
        if (currentPopup != null) {
            Destroy(currentPopup);
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

    // private void ChangeMaterialsForObject(Material newMaterial) {
    //     // Check if there are multiple materials
    //     if (objectRenderer.materials.Length > 1) {
    //         Material[] newMaterials = new Material[objectRenderer.materials.Length];
    //         for (int i = 0; i < newMaterials.Length; i++) {
    //             // Set each material to the new material
    //             newMaterials[i] = newMaterial;
    //         }
    //         objectRenderer.materials = newMaterials;
    //     }
    //     else {
    //         // If there is only one material, just set it directly
    //         objectRenderer.material = newMaterial;
    //     }
    // }


    // private void OnMouseExit() {
    //     objectRenderer.materials = originalMaterials;

    //     // Destroy the popup when the mouse exits
    //     if (currentPopup != null) {
    //         Destroy(currentPopup);
    //     }
    // }

  


}
