using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : WeaponScript
{
    private bool collided;
    
    protected override void Awake()
    {
        base.Awake();

        type = BaseWeaponManagerScript.Type.ThrowingKnife;

        Pool = GameObject.FindGameObjectWithTag("KnifeManager").GetComponent<ObjectPoolScript>();
    }

    private void OnEnable()
    {
        collided = false;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }
        
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if (distanceToPlayer >= distanceToDelete)
        {
            Pool.ReturnObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Shield"))
        {
            if (collided)
            {
                return;
            }
            
            Pool.ReturnObject(gameObject);
            
            other.gameObject.GetComponent<ObjectWithStatsScript>().Damage(damage);

            collided = true;
        }
    }

    public override void InitiateAngle(float angle)
    {
        Quaternion q = Quaternion.Euler(0, 0, angle - 45);
        transform.rotation = q;
    }
}
