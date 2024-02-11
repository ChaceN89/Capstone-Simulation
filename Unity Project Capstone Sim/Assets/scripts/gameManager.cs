using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Transform gondolaTransform;
    public Transform pUseCaseTransform;
    public Transform pDesignTransform;
    public Transform cameraTransform;
    public float rotationSpeed = 0.5f;
    public float moveSpeed = 5f;
    public float distanceFromObject = 10f;
    public float defaultCameraHeight = 6f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool cameraUpdating = false;
    private string activeScenario = "none";


    // This method sets the camera's target and starts the movement
    private void MoveCameraToTarget(Transform target) {

        // Calculate the direction from the origin to the target on the XZ plane
        Vector3 directionFromOrigin = new Vector3(target.position.x, 0f, target.position.z).normalized;

        // Set the target position to be in line with the target from the origin, at the specified distance, and at y = the defult heighht 
        targetPosition = target.position - directionFromOrigin * distanceFromObject;
        targetPosition.y = defaultCameraHeight; // Set the camera height to 2

        // Make sure the camera looks at the target
        targetRotation = Quaternion.LookRotation(target.position - targetPosition);

        cameraUpdating = true;
    }

    // Call this method when the button for "gondolaSim" is clicked
    public void OnGondolaSimButtonClicked() {
        MoveCameraToTarget(gondolaTransform);
        activeScenario = "gondola";
    }

    // Call this method when the button for "prototypeDesign" is clicked
    public void OnPrototypeDesignButtonClicked() {
        MoveCameraToTarget(pDesignTransform);
        activeScenario = "pDesign";
    }

    // Call this method when the button for "prototypeUseCase" is clicked
    public void OnPrototypeUseCaseButtonClicked() {
        MoveCameraToTarget(pUseCaseTransform);
        activeScenario = "pUseCase";
    }

    public string GetActiveScenerio() {
        return activeScenario;
    }

    public bool GetCameraStatus() {
        return cameraUpdating;
    }

    void Update() {
        if (cameraUpdating) {
            // Rotate and move the camera towards the target rotation
            cameraTransform.SetPositionAndRotation(Vector3.MoveTowards(cameraTransform.position, targetPosition, Time.deltaTime * moveSpeed), Quaternion.Slerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed));

            // Check if the camera has reached the target position and rotation
            if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f && Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1.0f) {
                cameraUpdating = false; // Stop updating once the target is reached
                // Here you can add any functionality you want to occur right after the camera stops moving
            }
        }
        else {

            // Else the camera is free to be manipulated by the user or other scripts
            // change ownership of the camera here
        }
    }
}
