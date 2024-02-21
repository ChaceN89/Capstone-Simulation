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

    public ToggleList [] toggleList;

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

    public void CheckVisibility(){
        foreach (ToggleList item in toggleList) {
            // Find GameObjects with the specified tag
            GameObject[] objs = GameObject.FindGameObjectsWithTag(item.name);
            foreach (var obj in objs) {
                if (obj.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    // Hide or show the mesh based on the toggle's state
                    meshRenderer.enabled = item.toggle.isOn;
                } else {
                    Debug.LogWarning($"MeshRenderer not found on GameObject with tag: {item.name}.");
                }
            }
        }
    }


  

}



// Class for the system paramerters  
[System.Serializable]
public class ToggleList {
    public string names;
    public Toggle toggle;

}

