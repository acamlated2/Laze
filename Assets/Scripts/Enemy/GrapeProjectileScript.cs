using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GrapeProjectileScript : MonoBehaviour
{
    [SerializeField] private float lifetime = 3;
    private float lifetimeDefault;
    [SerializeField] private float maxDamage = 20;
    [SerializeField] private float speed = 15;
    [SerializeField] private float range = 10;

    private bool _animating;
    private float _scale;
    private float _scaleDefault;
    private float _scalingSpeed;
    private float _scalingSpeedDefault;

    private ObjectPoolScript _pool;

    public GameObject owner;

    private void Awake()
    {
        _scale = transform.localScale.x;
        _scaleDefault = _scale;

        _scalingSpeedDefault = _scalingSpeed;

        lifetimeDefault = lifetime;

        _pool = GameObject.FindGameObjectWithTag("GrapeJuiceProjectileObjectPool").GetComponent<ObjectPoolScript>();
    }
    
    private void OnEnable()
    {
        lifetime = lifetimeDefault;

        _scale = _scaleDefault;
        transform.localScale = new Vector3(_scale, _scale, 1);

        _scalingSpeed = _scalingSpeedDefault;
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.up;
        Vector3 velocity = direction * speed * Time.deltaTime;

        transform.position += velocity;
    }

    private void Update()
    {
        lifetime -= 1 * Time.deltaTime;
        if (lifetime <= 0)
        {
            Explode();
        }

        if (_animating)
        {
            _scalingSpeed = range / Time.deltaTime;
            _scale += _scalingSpeed * Time.deltaTime;

            transform.localScale = new Vector3(_scale, _scale, 1);

            if (_scale >= range * 5)
            {
                _animating = false;
                transform.localScale = new Vector3(_scaleDefault, _scaleDefault, 1);
                _pool.ReturnObject(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        ObjectWithStatsScript[] damageableObjects = FindObjectsOfType<ObjectWithStatsScript>();

        for (int i = 0; i < damageableObjects.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, damageableObjects[i].transform.position);
            if (distance > range)
            {
                continue;
            }

            float damage = (1 - distance / range) * maxDamage;
            damageableObjects[i].GetComponent<ObjectWithStatsScript>()
                                .Damage(damage * owner.GetComponent<ObjectWithStatsScript>().damageMultiplier);
        }

        _animating = true;
    }
    
    public void SetRotation(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        transform.rotation = quaternion.LookRotation(dir, Vector3.forward);
    }
}
