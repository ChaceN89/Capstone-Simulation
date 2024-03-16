using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMillSpeed : MonoBehaviour {
    private Animator windmillAnimator;
    public Animator gondolaAnimator;

    void Start() {
        // This will get the Animator component attached to the same GameObject
        windmillAnimator = GetComponent<Animator>();

        // Start the "RotateFast" animation at a random point
        float randomStartTime = Random.Range(0.0f, 1.0f); // Generate a random start time
        windmillAnimator.Play("RotateFast", 0, randomStartTime);

    }

    void Update() {
        if (gondolaAnimator != null) {
            int layerIndex = gondolaAnimator.GetLayerIndex("Gondola 0");
            AnimatorStateInfo stateInfo = gondolaAnimator.GetCurrentAnimatorStateInfo(layerIndex);

            if (stateInfo.IsName("Empty")) {
                windmillAnimator.speed = 0.7f;
            }
            else if (stateInfo.IsName("Gondola Connection|Action")) {
                windmillAnimator.speed = 2f;
            }
            else if (stateInfo.IsName("Gondola Connection|Action 0 - reverse")) {
                windmillAnimator.speed = 0.2f;
            }
        }
    }
}
