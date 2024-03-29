using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : ObjectWithStatsScript
{
    public enum Type
    {
        Sushi, 
        Cream, 
        Chocolate, 
        GrapeJuice
    }

    public Type type = Type.Sushi;
    
    [SerializeField] private GameObject expPrefab;
    
    protected NavMeshAgent agent;

    [SerializeField] protected float damage = 10;

    public GameObject target;
    protected GameObject player;

    public float panicDistance = 5;
    
    public float playerDistance;

    protected override void Awake()
    {
        base.Awake();
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        gameController = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;

        var gameControllerScript = gameController.GetComponent<GameControllerScript>();
        
        gameControllerScript.AddToList(gameControllerScript.enemies, gameObject);
    }

    protected override void Update()
    {
        base.Update();
        
        var gameControllerScript = gameController.GetComponent<GameControllerScript>();
        target = gameControllerScript.GetClosestTarget(transform);

        if (player != null)
        {
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
        }
    }
    
    protected virtual void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        
        //agent.SetDestination(target.transform.position);
    }

    public override void Damage(float damage)
    {
        health -= damage;
        healthBar.GetComponent<HealthBarScript>().ChangeHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    protected override void Attack()
    {
        // melee
        target.GetComponent<ObjectWithStatsScript>().Damage(damage);
    }

    protected void SpawnExp()
    {
        GameObject newExp = Instantiate(expPrefab, transform.position, Quaternion.identity);
        var newExpScript = newExp.GetComponent<ExpScript>();
        
        switch (type)
        {
            case Type.Sushi:
                newExpScript.SetValue(5);
                break;
        }
    }

    protected virtual void Die()
    {
        var gameControllerScript = gameController.GetComponent<GameControllerScript>();
        gameControllerScript.enemies.Remove(gameObject);
            
        SpawnExp();
            
        Destroy(gameObject);
    }
}
