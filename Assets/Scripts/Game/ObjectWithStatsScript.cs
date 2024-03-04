using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithStatsScript : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField] [Min(0.1f)] public float health = 30;

    [SerializeField] protected bool handleAttack = true;
    protected bool shouldAttack;
    protected float attackTimer;
    [SerializeField] [Min(0.1f)] protected float attackTime = 1;

    protected GameObject gameController;

    [SerializeField] private GameObject healthBarPrefab;
    protected GameObject healthBar;
    private bool _maxHealthSet;

    protected virtual void Awake()
    {
        attackTimer = attackTime;
        
        gameController = GameObject.FindGameObjectWithTag("GameController");
        
        // create health bar
        healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBar.GetComponent<HealthBarScript>().owner = transform.gameObject;
    }

    protected virtual void Start()
    {
        healthBar.GetComponent<HealthBarScript>().ChangeMaxHealth(health);
    }

    protected void OnDestroy()
    {
        Destroy(healthBar);
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
        if (shouldAttack)
        {
            attackTimer -= 1 * Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackTimer = attackTime;
                
                Attack();
            }
        }
    }

    protected virtual void Attack()
    {
        
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        
        healthBar.GetComponent<HealthBarScript>().ChangeHealth(health);

        if (health <= 0)
        {
            Destroy(transform.gameObject);
            
            gameController.GetComponent<GameControllerScript>().RemoveTarget(gameObject);
        }
    }
}
