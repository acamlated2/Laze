using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;

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
    
    public bool shouldTargetPlayer;
    protected GameObject Player;

    public float panicDistance = 5;
    
    public float playerDistance;

    private ObjectPoolScript _expPool;

    protected override void Awake()
    {
        base.Awake();
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GameController = GameObject.FindGameObjectWithTag("GameController");
        Player = GameObject.FindGameObjectWithTag("Player");
        target = Player;

        _expPool = GameObject.FindGameObjectWithTag("ExpObjectPool").GetComponent<ObjectPoolScript>();
    }

    protected override void Update()
    {
        base.Update();

        if (!Player)
        {
            return;
        }
        
        target = GetClosestTarget();
        
        if (shouldTargetPlayer)
        {
            target = Player;
        }

        playerDistance = Vector3.Distance(transform.position, Player.transform.position);
    }

    public override void Damage(float damage)
    {
        if (immortal)
        {
            return;
        }
        
        health -= damage;
        HealthBar.GetComponent<HealthBarScript>().ChangeHealth(health);

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
        GameObject newExp = _expPool.GetObject();
        newExp.transform.position = transform.position;
        var newExpScript = newExp.GetComponent<ExpScript>();
        
        switch (type)
        {
            case Type.Sushi:
                newExpScript.SetValue(3);
                break;
            case Type.Chocolate:
                newExpScript.SetValue(5);
                break;
            case Type.Cream:
                newExpScript.SetValue(5);
                break;
            case Type.GrapeJuice:
                newExpScript.SetValue(7);
                break;
        }
    }

    protected void Die()
    {
        SpawnExp();
        GameController.GetComponent<EnemySpawningScript>().ReturnEnemy(gameObject);
        
        OnDeath();
    }

    protected virtual void OnDeath()
    {
        
    }

    private GameObject GetClosestTarget()
    {
        List<GameObject> targetables = new List<GameObject>();
        
        ObjectWithStatsScript[] damageableObjects = FindObjectsOfType<ObjectWithStatsScript>();

        foreach (var damageableObject in damageableObjects)
        {
            if (damageableObject.CompareTag("Shield") || 
                damageableObject.CompareTag("Enemy"))
            {
                continue;
            }
            
            targetables.Add(damageableObject.gameObject);
        }
        
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;
        
        for (int i = 0; i < targetables.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, targetables[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = targetables[i];
            }
        }

        return closestObject;
    }
}
