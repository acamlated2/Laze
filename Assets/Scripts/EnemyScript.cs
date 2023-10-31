using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent _agent;

    private GameObject _player;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }
}
