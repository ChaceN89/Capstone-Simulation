using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 30.0f;
    public float translationSpeed = 3.0f;

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

        // Translation with W and S keys
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * translationSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * translationSpeed * Time.deltaTime, Space.World);
        }
    }
}

