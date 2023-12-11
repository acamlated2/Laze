using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManagerScript : BaseWeaponManagerScript
{
    [SerializeField] private GameObject prefab;

    protected override void Awake()
    {
        base.Awake();
        type = Type.ThrowingKnife;
        canBurst = true;
    }

    protected override void Attack()
    {
        float angle = gameController.GetComponent<WeaponManagerScript>().angle;
        
        GameObject newKnife = Instantiate(prefab, player.transform.position, Quaternion.identity);
        newKnife.GetComponent<WeaponScript>().Initiate(angle);
    }
}
