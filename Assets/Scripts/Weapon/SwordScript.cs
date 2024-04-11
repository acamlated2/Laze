using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : WeaponScript
{
    [SerializeField] [Min(0.1f)] private float swordTimer = 0.25f;
    private float swordTimerDefault;
    
    protected override void Awake()
    {
        base.Awake();

        type = BaseWeaponManagerScript.Type.Sword;

        Pool = GameObject.FindGameObjectWithTag("SwordManager").GetComponent<ObjectPoolScript>();

        swordTimerDefault = swordTimer;
    }

    private void OnEnable()
    {
        swordTimer = swordTimerDefault;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }
        
        transform.position = player.transform.position;
        
        swordTimer -= 1 * Time.deltaTime;

        Vector3 rotation = transform.rotation.eulerAngles;
        
        if (swordTimer <= 0)
        {
            Pool.ReturnObject(gameObject);
        }
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y,
            rotation.z - 360 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Shield"))
        {
            other.gameObject.GetComponent<ObjectWithStatsScript>()
                 .Damage(damage * player.GetComponent<ObjectWithStatsScript>().damageMultiplier);
        }
    }

    public override void InitiateAngle(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        
        float startAngle = rotation.eulerAngles.z + 45;
        
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, startAngle);
    }
}
