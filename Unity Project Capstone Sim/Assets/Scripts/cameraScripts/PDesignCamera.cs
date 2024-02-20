/*
PDesignCamera.cs

Script to handle the camera movement when in the PDesign scenerio
*/
using UnityEngine;

// could use the tag switching to alternate around the deisng components 
// pDesignCamera.SetObjectTag(activeScenario);

public class PDesignCamera : MonoBehaviour {
    // the rotation speed of the camera 
    public float rotationSpeed = 60.0f;

    public float sensitivity = 10f; // Sensitivity of the mouse movement
    public float zoomSpeed = 2.0f; // Zoom speed of the camera
    public float minZoomDistance = 2.0f; // Minimum distance to the object
    public float maxZoomDistance = 10.0f; // Maximum distance to the object


    // tag of object the camera will rotate around 
    private string objectTag = "pDesign"; // initially none
    // private TransformInfo transformInfo; // Script for the transform it's looking for based on the tag 
    private new GameObject gameObject;


    private Vector3 previousPosition;

    // function to set the object tag if it needs to be changed
    public void SetObjectTag(string newTag) {
        objectTag = newTag;
    }
    public string GetObjectTag() {
        return objectTag;
    }

    // main update camera logic
    void Update() {
        // Check if the object tag exists and it it does
        if (objectTag != null) {
            // get the game object related to the current tag
            gameObject = GameObject.FindWithTag(objectTag);

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
                transform.RotateAround(gameObject.transform.position, Vector3.up, angleY);

                // Get current rotation
                Vector3 currentRotation = transform.eulerAngles;
                // Modify the pitch (X rotation) based on the mouse movement
                currentRotation.x += angleX;
                // Clamp the X rotation to prevent flipping
                currentRotation.x = Mathf.Clamp(currentRotation.x, -89f, 89f);
                
                // Apply the rotation to the camera
                transform.eulerAngles = currentRotation;

                // Keep looking at the target
                transform.LookAt(gameObject.transform);
            }

                            // Zooming using scroll wheel
            float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
            Vector3 currentPosition = transform.position;
            float distance = Vector3.Distance(currentPosition, gameObject.transform.position);

            distance -= scrollWheelInput * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);

            currentPosition = gameObject.transform.position - transform.forward * distance;
            transform.position = currentPosition;

        }else{
            Debug.Log("No valid objectTag set for CameraCircleObject.");
        }  
    }
}
