/*
PDesignBackend.cs

Script to handle the button inputs and calculations for the p design 
*/
using UnityEngine;

public class PDesignBackend : MonoBehaviour {

    private PDesignCamera pDesignCamera; // The active camera for this scenerio 
    public ToggleList[] toggleList; // list of toggles and items for tooggle to hide or show (visibility )

    private BackendFunctions backendFunctions;

    // findthe camera script to change where the camera is focused on 
    private void Start() {
        // get the camera and animator 
        pDesignCamera = FindObjectOfType<PDesignCamera>();
        backendFunctions = FindObjectOfType<BackendFunctions>();
    }

    public void Update() {
        backendFunctions.CheckVisibility(toggleList);
    }

    // change where the camera is facing by setting a new tag 
    public void ResetSnap() {
        pDesignCamera.ResetSnap();
    }

    // resets the script when called 
    public void Reset() {
        pDesignCamera.ResetSnap();
        backendFunctions.ShowAllChildren(toggleList);
    }
}
