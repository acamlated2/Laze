using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponManagerScript : MonoBehaviour
{
    public enum Type
    {
        ThrowingKnife,
        IceStaff,
        WindStaff,
        Boulder,
        Sword
    };
    
    [Header("Base Stats")]
    protected Type type;
    public bool unlocked;
    
    [SerializeField] [Min(0.1f)] protected float attackDelay = 1;
    protected float attackTimer;
    
    [SerializeField] protected bool canBurst;
    protected bool bursting;
    
    [SerializeField] [Min(1)] protected int burstAmount = 5;
    protected int burstCount;

    [SerializeField] [Min(0.1f)] protected float burstDelay = 0.1f;
    protected float burstTimer;

    protected GameObject gameController;
    protected GameObject player;

    protected virtual void Awake()
    {
        attackTimer = attackDelay;
        burstCount = burstAmount;
        burstTimer = burstDelay;

        gameController = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (unlocked)
        {
            TimeAttack();
        }
    }

    private void TimeAttack()
    {
        if (bursting)
        {
            HandleBursting();
            return;
        }
        
        attackTimer -= 1 * Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = attackDelay;

            if (canBurst)
            {
                bursting = true;
                return;
            }
            Attack();
        }
    }

    protected virtual void Attack()
    {
        
    }

    private void HandleBursting()
    {
        burstTimer -= 1 * Time.deltaTime;
        if (burstTimer <= 0)
        {
            burstTimer = burstDelay;

            burstCount -= 1;
            Attack();
            
            if (burstCount <= 0)
            {
                burstCount = burstAmount;
                bursting = false;
            }
        }
    }
}
