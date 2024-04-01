using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.Serialization;

public class ObjectWithStatsScript : MonoBehaviour
{
    [Header("General Stats")]
    [Min(0.1f)] public float health = 30;
    public float maxHealth;
    public bool immortal;

    public bool paused;

    [SerializeField] protected bool handleAttack = true;
    protected bool ShouldAttack;
    protected float AttackTimer;
    [SerializeField] [Min(0.1f)] protected float attackTime = 1;

    protected GameObject GameController;

    protected ObjectPoolScript HealthBarPool;
    protected GameObject HealthBar;
    
    public GameObject target;

    protected virtual void Awake()
    {
        AttackTimer = attackTime;
        
        GameController = GameObject.FindGameObjectWithTag("GameController");
        
        HealthBarPool = GameObject.FindGameObjectWithTag("HealthBarObjectPool").GetComponent<ObjectPoolScript>();

        maxHealth = health;
    }

    private void OnEnable()
    {
        health = maxHealth;
        
        HealthBar = HealthBarPool.GetObject();
        HealthBar.GetComponent<HealthBarScript>().owner = transform.gameObject;
        HealthBar.GetComponent<HealthBarScript>().ChangeMaxHealth(health);
        HealthBar.GetComponent<HealthBarScript>().ChangeHealth(health);
    }

    protected virtual void Update()
    {
        if (handleAttack)
        {
            TimeAttacks();
        }
    }

    private void TimeAttacks()
    {
        if (paused)
        {
            return;
        }

        if (!target)
        {
            return;
        }

        if (!ShouldAttack)
        {
            return;
        }
        
        AttackTimer -= 1 * Time.deltaTime;

        if (AttackTimer <= 0)
        {
            AttackTimer = attackTime;
                
            Attack();
        }
    }

    protected virtual void Attack()
    {
        
    }

    public virtual void Damage(float damage)
    {
        if (immortal)
        {
            return;
        }
        
        health -= damage;
        
        HealthBar.GetComponent<HealthBarScript>().ChangeHealth(health);

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
