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
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
