using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    protected WeaponManagerScript.Type type;

    [SerializeField] protected float speed = 10;
    [SerializeField] protected float distanceToDelete = 50;
    [SerializeField] protected float damage = 10;
    
    protected GameObject player;
    protected Vector3 playerSpawnPos;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Initiate(float angle)
    {
        Quaternion q = Quaternion.Euler(0, 0, angle - 45);
        transform.rotation = q;
    }
}
