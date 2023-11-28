using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RotateAroundY : MonoBehaviour
{
    public float orbitSpeedMin = 0.05f; // Minimum orbit speed
    public float orbitSpeedMax = 0.1f; // Maximum orbit speed
    public float radiusMin = 70f; // Minimum radius
    public float radiusMax = 140f; // Maximum radius

    private float orbitSpeed;
    private float radius;
    private float initialAngle;

    private int direction = -1;

    void Start()
    {
  
        float defaultY = transform.position.y;

        // Set random orbit speed and radius
        orbitSpeed = Random.Range(orbitSpeedMin, orbitSpeedMax);
        radius = Random.Range(radiusMin, radiusMax);


        // Generate a random angle
        float randomAngle = Random.Range(0f, 360f);

        // Convert polar coordinates to Cartesian coordinates
        float x = radius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float z = radius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
        Debug.Log("x:" + x + " z:" + z);

        // Set the object's position
        transform.position = new Vector3(x, defaultY, z);

        //set the initial angle for the fish
        initialAngle = Mathf.Atan2(transform.position.z, transform.position.x) * Mathf.Rad2Deg;



        // Get random bool for flip
        bool flip = Random.Range(0, 2) == 0;


        if (flip)
        {
            direction = 1;
            // Flip the x scale for the object
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }


        // Remove the second child randomly
        //if (transform.childCount > 1)
        //{
        //  int randomChildIndex = Random.Range(0, 3); // 0 or 1
        //Destroy(transform.GetChild(randomChildIndex).gameObject);
        //}

        RandomlyRemoveChildren();


    }

    void Update()
    {
 
        // Calculate the new position based on time, initial angle, orbit speed, and radius
        float angle = direction*(Time.time * orbitSpeed) + initialAngle;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        // Update the object's position
        transform.position = new Vector3(x, transform.position.y, z);

        // Calculate the rotation to look at the center
        Vector3 lookAtPosition = new Vector3(0f, transform.position.y, 0f);
        transform.LookAt(lookAtPosition);
    }



    void RandomlyRemoveChildren()
    {
        int childCount = transform.childCount;

        if (childCount > 1)
        {
            // Determine the number of children to remove
            int childrenToRemove = Random.Range(0, 4); // 0, 1, 2, or 3

            // Remove children based on the number to remove
            for (int i = 0; i < childrenToRemove; i++)
            {
                Destroy(transform.GetChild(childCount - 1 - i).gameObject);
            }
        }
    }
}
