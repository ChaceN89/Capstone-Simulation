/*
GondolaBackend.cs

Script to handle the button inputs and calculations for the gondola
*/
using UnityEngine;

public class GondolaBackend : MonoBehaviour {

    private Animator animator;

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
    }

    public void Reset() {
        gondolaCamera.ResetSnap();
    }

    public void PlayGondola() {
        animator.SetTrigger("PlayAllGondolas");
    }
    public void PlayReverseGondola() {
        animator.SetTrigger("PlayReverseGondolas");
    }
}