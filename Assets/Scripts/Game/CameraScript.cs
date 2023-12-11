using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform _player;
    private float _offset = -10;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_player != null)
        {
            Vector3 desiredPos = _player.position;
            desiredPos.z += _offset;
        
            transform.position = desiredPos;
        }
    }
}
