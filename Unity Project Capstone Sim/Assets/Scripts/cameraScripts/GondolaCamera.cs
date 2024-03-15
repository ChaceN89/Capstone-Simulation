/*
GondolaCamera.cs

Script to handle the camera movement when in the gondola scenerio
*/
using UnityEngine;

public class GondolaCamera : MonoBehaviour {
    // zoom level for this specific camera
    public float minZoom = 4.0f; // Minimum distance to the object
    public float maxZoom = 30.0f; // Maximum distance to the object

    // the name the camera will rotate around - Valve is in center and is default 
    private string targetName="OffShorePlatform";

    // for storing the camera functions also attached to the camera
    private CameraFunctions cameraFunctions;

    private void Start() {
        cameraFunctions = FindObjectOfType<CameraFunctions>();
    }

    // reset the snap to the Valve which is the center object
    public void ResetSnap(){
        targetName = "OffShorePlatform";
    }

    // main update camera logic
    void LateUpdate() {
        // check for a double mouse click on a component and set the target
        targetName = cameraFunctions.CheckMouseClick(targetName);

        // check for inputs to move the camera around the target
        cameraFunctions.MoveCamera(targetName, minZoom, maxZoom);
    }
}