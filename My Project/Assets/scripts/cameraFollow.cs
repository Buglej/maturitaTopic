using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y + 2.5f, -10f);
        transform.position = Vector3.Slerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}