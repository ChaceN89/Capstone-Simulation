/*
PDesignBackend.cs

Script to handle the button inputs and calculations for the p design 
*/
using UnityEngine;

public class PUseCaseBackend : MonoBehaviour {
    
    private PUseCaseCamera pUseCaseCamera; // The active camera for this scenerio 
    // public ToggleList[] toggleList; // list of toggles and items for tooggle to hide or show (visibility )

    private BackendFunctions backendFunctions;

    // findthe camera script to change where the camera is focused on 
    private void Start() {
        // get the camera and animator 
        pUseCaseCamera = FindObjectOfType<PUseCaseCamera>();
        backendFunctions = FindObjectOfType<BackendFunctions>();
    }

    public void Update() {
        // backendFunctions.CheckVisibility(toggleList);
    }

    // change where the camera is facing by setting a new tag 
    public void ResetSnap() {
        pUseCaseCamera.ResetSnap();
    }

    // resets the script when called 
    public void Reset() {
        pUseCaseCamera.ResetSnap();
        // backendFunctions.ShowAllChildren(toggleList);
    }
}