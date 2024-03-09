/*
PDesignBackend.cs

Script to handle the button inputs and calculations for the p design 
*/
using System;
using UnityEngine;
using UnityEngine.UI;

public class PDesignBackend : MonoBehaviour {

    private PDesignCamera pDesignCamera; // The active camera for this scenerio 
    private Animator pDesignAnimator; // the animator for the sceneio object
    public ToggleList[] toggleList; // list of toggles and items for tooggle to hide or show (visibility )


    // findthe camera script to change where the camera is focused on 
    private void Start() {
        // get the camera and animator 
        pDesignCamera = FindObjectOfType<PDesignCamera>();
        pDesignAnimator = GetComponent<Animator>();
    }

    public void Update() {
        CheckVisibility();
    }

    // functions to use animaitons- but they are called directly by the buttons now - keeping this here for referecne
    // // explode the model animation
    // public void ExplodeModel() => pDesignAnimator.SetTrigger("Explode");

    // // rebuild the model animation
    // public void RebuildModel() => pDesignAnimator.SetTrigger("Rebuild");
    
    // public void ShrinkBalloons(){
    //     pDesignAnimator.SetTrigger("ShrinkBalloons");
    //     // pDesignAnimator.SetBool("SmallBalloons", false);
    // } 
    // public void ExpandBalloons() {
    //     pDesignAnimator.SetTrigger("GrowBalloons");
    //     // pDesignAnimator.SetBool("SmallBalloons", false);
    // } 
    //-----------------------------------------------

    // change where the camera is facing by setting a new tag 
    public void ResetSnap() {
        pDesignCamera.ResetSnap();
    }

    // resets the script when called 
    public void Reset() {
        pDesignCamera.ResetSnap();
        ShowAllChildren();
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
