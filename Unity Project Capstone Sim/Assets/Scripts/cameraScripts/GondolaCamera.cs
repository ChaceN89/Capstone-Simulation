/*
GondolaCamera.cs

Script to handle the camera movement when in the gondola scenerio
*/
using UnityEngine;

public class GondolaCamera : MonoBehaviour {
    public float speed = 3f; // Speed of the vertical movement
    public float height = 0.5f; // Height of the oscillation

    private Vector3 startPosition;

    private void Start() {
        // Initialize the starting position of the camera
        startPosition = transform.position;
    }

    private void Update() {
        // Calculate the new Y position using the sine of time
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;
        // Set the camera's position to the new Y value, keeping X and Z the same
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Call this method to update the starting position whenever the camera is moved by another script
    public void UpdateStartPosition() {
        startPosition = transform.position;
    }
}