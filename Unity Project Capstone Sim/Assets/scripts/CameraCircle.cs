using UnityEngine;

public class CameraCircle : MonoBehaviour {
    public float rotationSpeed = 30.0f;

    private string objectTag = "gondola"; // initially none
    private TransformInfo transformInfo; // Script for the transform it's looking for based on the tag 
    private new GameObject gameObject;
    void Start() {

    }

    public void SetObjectTag(string newTag) {
        objectTag = newTag;
    }

    void Update() {
        // get the object i am looking at 
        gameObject = GameObject.FindWithTag(objectTag);
        
        // get eh transform info 
        if (gameObject.TryGetComponent<TransformInfo>(out transformInfo)) {
            // Use transformInfo's position for RotateAround
            if (Input.GetKey(KeyCode.A)) {
                transform.RotateAround(transformInfo.GetTransform().position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D)) {
                transform.RotateAround(transformInfo.GetTransform().position, Vector3.up, -rotationSpeed * Time.deltaTime);
            }

            // Update the camera's Y position to match the object's Y position, maintaining the current height offset
            Vector3 currentPosition = transform.position;
            float heightOffset = currentPosition.y - transformInfo.GetTransform().position.y; // Calculate the current height difference
            currentPosition.y = transformInfo.GetYPosition() + heightOffset;
            transform.position = currentPosition;

        } else {
            Debug.LogError("TransformInfo component not found on object with tag " + objectTag);
        }
    }
}
