using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithStatsScript : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField] [Min(0.1f)] public float health = 30;

    public bool paused;

    [SerializeField] protected bool handleAttack = true;
    protected bool ShouldAttack;
    protected float AttackTimer;
    [SerializeField] [Min(0.1f)] protected float attackTime = 1;

    protected GameObject GameController;

    [SerializeField] private GameObject healthBarPrefab;
    protected GameObject HealthBar;
    private bool _maxHealthSet;

    protected virtual void Awake()
    {
        AttackTimer = attackTime;
        
        GameController = GameObject.FindGameObjectWithTag("GameController");
        
        // create health bar
        HealthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        HealthBar.GetComponent<HealthBarScript>().owner = transform.gameObject;
    }

    protected virtual void Start()
    {
        HealthBar.GetComponent<HealthBarScript>().ChangeMaxHealth(health);
    }

    protected void OnDestroy()
    {
        Destroy(HealthBar);
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
        if (!paused)
        {
            if (ShouldAttack)
            {
                AttackTimer -= 1 * Time.deltaTime;

                if (AttackTimer <= 0)
                {
                    AttackTimer = attackTime;
                
                    Attack();
                }
            }
        }
    }

    protected virtual void Attack()
    {
        
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        
        HealthBar.GetComponent<HealthBarScript>().ChangeHealth(health);

        if (health <= 0)
        {
            Destroy(transform.gameObject);
            
            GameController.GetComponent<GameControllerScript>().RemoveTarget(gameObject);
        }
    }
}
