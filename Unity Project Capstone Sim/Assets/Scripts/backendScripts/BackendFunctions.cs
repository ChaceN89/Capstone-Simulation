using UnityEngine;
using UnityEngine.UI;


public class BackendFunctions : MonoBehaviour {
    

    public void ShowAllChildren(ToggleList[] toggleList) {
        // Assuming you want to show all children that are part of the toggle list
        foreach (ToggleList item in toggleList) {
            item.toggle.isOn = true;
        }
    }
    
    public void CheckVisibility(ToggleList[] toggleList) {
        foreach (ToggleList item in toggleList) {
            // For each name in the item's names array, search through all children and descendants
            foreach (string name in item.names) {
                // Start the search from this GameObject's transform
                Transform foundChild = FindDeepChild(transform, name);
                if (foundChild != null) {
                    // Get the Renderer component, if any, directly on the found child
                    if (foundChild.TryGetComponent<Renderer>(out var renderer)) {
                        renderer.enabled = item.toggle.isOn;// Toggle visibility of this renderer only
                    }

                    // Check for a SkinnedMeshRenderer as well, in case the object uses one (and regular renders)
                    if (foundChild.TryGetComponent<SkinnedMeshRenderer>(out var skinnedRenderer)) {
                        skinnedRenderer.enabled = item.toggle.isOn; // Toggle visibility of this skinned mesh renderer only
                    }
                    // toggle the collider
                    ToggleCollider(foundChild, item.toggle.isOn);
                }
                else {
                    Debug.LogWarning($"Child with name {name} not found.");
                }
            }
        }
    }

    

    private void ToggleCollider(Transform target, bool state) {
        // Disable or enable all the Colliders on the component, if they are present
        Collider[] colliders = target.GetComponents<Collider>();
        foreach (Collider collider in colliders) {
            collider.enabled = state;
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


// Class for the the list of toggles for visibility   
[System.Serializable]
public class ToggleList {
    public string[] names;
    public Toggle toggle;
}
