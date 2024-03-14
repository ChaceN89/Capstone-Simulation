
using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro

// simiar to HIGHlight on Mouse Over but specific for PUseCase as its the only object with no render on the parent

public class HighlightForPUseCase : MonoBehaviour {
    
    private Renderer[] originalRenderers;
    private Material[][] originalMaterials;
    public Material outlineMaterial;
    public GameObject popupPrefab;
    private GameObject currentPopup;

    private Infomation info; 

    private void Start() {
        originalRenderers = GetComponentsInChildren<Renderer>(true);
        originalMaterials = new Material[originalRenderers.Length][];
        for (int i = 0; i < originalRenderers.Length; i++) {
            originalMaterials[i] = originalRenderers[i].materials;
        }
        info = FindObjectOfType<Infomation>();
    }

    private void OnMouseEnter() {
        if (!Input.GetMouseButton(0)) {
            foreach (Renderer renderer in originalRenderers) {
                // Exclude specific children by name
                if (renderer.gameObject.name != "Weight" && renderer.gameObject.name != "Wire Clamps") {
                    ChangeMaterialsForObject(renderer, outlineMaterial);
                }
            }
            currentPopup = info.CreatePopUp(popupPrefab, currentPopup, gameObject.name);

        }
    }

    private void ChangeMaterialsForObject(Renderer renderer, Material newMaterial) {
        if (renderer.gameObject.name != "Weight" && renderer.gameObject.name != "Wire Clamps") {
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
    }

    private void OnMouseExit() {
        for (int i = 0; i < originalRenderers.Length; i++) {
            // Exclude specific children by name
            if (originalRenderers[i].gameObject.name != "Weight" && originalRenderers[i].gameObject.name != "Wire Clamps") {
                originalRenderers[i].materials = originalMaterials[i];
            }
        }
        if (currentPopup != null) {
            Destroy(currentPopup);
        }
    }

    



  


}
