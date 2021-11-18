using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float followSpeed = 1f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPostion = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPostion, followSpeed);
        transform.position = smoothedPosition;        
    }
}
