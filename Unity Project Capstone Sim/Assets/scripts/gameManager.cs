using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform gondolaTransform;
    public Transform pUseCaseTransform;
    public Transform pDesignTransform;
    public Transform cameraTransform;
    public float rotationSpeed = 0.5f;
    public float moveSpeed = 5f;
    public float distanceFromObject = 10f;

    private Transform targetObjectTransform;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool cameraUpdating = false;

    

    // This method sets the camera's target and starts the movement
    private void MoveCameraToTarget(Transform target)
    {
        targetObjectTransform = target;

        // Calculate the direction from the origin to the target on the XZ plane
        Vector3 directionFromOrigin = (new Vector3(target.position.x, 0f, target.position.z)).normalized;

        // Set the target position to be in line with the target from the origin, at the specified distance, and at y = 2
        targetPosition = target.position - directionFromOrigin * distanceFromObject;
        targetPosition.y = 3f; // Set the camera height to 2

        // Make sure the camera looks at the target
        targetRotation = Quaternion.LookRotation(target.position - targetPosition);

        cameraUpdating = true;
    }

    // Call this method when the button for "gondolaSim" is clicked
    public void OnGondolaSimButtonClicked()
    {
        MoveCameraToTarget(gondolaTransform);
    }

    // Call this method when the button for "prototypeDesign" is clicked
    public void OnPrototypeDesignButtonClicked()
    {
        MoveCameraToTarget(pDesignTransform);
    }

    // Call this method when the button for "prototypeUseCase" is clicked
    public void OnPrototypeUseCaseButtonClicked()
    {
        MoveCameraToTarget(pUseCaseTransform);
    }

    void Update()
    {
        if (cameraUpdating)
        {
            // Move the camera towards the target position
            cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, targetPosition, Time.deltaTime * moveSpeed);
            
            // Rotate the camera towards the target rotation
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Check if the camera has reached the target position and rotation
            if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f && Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1.0f)
            {
                cameraUpdating = false; // Stop updating once the target is reached
                // Here you can add any functionality you want to occur right after the camera stops moving
            }
        }
        // Else the camera is free to be manipulated by the user or other scripts
    }
}
