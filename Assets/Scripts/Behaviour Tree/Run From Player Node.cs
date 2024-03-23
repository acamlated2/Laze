using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunFromPlayerNode : ActionNode
{
    private bool _relocating;
    private Vector3 _relocatingPosition;

    private float panicDistance;
    
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (panicDistance == 0)
        {
            panicDistance = ownerTransform.GetComponent<EnemyScript>().panicDistance;
        }
        if (!Agent)
        {
            Agent = ownerTransform.GetComponent<NavMeshAgent>();
        }
        
        if (_relocating)
        {
            Agent.SetDestination(_relocatingPosition);

            if (Vector3.Distance(ownerTransform.position, _relocatingPosition) <= 10)
            {
                _relocating = false;
            }
            
            return State.Success;
        }

        Vector3 runAwayPosition = Player.transform.position - (Player.transform.position -
                                                               ownerTransform.position).normalized *
            (panicDistance * 2);

        Vector3 raycastDirection = -ownerTransform.forward;
        RaycastHit hit;

        if (Physics.Raycast(ownerTransform.position, raycastDirection, out hit))
        {
            float distance = Vector3.Distance(ownerTransform.position, hit.point);

            if (distance <= 10)
            {
                runAwayPosition = ownerTransform.position +
                                  (Player.transform.position - ownerTransform.position).normalized *
                                  (panicDistance + 5);

                _relocating = true;
                _relocatingPosition = runAwayPosition;
            }
        }
            
        Agent.SetDestination(runAwayPosition);
        
        return State.Success;
    }
}
