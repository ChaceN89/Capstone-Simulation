/*
PDesiPUseCaseCameragnCamera.cs

Script to handle the camera movement when in the PUseCase scenerio
*/
using UnityEngine;

public class PUseCaseCamera : MonoBehaviour {

    // can chagne script as needed as it will have to follow the PUsecase at some point 

    public Transform target; // The target the camera should orbit around
    public float rotationSpeed = 4f; // the rotation speed of the camera 
    public float sensitivity = 5f; // Sensitivity of the mouse movement

    public float zoomSpeed = 2.0f; // Zoom speed of the camera
    public float minZoomDistance = 2.0f; // Minimum distance to the object
    public float maxZoomDistance = 10.0f; // Maximum distance to the object

    private Vector3 previousPosition;

    private void Update() {

        Vector3 newPosition = target.position;
        MoveCameraAroundObject(newPosition);
        ZoomCamera(newPosition);
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

}