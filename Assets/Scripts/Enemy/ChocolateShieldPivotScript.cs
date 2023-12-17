using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateShieldPivotScript : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] [Min(0.1f)] private float rotationSpeed = 3;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        float angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angleToTarget),
            rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }
}
