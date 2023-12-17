using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateShieldScript : ObjectWithStatsScript
{
    private GameObject _pivot;
    
    protected override void Awake()
    {
        base.Awake();
        handleAttack = false;

        _pivot = transform.parent.gameObject;
    }

    public override void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(_pivot);
        }
    }
}
