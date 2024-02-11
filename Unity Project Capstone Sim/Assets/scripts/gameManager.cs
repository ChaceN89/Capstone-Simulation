using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Object transforms to snap to 3 scenerios (the objcts can be what ever the focus of the scenerio is)
    public Transform gondolaTransform;
    public Transform pUseCaseTransform;
    public Transform pDesignTransform;

    // Variables that control camera movement speed and distance etc
    public float rotationSpeed = 0.5f;
    public float moveSpeed = 5f;
    public float distanceFromObject = 10f;
    public float defaultCameraHeight = 6f;

    // the target of the camera motion for position and rotation 
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    
    // If the camera if updating or not, and the current scenerio 
    private bool cameraUpdating = false;
    private string activeScenario = "none";

    // camera scripts 
    public CameraCircle cameraCircle; // script to rotate the camera around the object 
    public Transform cameraTransform;

    // -------------- Getters -----------------
    public string GetActiveScenerio() {
        return activeScenario;
    }
    public bool GetCameraStatus() {
        return cameraUpdating;
    }

    public float GetDefaultCameraHeight() {
        return defaultCameraHeight;
    }


    // ----------- Function triggered by buttons ----------
    // sets the active scenario and updates the camera behavious 
    public void SetActiveScenario(string scenario, Transform objectToMoveTo) {
        activeScenario = scenario;
        MoveCameraToTarget(objectToMoveTo);
        UpdateCameraBehavior(); // Update camera behavior based on the new active scenario
    }
    // Call this method when the button for "gondolaSim" is clicked
    public void OnGondolaSimButtonClicked() {
        SetActiveScenario("gondola", gondolaTransform);
    }

    // Call this method when the button for "prototypeDesign" is clicked
    public void OnPrototypeDesignButtonClicked() {
        SetActiveScenario("pDesign", pDesignTransform);
    }

    // Call this method when the button for "prototypeUseCase" is clicked
    public void OnPrototypeUseCaseButtonClicked() {
        SetActiveScenario("pUseCase", pUseCaseTransform);
    }



    // This method sets the camera's target and starts the movement - to be 
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

    private void UpdateCameraTransform(){
        // Rotate and move the camera towards the target rotation
        cameraTransform.SetPositionAndRotation(Vector3.MoveTowards(cameraTransform.position, targetPosition, Time.deltaTime * moveSpeed), Quaternion.Slerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed));

        // Check if the camera has reached the target position and rotation
        if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f && Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1.0f) {
            cameraUpdating = false; // Stop updating once the target is reached
            // Here you can add any functionality you want to occur right after the camera stops moving
        }
    }


    // Method to activate or deactivate the CameraCircle script
    private void UpdateCameraBehavior() {
        if (! cameraUpdating){
            if (cameraCircle == null) return;

            // Example conditions to activate/deactivate the CameraCircle script
            // Adjust these conditions based on your scenarios
            if (activeScenario == "pDesign" || activeScenario == "pUseCase") {
                cameraCircle.enabled = true; // Activate CameraCircle for specific scenarios
                cameraCircle.SetObjectTag(activeScenario);

            // more if statements for turnoing on and off other cameras
            } else {
                DisableCameras(); // Deactivate otherwise
                // deactivate all cameras
            }
        }else{
            DisableCameras();
            
        }
    }

    private void DisableCameras(){
        cameraCircle.enabled = false; 
    }


    private void Start() {
        // Optionally, find the CameraCircle component at start if not assigned
        if (cameraCircle == null) {
            cameraCircle = FindObjectOfType<CameraCircle>();
        }
    }

    void Update() {
        if (cameraUpdating) {
  
            UpdateCameraTransform();
        }else {

            // Else the camera is free to be manipulated by the user or other scripts
            // change ownership of the camera here
            UpdateCameraBehavior();

        }
    }
}
