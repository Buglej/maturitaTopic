using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollScript : MonoBehaviour
{
    public CollManagement counter;

    private void Awake()
    {
        counter = GameObject.Find("Collection Obj").GetComponent<CollManagement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            counter.collItems++;
            Destroy(gameObject);
            Debug.Log("Collected");

        }
    }
    
}
