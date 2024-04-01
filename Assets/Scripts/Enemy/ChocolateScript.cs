using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateScript : EnemyScript
{
    public bool haveShield = true;
    
    protected override void Awake()
    {
        base.Awake();
        type = Type.Chocolate;

        health = 50;
    }
}
