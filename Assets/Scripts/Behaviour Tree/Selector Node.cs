using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public Node check;
    public Node success;
    public Node failure;


    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (!check || !success || !failure)
        {
            Debug.Log("Node not set");
            return State.Failure;
        }

        State returnState;
        
        if (check.UpdateNode(ownerTransform) == State.Success)
        {
            returnState = success.UpdateNode(ownerTransform);
        }
        else
        {
            returnState = failure.UpdateNode(ownerTransform);
        }

        return returnState;
    }
}
