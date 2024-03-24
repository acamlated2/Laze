using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutOfBullets : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (ownerTransform.GetComponent<GrapeJuiceScript>().outOfBullets)
        {
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
