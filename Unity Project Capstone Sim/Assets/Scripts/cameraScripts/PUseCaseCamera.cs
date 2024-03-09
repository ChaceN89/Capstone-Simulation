/*
PDesiPUseCaseCameragnCamera.cs

Script to handle the camera movement when in the PUseCase scenerio
*/
using UnityEngine;

public class PUseCaseCamera : MonoBehaviour {
    // zoom level for this specific camera
    public float minZoom = 2.0f; // Minimum distance to the object
    public float maxZoom = 16.0f; // Maximum distance to the object

    // the name the camera will rotate around - Valve is in center and is default 
    private string targetName="PUseCaseBoat";

    // for storing the camera functions also attached to the camera
    private CameraFunctions cameraFunctions;

    private void Start() {
        cameraFunctions = FindObjectOfType<CameraFunctions>();
    }

    // reset the snap to the Valve which is the center object
    public void ResetSnap(){
        targetName="PUseCaseBoat";
    }

    // main update camera logic
    void Update() {
        // check for a double mouse click on a component and set the target
        targetName = cameraFunctions.CheckMouseClick(targetName);

        // check for inputs to move the camera around the target
        cameraFunctions.MoveCamera(targetName, minZoom, maxZoom);
    }
}