/*
PDesignCamera.cs

Script to handle the camera movement when in the PDesign scenerio
*/
using UnityEngine;

// could use the tag switching to alternate around the deisng components 
// pDesignCamera.SetObjectTag(activeScenario);

public class PDesignCamera : MonoBehaviour {
    
    public float rotationSpeed = 5f; // the rotation speed of the camera 
    public float sensitivity = 4f; // Sensitivity of the mouse movement
    public float zoomSpeed = 2.0f; // Zoom speed of the camera
    public float minZoomDistance = 2.0f; // Minimum distance to the object
    public float maxZoomDistance = 10.0f; // Maximum distance to the object

    // the name the camera will rotate around - Valve is in center and is default 
    private string targetName="Valve";

    private Vector3 previousPosition; // the previous position for using the mouse


    // variable for double click 
    private bool isClicked = false;
    private float doubleClickTimer = 0f;
    private float doubleClickThreshold = 0.3f; // Adjust as needed


    // reset the snap to the Valve which is the center object
    public void ResetSnap(){
        targetName = "Valve";
    }

    // main update camera logic
    void Update() {
        // check for a double mouse click on a component
        CheckMouseClick();

        // check for inputs to move the camera
        MoveCamera();
    }


    private void MoveCamera(){
        // Check if the object tag exists and it it does
        if (targetName != null) {
            // get the game object related to the current tag
            GameObject targetObject = GameObject.Find(targetName);
            Renderer targetRenderer = targetObject.GetComponentInChildren<Renderer>();
            Vector3 targetPosition = targetRenderer.bounds.center;;
  
            // move around the object 
            MoveCameraAroundObject(targetPosition);

            // zoom into the object 
            ZoomCamera(targetPosition);

        }else{
            Debug.Log("No valid objectTag set for CameraCircleObject.");
        }  
    }


    private void MoveCameraAroundObject(Vector3 targetPosition){

            // rotate and zoom around the object 
            if (Input.GetMouseButtonDown(0)) {
                previousPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0)) {
                Vector3 delta = Input.mousePosition - previousPosition;
                previousPosition = Input.mousePosition;

                // Calculate the rotation angles based on mouse movement
                float angleY = delta.x * sensitivity * Time.deltaTime * rotationSpeed;
                float angleX = -delta.y * sensitivity * Time.deltaTime * rotationSpeed;

                // Rotate the camera around the target on both the Y and X axes
                transform.RotateAround(targetPosition, Vector3.up, angleY);
                transform.RotateAround(targetPosition, transform.right, angleX);

                // Keep looking at the target
                transform.LookAt(targetPosition);
            }

    }


    private void ZoomCamera(Vector3 targetPosition){
        // Zooming using scroll wheel
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, targetPosition);

        distance -= scrollWheelInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);

        currentPosition = targetPosition - transform.forward * distance;
        transform.position = currentPosition;
    }




    private void CheckMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            if (isClicked && Time.time - doubleClickTimer < doubleClickThreshold) {
                // Double click detected
                OnDoubleClick();
                isClicked = false; // Reset flag
            } else {
                isClicked = true;
                doubleClickTimer = Time.time;
            }
        } else if (isClicked && Time.time - doubleClickTimer >= doubleClickThreshold) {
            // If no double click occurred within the threshold, reset the flag
            isClicked = false;
        }
    }

    private void OnDoubleClick() {
        // Prepare a raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform the raycast
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            // Log the name of the object that was hit
            Debug.Log(hit.transform.name);

            targetName = hit.transform.name;
        }
        
    }

}
