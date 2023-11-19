using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent _agent;

    private GameObject _player;

    [SerializeField] private float health = 50;

    private GameObject _gameController;

    [SerializeField] private float damage = 10;

    private bool _attackingMelee;
    private float _meleeTimer;
    private float _meleeTimerMax = 1;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        _player = GameObject.FindGameObjectWithTag("Player");

        _gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void Update()
    {
        var gameControllerScript = _gameController.GetComponent<GameControllerScript>();

        GameObject target = gameControllerScript.GetClosestTarget(transform);
        _agent.SetDestination(target.transform.position);
        
        HandleAttacks();
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _attackingMelee = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _attackingMelee = false;

            _meleeTimer = 0;
        }
    }

    private void HandleAttacks()
    {
        if (_attackingMelee)
        {
            _meleeTimer -= 1 * Time.deltaTime;

            if (_meleeTimer <= 0)
            {
                _meleeTimer = _meleeTimerMax;
                
                _gameController.GetComponent<GameControllerScript>().DamagePlayer(damage);
            }
        }
    }
}
