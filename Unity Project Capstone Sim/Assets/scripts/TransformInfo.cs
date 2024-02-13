//  to attach to an object to easily get the transform info of an object 
// returns may different function used to get values from the object this script is attached to
using UnityEngine;

public class TransformInfo : MonoBehaviour {
    public float GetYPosition() => transform.position.y;
    public float GetXPosition() => transform.position.x;
    public float GetZPosition() => transform.position.z;
    public Transform GetTransform() => transform;

    public Vector3 GetRotationEuler() => transform.eulerAngles;
    public Quaternion GetRotationQuaternion() => transform.rotation;
    public Vector3 GetScale() => transform.localScale;

    public void SetPosition(Vector3 newPosition) => transform.position = newPosition;
    public void SetRotationEuler(Vector3 newEulerAngles) => transform.eulerAngles = newEulerAngles;
    public void SetRotationQuaternion(Quaternion newRotation) => transform.rotation = newRotation;
    public void SetScale(Vector3 newScale) => transform.localScale = newScale;

    public void Translate(Vector3 translation) => transform.Translate(translation);
    public void Rotate(Vector3 eulerAngles) => transform.Rotate(eulerAngles);
    public void RotateQuaternion(Quaternion rotation) => transform.rotation *= rotation;

    public void LookAt(Transform target) => transform.LookAt(target);
    public void LookAt(Vector3 point) => transform.LookAt(point);

    public Vector3 GetForward() => transform.forward;
    public Vector3 GetRight() => transform.right;
    public Vector3 GetUp() => transform.up;

    public void ResetTransform() {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;
    }
}

/*
for accessing this script

    GameObject playerObject = GameObject.FindWithTag("Player");
    TransformInfo playerTransformInfo = playerObject.GetComponent<TransformInfo>();
 */