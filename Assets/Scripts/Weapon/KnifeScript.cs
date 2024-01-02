using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : WeaponScript
{
    protected override void Awake()
    {
        base.Awake();

        type = BaseWeaponManagerScript.Type.ThrowingKnife;
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
            
            other.gameObject.GetComponent<ObjectWithStatsScript>().Damage(damage);
        }
    }

    public override void InitiateAngle(float angle)
    {
        Quaternion q = Quaternion.Euler(0, 0, angle - 45);
        transform.rotation = q;
    }
}
