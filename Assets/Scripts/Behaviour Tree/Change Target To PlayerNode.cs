using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTargetToPlayerNode : CompositeNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        ownerTransform.GetComponent<EnemyScript>().shouldTargetPlayer = true;
        
        foreach (var child in children)
        {
            child.UpdateNode(ownerTransform);
        }

        return State.Success;
    }
}
