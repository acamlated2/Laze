using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float distanceToDelete = 50;
    [SerializeField] private float damage = 10;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);

        float distanceToPlayer = distanceToDelete;
        
        if (_player != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        }
        
        if (distanceToPlayer >= distanceToDelete)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().Damage(damage);
            
            Destroy(gameObject);
        }
    }
}
