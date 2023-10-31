using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float _offset = -10;

    private void Update()
    {
        Vector3 desiredPos = target.transform.position;
        desiredPos.z += _offset;
        
        transform.position = desiredPos;
    }
}
