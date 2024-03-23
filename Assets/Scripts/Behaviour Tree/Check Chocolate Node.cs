using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckChocolateNode : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (ownerTransform.GetComponent<CreamScript>().closestChocolate)
        {
            Debug.Log("chocolate found");
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
