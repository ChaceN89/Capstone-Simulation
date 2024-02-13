/*
PDesiPUseCaseCameragnCamera.cs

Script to handle the camera movement when in the PUseCase scenerio
*/
using UnityEngine;

public class PUseCaseCamera : MonoBehaviour {
    public Transform target; // The target the camera should orbit around
    public float sensitivity = 10f; // Sensitivity of the mouse movement

    private Vector3 previousPosition;

    private void Update() {
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
            transform.RotateAround(target.position, Vector3.up, angleY);

            // Get current rotation
            Vector3 currentRotation = transform.eulerAngles;
            // Modify the pitch (X rotation) based on the mouse movement
            currentRotation.x += angleX;
            // Clamp the X rotation to prevent flipping
            currentRotation.x = Mathf.Clamp(currentRotation.x, -89f, 89f);
            
            // Apply the rotation to the camera
            transform.eulerAngles = currentRotation;

            // Keep looking at the target
            transform.LookAt(target);
        }
    }
}