using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithStatsScript : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField] [Min(0.1f)] public float health = 20;

    [SerializeField] protected bool handleAttack = true;
    protected bool shouldAttack;
    protected float attackTimer;
    [SerializeField] [Min(0.1f)] protected float attackTime = 1;

    protected GameObject gameController;

    protected virtual void Awake()
    {
        attackTimer = attackTime;
        
        gameController = GameObject.FindGameObjectWithTag("GameController");
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

        if (health <= 0)
        {
            Destroy(transform.gameObject);
            
            gameController.GetComponent<GameControllerScript>().RemoveTarget(gameObject);
        }
    }
}
