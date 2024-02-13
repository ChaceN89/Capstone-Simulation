using UnityEngine;



public class GameManager : MonoBehaviour {
    // Object transforms to snap to 3 scenerios (the objcts can be what ever the focus of the scenerio is)
    public Transform gondolaTransform;
    public Transform pUseCaseTransform;
    public Transform pDesignTransform;

    // Variables that control camera movement speed and distance etc
    public float rotationSpeed = 0.5f;
    public float moveSpeed = 5f;

    // Camera height and distances to object for each scenerio
    public ScenerioParams pDesign = new() {name="pDesign", camHeight = 4f, distToCam = 5f };
    public ScenerioParams pUseCase = new() {name="pUseCase", camHeight = 6f, distToCam = 6f };
    public ScenerioParams gondola = new() {name="gondola", camHeight = 8f, distToCam = 15f };

    // the target of the camera motion for position and rotation 
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    // If the camera if updating or not, and the current scenerio 
    private bool cameraUpdating = false;
    private string activeScenario = "none";

    // camera scripts 
    public PDesignCamera pDesignCamera; // script to rotate the camera around the object 
    public Transform cameraTransform;

    // -------------- Getters -----------------
    public string GetActiveScenerio() {
        return activeScenario;
    }
    public bool GetCameraStatus() {
        return cameraUpdating;
    }

    // ----------- Function triggered by buttons ----------
    // sets the active scenario and updates the camera behavious 
    public void SetActiveScenario(ScenerioParams scenario, Transform objectToMoveTo) {
        activeScenario = scenario.name;
        MoveCameraToTarget(scenario, objectToMoveTo);
        UpdateActiveScripts(); // Update camera behavior and other scripts based on the new active scenario
    }
    // Call this method when the button for "gondolaSim" is clicked
    public void OnGondolaSimButtonClicked() {
        SetActiveScenario(gondola, gondolaTransform );
    }
    // Call this method when the button for "prototypeDesign" is clicked
    public void OnPrototypeDesignButtonClicked() {
        SetActiveScenario(pDesign, pDesignTransform);
    }
    // Call this method when the button for "prototypeUseCase" is clicked
    public void OnPrototypeUseCaseButtonClicked() {
        SetActiveScenario(pUseCase, pUseCaseTransform);
    }



    // This method sets the camera's target and starts the movement - very related to the update camera transform function 
    // takes the target transform and the distance we want the camera to be away from it and the height of the camera at the obeject
    private void MoveCameraToTarget(ScenerioParams scenario, Transform target) {

        // Calculate the direction from the origin to the target on the XZ plane
        Vector3 directionFromOrigin = new Vector3(target.position.x, 0f, target.position.z).normalized;

        // Set the target position to be in line with the target from the origin, at the specified distance, and at y = the defult heighht 
        targetPosition = target.position - directionFromOrigin * scenario.distToCam;
        targetPosition.y = scenario.camHeight; // Set the camera height to 2

        // Make sure the camera looks at the target
        targetRotation = Quaternion.LookRotation(target.position - targetPosition);

        // set the camera updating to true 
        cameraUpdating = true;
    }

    // updates the camera position over time and turns off the updating bool when the camera is finsihed moving
    private void UpdateCameraTransform() {
        // Rotate and move the camera towards the target rotation
        cameraTransform.SetPositionAndRotation(Vector3.MoveTowards(cameraTransform.position, targetPosition, Time.deltaTime * moveSpeed), Quaternion.Slerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed));

        // Check if the camera has reached the target position and rotation
        if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f && Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1.0f) {
            cameraUpdating = false; // Stop updating once the target is reached
            // Here you can add any functionality you want to occur right after the camera stops moving
        }
    }

    // Looks at the current scenerio and  change if scripts are active or not (camera, UI etx)
    private void UpdateActiveScripts() {
        if (!cameraUpdating) {

            // Example conditions to activate/deactivate the CameraCircle script
            // Adjust these conditions based on your scenarios
            if (activeScenario == "pDesign" || activeScenario == "pUseCase") {
                
                pDesignCamera.enabled = true; // Activate CameraCircle for specific scenarios
                pDesignCamera.SetObjectTag(activeScenario);

                // more if statements for turnoing on and off other cameras
            }
            else {
                DisableCameras(); // Deactivate otherwise
                // deactivate all cameras
            }
        }
        else {
            DisableCameras();
        }
    }

    // disbles all cameras
    private void DisableCameras() {
        pDesignCamera.enabled = false;
    }

    // makes cure the camerCircle Script it set 
    private void Start() {
        // Optionally, find the CameraCircle component at start if not assigned
        if (pDesignCamera == null) {
            pDesignCamera = FindObjectOfType<PDesignCamera>();
        }
    }

    // main update fucntion to see if the camera is updated and if not change the camera behaiours
    void Update() {
        if (cameraUpdating) {
            UpdateCameraTransform();
        }
        else {
            UpdateActiveScripts();
        }
    }
}

// Class for the system paramerters  
[System.Serializable]
public class ScenerioParams {
    public string name;
    public float camHeight;
    public float distToCam;
}
