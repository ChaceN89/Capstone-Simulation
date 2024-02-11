

using UnityEngine;

public class UseCaseAnimation : MonoBehaviour {
    private Animator animator;

    void Pause(){
        animator.speed = 0f;
    }
    void Resume(){
        animator.speed = 1f;

    }
    void Restart() {
        // Set animation to first frame and play
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0);
        animator.speed = 1f; // Ensure animation plays at normal speed
    }

    void SlowDown(float speed){
        animator.speed = speed;

    }

    void Start() {
        // Get the Animator component
        animator = GetComponent<Animator>();
    }


    // Function to get the state of the current animation as a percentage (0 to 100)
    public float GetAnimationStatePercentage() {
        if (animator != null) {
            // Get information about the current animation state in layer 0
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // Normalize the progress to a percentage
            float progress = stateInfo.normalizedTime % 1; // Use modulus to handle looping animations
            return progress * 100; // Convert to percentage
        }
        return 0f; // Return 0 if animator is not assigned
    }


    // replace key strokes with a UI button eventually and a slider

    void Update() {
        // Check if the Animator component has been assigned
        if (animator != null) {
            // Example keyboard inputs to control animation
            if (Input.GetKeyDown(KeyCode.P)) { // Press P to pause
                Pause();
            }
            if (Input.GetKeyDown(KeyCode.R)) { // Press R to resume
                Resume();
            }
            if (Input.GetKeyDown(KeyCode.T)) { // Press T to restart
                Restart();
            }
            if (Input.GetKeyDown(KeyCode.S)) { // Press S to slow down
                SlowDown(0.5f); // Example speed, adjust as needed
            }
        }
    }
}
