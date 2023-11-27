using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformInfo : MonoBehaviour{

    // Function to get the Y position of the object
    public float GetYPosition(){
        return transform.position.y;
    }

    // Function to get the X position of the object
    public float GetXPosition(){
        return transform.position.x;
    }

    // Function to get the Z position of the object
    public float GetZPosition(){
        return transform.position.z;
    }

    // Function to get the Transform of the object
    public Transform GetTransform(){
        return transform;
    }
}


/*
for accessing this script

    pass an object to this script
    public GameObject playerObject; // Assign the player object in the Unity Editor

    in start get the script of the obejct 
        TransformInfo playerDepthScript = playerObject.GetComponent<TransformInfo>();

    playerDepthScript.fucntions()


Or use in start
    GameObject playerObject = GameObject.FindWithTag("Player");
    TransformInfo playerDepthScript = playerObject.GetComponent<TransformInfo>();


 */