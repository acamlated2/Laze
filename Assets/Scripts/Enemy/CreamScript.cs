using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamScript : EnemyScript
{
    [SerializeField] private GameObject projectile;

    private bool _relocating;
    private Vector3 _relocatingPosition;
    
    protected override void Awake()
    {
        base.Awake();
        type = Type.Cream;

        shouldAttack = true;
    }

    protected override void FixedUpdate()
    {
        if (playerDistance <= panicDistance)
        {
            HandleRunning();
        }
        else
        {
            HandleNormal();
        }
    }

    protected override void Attack()
    {
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<CreamProjectileScript>().SetRotation(target.transform.position);
    }

    private void HandleRunning()
    {
        if (_relocating)
        {
            agent.SetDestination(_relocatingPosition);

            if (Vector3.Distance(transform.position, _relocatingPosition) <= 10)
            {
                _relocating = false;
            }
            
            return;
        }
        
        Vector3 runAwayPosition = player.transform.position - (player.transform.position - 
                                                               transform.position).normalized * (panicDistance * 2);

        Vector3 raycastDirection = -transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, raycastDirection, out hit))
        {
            float distance = Vector3.Distance(transform.position, hit.point);

            if (distance <= 10)
            {
                runAwayPosition = transform.position + (player.transform.position - transform.position).normalized *
                    (panicDistance + 5);

                _relocating = true;
                _relocatingPosition = runAwayPosition;
            }
        }
            
        agent.SetDestination(runAwayPosition);
    }

    private void HandleNormal()
    {
        Vector3 direction = transform.position - target.transform.position;
        direction.Normalize();

        Vector3 desiredPos = target.transform.position + direction * (panicDistance + 5);
        
        agent.SetDestination(desiredPos);
    }
}
