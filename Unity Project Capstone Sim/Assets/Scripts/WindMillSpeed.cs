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


            // normal speed sicne animations are at the end (ie not running)
            if (stateInfo.IsName("Gondola Connection|Action") && stateInfo.normalizedTime >= 1f) {
                windmillAnimator.speed = 0.6f;
            }
            else if (stateInfo.IsName("Gondola Connection|Action 0 - reverse") && stateInfo.normalizedTime >= 1f) {
                windmillAnimator.speed = 0.6f;
            }

            // storging energy
            else if (stateInfo.IsName("Gondola Connection|Action")) {
                windmillAnimator.speed = 2.4f;
            }

            // releasing energy
            else if (stateInfo.IsName("Gondola Connection|Action 0 - reverse")) {
                windmillAnimator.speed = 0.1f;
            }
        }
    }
}
