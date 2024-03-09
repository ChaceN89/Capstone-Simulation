using System;
using UnityEngine;

// this class contains a series of functions that move the camera that can be called by various scritps

public class CameraFunctions : MonoBehaviour {
    
    // camera movement varibales
    public float rotationSpeed = 5f; // the rotation speed of the camera 
    public float sensitivity = 4f; // Sensitivity of the mouse movement
    public float zoomSpeed = 2.0f; // Zoom speed of the camera



    // for determining if a double click takes place
    private bool isClicked = false;
    private float doubleClickTimer = 0f;
    private readonly float doubleClickThreshold = 0.3f; // Adjust as needed

    // for position of the camera 
    private Vector3 previousPosition; // the previous position for using the mouse



    // to move the camera in and out and around
    public void MoveCamera(string targetName, float minZoom = 2.0f, float maxZoom = 16.0f){
        // Check if the object tag exists and it it does
        if (targetName != null) {
            // get the game object related to the current tag
            GameObject targetObject = GameObject.Find(targetName);


            // get the render for the object
            Renderer targetRenderer = targetObject.GetComponent<Renderer>();
            // Vector3 targetPosition = targetRenderer.bounds.center;;
            Vector3 targetPosition = targetObject.transform.position;
  
            // move around the object 
            MoveCameraAroundObject(targetPosition);

            // zoom into the object 
            ZoomCamera(targetPosition, minZoom, maxZoom);

        }else{
            Debug.Log("No valid Object name set for Camera.");
        }  
    }

    // to rotate the camera around on object when the mouse is cliked
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

    // /for zooming in a and out of the object 
    private void ZoomCamera(Vector3 targetPosition, float minZoom, float maxZoom){
        // Zooming using scroll wheel
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, targetPosition);

        distance -= scrollWheelInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minZoom, maxZoom);

        currentPosition = targetPosition - transform.forward * distance;
        transform.position = currentPosition;
    }


    // checking for a double click to change the target name
    public string CheckMouseClick(string targetName) {
        // check if double click takes place and then set the new target name if it is
        if (Input.GetMouseButtonDown(0)) {
            if (isClicked && Time.time - doubleClickTimer < doubleClickThreshold) {
                // Double click detected
                targetName = OnDoubleClick(targetName);
                isClicked = false; // Reset flag
            } else {
                isClicked = true;
                doubleClickTimer = Time.time;
            }
        } else if (isClicked && Time.time - doubleClickTimer >= doubleClickThreshold) {
            // If no double click occurred within the threshold, reset the flag
            isClicked = false;
        }

        return targetName;
    }

    // make sure its a double click
    private string OnDoubleClick(string targetName) {
        // Prepare a raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform the raycast
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            // Log the name of the object that was hit
            Debug.Log(hit.transform.name);

            targetName = hit.transform.name;
        }
        return  targetName;
        
        
    }



}