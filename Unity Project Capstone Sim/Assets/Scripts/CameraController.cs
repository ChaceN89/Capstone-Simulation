using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 30.0f;

    TransformInfo transformInfo;


    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        transformInfo = playerObject.GetComponent<TransformInfo>();
    }

    void Update()
    {
        // Rotation with A and D keys
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        // Set the object's Y position to be the same as the player's Y position
        Vector3 currentPosition = transform.position;
        currentPosition.y = transformInfo.GetYPosition();
        transform.position = currentPosition;

    }
}

