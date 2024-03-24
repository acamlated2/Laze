using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunFromGrapeProjectileNode : ActionNode
{
    [SerializeField] private float runAwayDistance = 5;
    
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (!Agent)
        {
            Agent = ownerTransform.GetComponent<NavMeshAgent>();
        }
        
        GrapeProjectileScript[] projectiles = FindObjectsOfType<GrapeProjectileScript>();

        for (int i = 0; i < projectiles.Length; i++)
        {
            Vector3 dir = (ownerTransform.position - projectiles[i].transform.position);
            dir.Normalize();

            Vector3 runAwayPos = ownerTransform.position + (dir * runAwayDistance);

            Agent.SetDestination(runAwayPos);
        }

        return State.Success;
    }
}
