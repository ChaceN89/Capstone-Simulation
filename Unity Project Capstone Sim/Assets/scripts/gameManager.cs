/*
gameManager.cs

this script is the main driver for the game
it has a couple of important functionalities 

has functions that when activated move the camera to 1 of 3 transforms
it will also active UI and camera controllers for each of scenerios 

*/

using UnityEngine;

public class GameManager : MonoBehaviour {

    // Variables that control camera movement speed and distance etc for main camera motion 
    public float rotationSpeed = 0.5f; // rotation speed of the camera
    public float moveSpeed = 5f; // speed of the camera
    public Transform mainCameraTransform; // main camera transform 
    public string activeScenario = "none";  // The current scenerio 


    // Camera height and distances to object for each scenerio
    public ScenerioParams pDesignParams = new() {name = "pDesign", camHeight = 4f, distToCam = 5f };
    public ScenerioParams pUseCaseParams = new() {name = "pUseCase", camHeight = 6f, distToCam = 6f };
    public ScenerioParams gondolaParams = new() {name = "gondola", camHeight = 8f, distToCam = 15f };

    // camera movement script for each of the 3 scenarios
    private PDesignCamera pDesignCamera; 
    private PUseCaseCamera pUseCaseCamera; 
    private GondolaCamera gondolaCamera; 

    // references to the scripts for controlling backend components for each of the 3 scenerios 
    private GondolaBackend gondolaBackend;
    private PDesignBackend pDesignBackend;
    private PUseCaseBackend pUseCaseBackend;

    // private 
    private bool cameraUpdating = false;
    private Vector3 targetPosition;  // The target position and rotation for the camera to move to - one of the 3 scenerio transforms  -and the main camera
    private Quaternion targetRotation; // the rotation of the target to snap to 


    // ------------ Getters -----------
    public string GetActiveScenerio() => activeScenario;
    

    // ----------- Functions triggered by buttons ----------
    // sets the active scenario and updates the camera behavious 
    public void SetActiveScenario(ScenerioParams scenario ) {
        activeScenario = scenario.name;
        MoveCameraToTarget(scenario, scenario.target);
        UpdateActiveScripts(); // Update camera behavior and other scripts based on the new active scenario
    }
    // Call this method when the button for "gondolaSim" is clicked
    public void OnGondolaSimButtonClicked() => SetActiveScenario(gondolaParams);
    
    // Call this method when the button for "prototypeDesign" is clicked
    public void OnPrototypeDesignButtonClicked() => SetActiveScenario(pDesignParams);
    
    // Call this method when the button for "prototypeUseCase" is clicked
    public void OnPrototypeUseCaseButtonClicked() => SetActiveScenario(pUseCaseParams);
    
    // Function to enable/disable a canvas or disable all canvuses
    private void SetCanvasEnabled(Canvas canvas, bool isEnabled) => canvas.enabled = isEnabled;

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
        mainCameraTransform.SetPositionAndRotation(Vector3.MoveTowards(mainCameraTransform.position, targetPosition, Time.deltaTime * moveSpeed), Quaternion.Slerp(mainCameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed));

        // Check if the camera has reached the target position and rotation
        if (Vector3.Distance(mainCameraTransform.position, targetPosition) < 0.1f && Quaternion.Angle(mainCameraTransform.rotation, targetRotation) < 1.0f) {
            cameraUpdating = false; // Stop updating once the target is reached
            // Here you can add any functionality you want to occur right after the camera stops moving
        }
    }

    // Looks at the current scenerio and  change if scripts are active or not (camera, UI etx)
    private void UpdateActiveScripts() {
        // if camera is not updating we can show the correct UI and activate the corrcet camera
        if (!cameraUpdating) {
            if (activeScenario == "pDesign" ){
                pDesignCamera.enabled = true;
                SetCanvasEnabled(pDesignParams.canvas, true);
                
            }else if( activeScenario == "pUseCase") {
                pUseCaseCamera.enabled = true;
                SetCanvasEnabled(pUseCaseParams.canvas, true);
            }
            else if (activeScenario == "gondola") {
                gondolaCamera.enabled = true;
                SetCanvasEnabled(gondolaParams.canvas, true);

            }else{ // disable everything if the wrong scenerio is called
                DisableCameras(); 
                DeactivateAllCanvases();
                ResetBackendScripts();
            }

        // the camera is updating and everything should be reset  
        }else {
            DisableCameras();
            DeactivateAllCanvases();
            ResetBackendScripts();
        }
    }

    // disbles all cameras
    private void DisableCameras() {
        pDesignCamera.enabled = false;
        pUseCaseCamera.enabled = false;
        gondolaCamera.enabled = false;
    }

    private void ResetBackendScripts(){
        gondolaBackend.Reset();
        pDesignBackend.Reset();
        pUseCaseBackend.Reset();
    } 

        // Disable all the canvases 
    private void DeactivateAllCanvases(){
        SetCanvasEnabled(gondolaParams.canvas, false);
        SetCanvasEnabled(pDesignParams.canvas, false);
        SetCanvasEnabled(pUseCaseParams.canvas, false);
    }

    // makes cure the camerCircle Script it set 
    private void Start() {
        // find all the cameras
        pDesignCamera = FindObjectOfType<PDesignCamera>();
        pUseCaseCamera = FindObjectOfType<PUseCaseCamera>();
        gondolaCamera = FindObjectOfType<GondolaCamera>();

        // find all the backend scripts
        gondolaBackend = FindObjectOfType<GondolaBackend>();
        pDesignBackend = FindObjectOfType<PDesignBackend>();
        pUseCaseBackend = FindObjectOfType<PUseCaseBackend>();

        // turn off the camera initially and turn off the canvases 
        DisableCameras();
        DeactivateAllCanvases();
        // ResetBackendScripts();
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


// Class for the system paramerters for each scenerio 
[System.Serializable]
public class ScenerioParams {
    public string name; // name of the scenerio
    public float camHeight; // the height of the object to the camera when it moves to it
    public float distToCam; // the distacne of the object to the camera when it moves to it
    public Canvas canvas; // the UI for the scenerio 
    public Transform target; // the location of the scenerio
}
