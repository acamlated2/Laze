using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CreamProjectileScript : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float speed = 15;
    private void FixedUpdate()
    {
        Vector3 direction = transform.up;
        Vector3 velocity = direction * speed * Time.deltaTime;

        transform.position += velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            other.GetComponent<ObjectWithStatsScript>().Damage(damage);
        }
    }

    public void SetRotation(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        transform.rotation = quaternion.LookRotation(dir, Vector3.forward);
    }
}
