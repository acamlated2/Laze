using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckShieldNode : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (ownerTransform.GetComponent<ChocolateScript>().haveShield)
        {
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
