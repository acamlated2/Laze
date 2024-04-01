using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateScript : EnemyScript
{
    public bool haveShield = true;

    private GameObject _pivot;
    
    protected override void Awake()
    {
        base.Awake();
        type = Type.Chocolate;

        health = 50;

        _pivot = transform.GetChild(1).gameObject;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        _pivot.SetActive(true);
        haveShield = true;
    }
}
