using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateShieldScript : ObjectWithStatsScript
{
    private GameObject _pivot;
    private GameObject _chocolate;
    
    protected override void Awake()
    {
        base.Awake();
        handleAttack = false;
        health = 30;

        _pivot = transform.parent.gameObject;
        _chocolate = _pivot.transform.parent.gameObject;
    }

    public override void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(_pivot);

            _chocolate.GetComponent<ChocolateScript>().haveShield = false;
        }
    }
}
