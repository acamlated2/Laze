using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManagerScript : BaseWeaponManagerScript
{
    [SerializeField] private GameObject prefab;
    
    protected override void Awake()
    {
        base.Awake();

        type = Type.Sword;
    }
    
    protected override void Attack()
    {
        if (player == null)
        {
            return;
        }
        
        float angle = gameController.GetComponent<WeaponManagerScript>().angle;
        
        GameObject newSword = Instantiate(prefab, player.transform.position, Quaternion.identity);
        newSword.GetComponent<WeaponScript>().InitiateAngle(angle);
    }
}
