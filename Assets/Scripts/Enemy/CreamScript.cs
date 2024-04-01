using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreamScript : EnemyScript
{
    [SerializeField] private GameObject projectile;

    private bool _relocating;
    private Vector3 _relocatingPosition;

    public float hidingRange = 20;
    public GameObject closestChocolate;
    
    protected override void Awake()
    {
        base.Awake();
        type = Type.Cream;

        ShouldAttack = true;

        ProjectilePool = GameObject.FindGameObjectWithTag("CreamProjectileObjectPool").GetComponent<ObjectPoolScript>();
    }

    protected override void Update()
    {
        base.Update();

        closestChocolate = GetClosestChocolate();
    }

    protected override void Attack()
    {
        GameObject newProjectile = ProjectilePool.GetObject();
        newProjectile.transform.position = transform.position;
        newProjectile.GetComponent<CreamProjectileScript>().SetRotation(target.transform.position);
    }

    private GameObject GetClosestChocolate()
    {
        ChocolateScript[] chocolates = FindObjectsOfType<ChocolateScript>();
        
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;
    
        for (int i = 0; i < chocolates.Length; i++)
        {
            if (!chocolates[i].GetComponent<ChocolateScript>().haveShield)
            {
                continue;
            }
            
            float distance = Vector2.Distance(transform.position, chocolates[i].transform.position);
    
            if (distance > hidingRange)
            {
                continue;
            }
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = chocolates[i].gameObject;
            }
        }
    
        return closestObject;
    }
}
