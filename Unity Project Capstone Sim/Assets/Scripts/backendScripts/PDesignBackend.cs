/*
PDesignBackend.cs

Script to handle the button inputs and calculations for the p design 
*/
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PDesignBackend : MonoBehaviour {

    private PDesignCamera pDesignCamera;
    private Animator pDesignAnimator;

    public ToggleList[] toggleList;

    // findthe camera script to change where the camera is focused on 
    private void Start() {
        pDesignCamera = FindObjectOfType<PDesignCamera>();
        pDesignAnimator = GetComponent<Animator>();
    }

    // explode the model 
    public void ExplodeModel() {
        pDesignAnimator.SetTrigger("Explode");
    }

    // rebuild the model 
    public void RebuildModel() {
        pDesignAnimator.SetTrigger("Rebuild");
    }


    // change where the camera is facing
    public void ChangeSnap(string name) {
        pDesignCamera.SetObjectTag(name);
    }


    public void Reset() {
        // Debug.Log("P Design is reset ");
        pDesignCamera.SetObjectTag("pDesign");
        ShowAllChildren();
    }


    public void Update() {
        CheckVisibility();
    }

    public void ShowAllChildren() {
        // Assuming you want to show all children that are part of the toggle list
        foreach (ToggleList item in toggleList) {
            item.toggle.isOn = true;
        }
    }



public void CheckVisibility() {
    foreach (ToggleList item in toggleList) {
        // For each name in the item's names array, search through all children and descendants
        foreach (string name in item.names) {
            // Start the search from this GameObject's transform
            Transform foundChild = FindDeepChild(transform, name);
            if (foundChild != null) {
                // Get the Renderer component, if any, directly on the found child
                Renderer renderer = foundChild.GetComponent<Renderer>();
                if (renderer != null) {
                    // Toggle visibility of this renderer only
                    renderer.enabled = item.toggle.isOn;
                }

                // Check for a SkinnedMeshRenderer as well, in case the object uses one
                SkinnedMeshRenderer skinnedRenderer = foundChild.GetComponent<SkinnedMeshRenderer>();
                if (skinnedRenderer != null) {
                    // Toggle visibility of this skinned mesh renderer only
                    skinnedRenderer.enabled = item.toggle.isOn;
                }
            } else {
                Debug.LogWarning($"Child with name {name} not found.");
            }
        }
    }
}

// Recursive method to find a child by name in the descendants
private Transform FindDeepChild(Transform parent, string name) {
    foreach (Transform child in parent) {
        if (child.name == name) {
            return child;
        }
        Transform found = FindDeepChild(child, name);
        if (found != null) {
            return found;
        }
    }
    return null; // If no child with the name is found
}


}



// Class for the system paramerters  
[System.Serializable]
public class ToggleList {
    public string [] names;
    public Toggle toggle;

}

