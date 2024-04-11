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
    public GameObject healthBar;
    
    public GameObject target;

    public float defense = 1;

    public float attackSpeed = 1;

    public float damageMultiplier = 1;
    
    protected virtual void Awake()
    {
        AttackTimer = attackTime;
        
        GameController = GameObject.FindGameObjectWithTag("GameController");
        
        HealthBarPool = GameObject.FindGameObjectWithTag("HealthBarObjectPool").GetComponent<ObjectPoolScript>();

        maxHealth = health;
    }

    protected virtual void OnEnable()
    {
        health = maxHealth;
        
        healthBar = HealthBarPool.GetObject();
        healthBar.GetComponent<HealthBarScript>().owner = transform.gameObject;
        healthBar.GetComponent<HealthBarScript>().ChangeMaxHealth(health);
        healthBar.GetComponent<HealthBarScript>().ChangeHealth(health);
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
        
        AttackTimer -= 1 * attackSpeed * Time.deltaTime;

        if (AttackTimer <= 0)
        {
            AttackTimer = attackTime;
                
            Attack();
        }
    }

    protected virtual void Attack()
    {
        
    }

    public void Damage(float damage)
    {
        if (immortal)
        {
            return;
        }

        float calculatedDamage = damage - defense;
        if (calculatedDamage <= 0)
        {
            return;
        }
        
        health -= calculatedDamage;
        
        healthBar.GetComponent<HealthBarScript>().ChangeHealth(health);

        if (health <= 0)
        {
            OnZeroHealth();
        }
        
        OnDamaged();
    }

    protected virtual void OnZeroHealth()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnDamaged()
    {
        
    }
}
