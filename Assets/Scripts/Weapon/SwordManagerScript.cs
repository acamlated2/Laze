using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManagerScript : BaseWeaponManagerScript
{
    protected override void Awake()
    {
        base.Awake();

        type = Type.Sword;

        Pool = GameObject.FindGameObjectWithTag("SwordManager").GetComponent<ObjectPoolScript>();
    }
    
    protected override void Attack()
    {
        if (player == null)
        {
            return;
        }
        
        float angle = gameController.GetComponent<WeaponManagerScript>().angle;

        GameObject newSword = Pool.GetObject();
        newSword.transform.position = player.transform.position;
        newSword.GetComponent<WeaponScript>().InitiateAngle(angle);
    }
}
