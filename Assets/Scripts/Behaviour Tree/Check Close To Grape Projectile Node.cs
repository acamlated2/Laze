using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckCloseToGrapeProjectileNode : ActionNode
{
    [SerializeField] private float detectDistance = 5;
    
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        GrapeProjectileScript[] projectiles = FindObjectsOfType<GrapeProjectileScript>();

        for (int i = 0; i < projectiles.Length; i++)
        {
            float distance = Vector3.Distance(ownerTransform.position, projectiles[i].transform.position);
            if (distance <= detectDistance)
            {
                return State.Success;
            }
        }

        return State.Failure;
    }
}
