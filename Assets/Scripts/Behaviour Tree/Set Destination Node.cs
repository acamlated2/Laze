using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetDestinationNode : ActionNode
{
    public GameObject target;
    public float distance;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        target = ownerTransform.GetComponent<EnemyScript>().target;
        Agent = ownerTransform.GetComponent<NavMeshAgent>();

        Vector3 targetPos = target.transform.position;
        Vector3 desiredPosition = targetPos - (targetPos - ownerTransform.position).normalized * distance;
        
        Agent.SetDestination(desiredPosition);
        return State.Success;
    }
}
