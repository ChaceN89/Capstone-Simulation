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
    private TransformInfo transformInfo; // Script for the transform it's looking for based on the tag 
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

            // get the transform info and if not Degub 
            if (gameObject.TryGetComponent<TransformInfo>(out transformInfo)) {
                // Use transformInfo's position for RotateAround


                if (Input.GetMouseButtonDown(0)) {
                    previousPosition = Input.mousePosition;
                    Debug.Log("prvious position " + previousPosition );
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
                float distance = Vector3.Distance(currentPosition, transformInfo.GetTransform().position);

                distance -= scrollWheelInput * zoomSpeed;
                distance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);

                currentPosition = transformInfo.GetTransform().position - transform.forward * distance;
                transform.position = currentPosition;



                // if (Input.GetKey(KeyCode.A)) {
                //     transform.RotateAround(transformInfo.GetTransform().position, Vector3.up, rotationSpeed * Time.deltaTime);
                // }
                // if (Input.GetKey(KeyCode.D)) {
                //     transform.RotateAround(transformInfo.GetTransform().position, Vector3.up, -rotationSpeed * Time.deltaTime);
                // }

                // // Update the camera's Y position to match the object's Y position, maintaining the current height offset
                // Vector3 currentPosition = transform.position;
                // float heightOffset = currentPosition.y - transformInfo.GetTransform().position.y; // Calculate the current height difference
                // currentPosition.y = transformInfo.GetYPosition() + heightOffset;
                // transform.position = currentPosition;

            } else {
                Debug.LogError("TransformInfo component not found on object with tag " + objectTag);
            }
        }else{
            Debug.Log("No valid objectTag set for CameraCircleObject.");
        }
    }
}
