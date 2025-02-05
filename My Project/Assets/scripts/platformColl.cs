using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformColl : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Get the velocity of the colliding object relative to this object
        Vector3 relativeVelocity = collision.relativeVelocity;

        // Check if the collision is from the bottom (y-axis)
        if (relativeVelocity.y < 0) 
        {
            // Ignore the collision
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            Debug.Log("Collision from bottom ignored!");
        }
        else
        {
            // Handle the collision as usual
            Debug.Log("Collision detected!");
        }
    }
}
