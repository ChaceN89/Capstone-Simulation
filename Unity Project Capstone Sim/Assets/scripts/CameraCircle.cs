using UnityEngine;

public class CameraCircle : MonoBehaviour {
    public float rotationSpeed = 30.0f;

    private string objectTag = "none"; // initially none
    private TransformInfo transformInfo; // Script for the transform it's looking for based on the tag 

    void Start() {

    }

    void SetObjectTag(string newTag) {
        objectTag = newTag;
    }

    void Update() {
        // get the object i am looking at 
        GameObject playerObject = GameObject.FindWithTag(objectTag);
        transformInfo = playerObject.GetComponent<TransformInfo>();

        // Rotation with A and D keys
        if (Input.GetKey(KeyCode.A)) {
            transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.RotateAround(Vector3.zero, Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        // Set the object's Y position to be the same as the player's Y position
        Vector3 currentPosition = transform.position;
        currentPosition.y = transformInfo.GetYPosition();
        transform.position = currentPosition;
    }

}
