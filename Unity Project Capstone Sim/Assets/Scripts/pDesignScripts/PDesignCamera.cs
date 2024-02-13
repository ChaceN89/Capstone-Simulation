using UnityEngine;

public class PDesignCamera : MonoBehaviour {
    // the rotation speed of the camera 
    public float rotationSpeed = 60.0f;

    // tag of object the camera will rotate around 
    private string objectTag = null; // initially none
    private TransformInfo transformInfo; // Script for the transform it's looking for based on the tag 
    private new GameObject gameObject;

    // function to set the object tag if it needs to be changed
    public void SetObjectTag(string newTag) {
        objectTag = newTag;
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
        }else{
            Debug.Log("No valid objectTag set for CameraCircleObject.");
        }
    }
}
