using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeJuiceScript : EnemyScript
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int bulletCount = 3;
    private int bulletCountDefault;
    public bool outOfBullets;

    protected override void Awake()
    {
        base.Awake();
        type = Type.GrapeJuice;

        ShouldAttack = true;
        target = Player;

        ProjectilePool = GameObject.FindGameObjectWithTag("GrapeJuiceProjectileObjectPool")
            .GetComponent<ObjectPoolScript>();

        bulletCountDefault = bulletCount;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        bulletCount = bulletCountDefault;
        outOfBullets = false;
    }

    protected override void Attack()
    {
        if (outOfBullets)
        {
            return;
        }

        GameObject newProjectile = ProjectilePool.GetObject();
        newProjectile.transform.position = transform.position;
        newProjectile.GetComponent<GrapeProjectileScript>().SetRotation(target.transform.position);

        bulletCount -= 1;
        if (bulletCount <= 0)
        {
            outOfBullets = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Base") ||
            other.gameObject.CompareTag("Tower"))
        {
            if (outOfBullets)
            {
                Die();
            }
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        SpawnRing();
    }

    private void SpawnRing()
    {
        Vector3 pos = transform.position;
        
        List<Vector3> rotations = new List<Vector3>()
        {
            new(0, 10),
            new(10, 10),
            new(10, 0),
            new(10, -10),
            new(0, -10),
            new(-10, -10),
            new(-10, 0),
            new(-10, 10)
        };
        
        for (int i = 0; i < rotations.Count; i++)
        {
            GameObject newProjectile = ProjectilePool.GetObject();
            newProjectile.transform.position = transform.position;
            Vector3 targetPos = new Vector3();
            targetPos.x = pos.x + rotations[i].x;
            targetPos.y = pos.y + rotations[i].y;
            newProjectile.GetComponent<GrapeProjectileScript>().SetRotation(targetPos);
        }
    }
}
