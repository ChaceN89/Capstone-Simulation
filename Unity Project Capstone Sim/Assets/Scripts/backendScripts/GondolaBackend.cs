/*
GondolaBackend.cs

Script to handle the button inputs and calculations for the gondola
*/
using UnityEngine;
using UnityEngine.UI;

public class GondolaBackend : MonoBehaviour {

    public Canvas camCanvas;
    private Animator animator;

    public Toggle camToggle; // 
    public ToggleList[] toggleList; // list of toggles and items for tooggle to hide or show (visibility )
    private GondolaCamera gondolaCamera; // The active camera for this scenerio 

 private BackendFunctions backendFunctions;

    void Start() {
        animator = GetComponent<Animator>();
        gondolaCamera = FindObjectOfType<GondolaCamera>();
        backendFunctions = FindObjectOfType<BackendFunctions>();
    }
    public void Update() {
        backendFunctions.CheckVisibility(toggleList);
        camCanvas.enabled=camToggle.isOn;
    }

    public void Reset() {
        gondolaCamera.ResetSnap();
        camToggle.isOn = false;        
    }

    public void SetSnap(string newSnap) {
        gondolaCamera.SetSnap(newSnap);
    }

    public void PlayGondola() {
        animator.SetTrigger("PlayAllGondolas");
    }
    public void PlayReverseGondola() {
        animator.SetTrigger("PlayReverseGondolas");
    }
}