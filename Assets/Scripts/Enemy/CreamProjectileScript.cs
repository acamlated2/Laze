using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CreamProjectileScript : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float speed = 15;
    [SerializeField] private float lifetime = 5;
    private float _lifetimeDefault;

    private ObjectPoolScript _pool;
    private void FixedUpdate()
    {
        Vector3 direction = transform.up;
        Vector3 velocity = direction * speed * Time.deltaTime;

        transform.position += velocity;

        _lifetimeDefault = lifetime;

        _pool = GameObject.FindGameObjectWithTag("CreamProjectileObjectPool").GetComponent<ObjectPoolScript>();
    }

    private void OnEnable()
    {
        lifetime = _lifetimeDefault;
    }

    private void Awake()
    {
        lifetime -= 1 * Time.deltaTime;
        if (lifetime <= 0)
        {
            _pool.ReturnObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _pool.ReturnObject(gameObject);

            other.GetComponent<ObjectWithStatsScript>().Damage(damage);
        }
    }

    public void SetRotation(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        transform.rotation = quaternion.LookRotation(dir, Vector3.forward);
    }
}
