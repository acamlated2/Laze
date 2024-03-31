using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopEverythingNode : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        // movement
        Agent = ownerTransform.GetComponent<NavMeshAgent>();
        Agent.isStopped = true;

        // attack
        ownerTransform.GetComponent<ObjectWithStatsScript>().paused = true;
        
        return State.Success;
    }
}
