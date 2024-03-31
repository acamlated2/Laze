using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StartEverythingNode : DecoratorNode
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
        if (Agent.isStopped)
        {
            Agent.isStopped = false;
        }
        
        // attacks
        ownerTransform.GetComponent<ObjectWithStatsScript>().paused = false;
        
        // update child
        child.UpdateNode(ownerTransform);
        return State.Success;
    }
}
