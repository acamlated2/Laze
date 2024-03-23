using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToChocolateNode : ActionNode
{
    private float hidingRange;
    private float panicDistance;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (hidingRange == 0)
        {
            hidingRange = ownerTransform.GetComponent<CreamScript>().hidingRange;
        }
        if (panicDistance == 0)
        {
            panicDistance = ownerTransform.GetComponent<EnemyScript>().panicDistance;
        }

        Agent = ownerTransform.GetComponent<NavMeshAgent>();
        
        GameObject closestChocolate = ownerTransform.GetComponent<CreamScript>().closestChocolate;

        Vector3 desiredPosition = closestChocolate.transform.position - (Player.transform.position -
                                                                         ownerTransform.position).normalized *
            panicDistance;
        
        Agent.SetDestination(desiredPosition);
        
        return State.Success;
    }
}
