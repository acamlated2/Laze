using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    protected BaseWeaponManagerScript.Type type;

    [SerializeField] protected float speed = 20;
    [SerializeField] protected float distanceToDelete = 50;
    [SerializeField] protected float damage = 10;
    
    protected GameObject player;

    protected ObjectPoolScript Pool;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void InitiateAngle(float angle)
    {
        
    }
}
