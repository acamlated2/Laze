using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerTooCloseNode : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (ownerTransform.GetComponent<EnemyScript>().playerDistance <=
            ownerTransform.GetComponent<EnemyScript>().panicDistance)
        {
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
