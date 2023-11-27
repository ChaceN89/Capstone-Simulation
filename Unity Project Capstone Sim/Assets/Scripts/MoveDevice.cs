using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDevice : MonoBehaviour
{

    public float translationSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
