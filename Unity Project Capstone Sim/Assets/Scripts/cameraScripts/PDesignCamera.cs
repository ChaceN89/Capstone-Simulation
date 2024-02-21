/*
PDesignCamera.cs

Script to handle the camera movement when in the PDesign scenerio
*/
using UnityEngine;

// could use the tag switching to alternate around the deisng components 
// pDesignCamera.SetObjectTag(activeScenario);

public class PDesignCamera : MonoBehaviour {
    
    public float rotationSpeed = 60.0f; // the rotation speed of the camera 
    public float sensitivity = 10f; // Sensitivity of the mouse movement
    public float zoomSpeed = 2.0f; // Zoom speed of the camera
    public float minZoomDistance = 2.0f; // Minimum distance to the object
    public float maxZoomDistance = 10.0f; // Maximum distance to the object


    // the name the camera will rotate around
    private string targetName="Valve";


 
    
    private Vector3 previousPosition; // the previous position for using the mouse



    public void ResetSnap(){
        targetName = "Valve";
    }



    // main update camera logic
    void Update() {

        CheckMouseClick();

        // Check if the object tag exists and it it does
        if (targetName != null) {
            // get the game object related to the current tag
            GameObject targetObject = GameObject.Find(targetName);
            Renderer targetRenderer = targetObject.GetComponentInChildren<Renderer>();
            Vector3 targetPosition = targetRenderer.bounds.center;;
  

            // rotate and zoom around the object 
            if (Input.GetMouseButtonDown(0)) {
                previousPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0)) {
                Vector3 delta = Input.mousePosition - previousPosition;
                previousPosition = Input.mousePosition;

                // Calculate the new rotation around the Y axis (yaw)
                float angleY = delta.x * sensitivity * Time.deltaTime;
                // Calculate the new rotation around the X axis (pitch)
                float angleX = -delta.y * sensitivity * Time.deltaTime;

                // Rotate the camera around the target on the Y axis
                transform.RotateAround(targetPosition, Vector3.up, angleY);

                // Get current rotation
                Vector3 currentRotation = transform.eulerAngles;
                // Modify the pitch (X rotation) based on the mouse movement
                currentRotation.x += angleX;
                // Clamp the X rotation to prevent flipping
                currentRotation.x = Mathf.Clamp(currentRotation.x, -89f, 89f);
                
                // Apply the rotation to the camera
                transform.eulerAngles = currentRotation;

                // Keep looking at the target
                transform.LookAt(targetPosition);
            }

                            // Zooming using scroll wheel
            float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
            Vector3 currentPosition = transform.position;
            float distance = Vector3.Distance(currentPosition, targetPosition);

            distance -= scrollWheelInput * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);

            currentPosition = targetPosition - transform.forward * distance;
            transform.position = currentPosition;

        }else{
            Debug.Log("No valid objectTag set for CameraCircleObject.");
        }  
    }
    private void CheckMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
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

}
