using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeJuiceScript : EnemyScript
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int bulletCount = 3;
    public bool outOfBullets;

    protected override void Awake()
    {
        base.Awake();
        type = Type.GrapeJuice;

        ShouldAttack = true;
        target = Player;
    }

    protected override void Attack()
    {
        if (outOfBullets)
        {
            return;
        }
        
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
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

    protected override void Die()
    {
        SpawnExp();
        Destroy(gameObject);

        SpawnRing();
    }

    private void SpawnRing()
    {
        Vector3 pos = transform.position;
        
        List<Vector3> rotations = new List<Vector3>()
        {
            new Vector3(0, 10),
            new Vector3(10, 10),
            new Vector3(10, 0),
            new Vector3(10, -10),
            new Vector3(0, -10),
            new Vector3(-10, -10),
            new Vector3(-10, 0),
            new Vector3(-10, 10)
        };
        
        for (int i = 0; i < rotations.Count; i++)
        {
            GameObject newProjectile = Instantiate(projectile, pos, Quaternion.identity);
            Vector3 targetPos = new Vector3();
            targetPos.x = pos.x + rotations[i].x;
            targetPos.y = pos.y + rotations[i].y;
            newProjectile.GetComponent<GrapeProjectileScript>().SetRotation(targetPos);
        }
    }
}
