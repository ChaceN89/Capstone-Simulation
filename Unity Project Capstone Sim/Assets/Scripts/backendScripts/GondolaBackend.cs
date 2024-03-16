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


    public Animator windmillAnimator;
    public Animator gondolaAnimator;

    private BackendFunctions backendFunctions;

    void Start() {
        animator = GetComponent<Animator>();
        gondolaCamera = FindObjectOfType<GondolaCamera>();
        backendFunctions = FindObjectOfType<BackendFunctions>();
    }
    public void Update() {
        backendFunctions.CheckVisibility(toggleList);
        camCanvas.enabled=camToggle.isOn;

        // if (gondolaAnimator no animation current playing only works on Gondola 0 layer since it lin
 
        int layerIndex = gondolaAnimator.GetLayerIndex("Gondola 0");
        AnimatorStateInfo stateInfo = gondolaAnimator.GetCurrentAnimatorStateInfo(layerIndex);

        // Replace "Empty" with the correct name or hash of the state you want to check for.
        if (stateInfo.IsName("Empty")) {
            windmillAnimator.speed= 0.7f;
        }else if(stateInfo.IsName("Gondola Connection|Action")){
            windmillAnimator.speed= 2f;
        }
        else if(stateInfo.IsName("Gondola Connection|Action 0 - reverse")){
            windmillAnimator.speed= 0.1f;
        }


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