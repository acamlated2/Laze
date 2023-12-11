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
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        
        float distanceToPlayer = Vector2.Distance(transform.position, playerSpawnPos);
        
        if (distanceToPlayer >= distanceToDelete)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            
            other.gameObject.GetComponent<EnemyScript>().Damage(damage);
        }
    }
}
