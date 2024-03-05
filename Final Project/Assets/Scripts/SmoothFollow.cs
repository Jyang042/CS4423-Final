using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [Header("Camera Stats")]
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed = 5;

    [Header("Boundaries")]
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;


    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            
        }
    }
    public void AssignTarget(Transform newTarget){
        target = newTarget;
    }
}