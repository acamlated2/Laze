using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManagerScript : BaseWeaponManagerScript
{
    protected override void Awake()
    {
        base.Awake();
        type = Type.ThrowingKnife;
        canBurst = true;

        Pool = GameObject.FindGameObjectWithTag("KnifeManager").GetComponent<ObjectPoolScript>();
    }

    protected override void Attack()
    {
        if (player == null)
        {
            return;
        }
        
        float angle = gameController.GetComponent<WeaponManagerScript>().angle;

        GameObject newKnife = Pool.GetObject();
        newKnife.transform.position = player.transform.position;
        newKnife.GetComponent<WeaponScript>().InitiateAngle(angle);
    }
}
