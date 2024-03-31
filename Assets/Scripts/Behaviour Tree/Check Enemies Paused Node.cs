using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemiesPausedNode : ActionNode
{
    private GameStateControllerScript _gameStateControllerScript;

    protected override void OnStart()
    {
        if (!_gameStateControllerScript)
        {
            _gameStateControllerScript = GameController.GetComponent<GameStateControllerScript>();
        }
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate(Transform ownerTransform)
    {
        if (_gameStateControllerScript.GetState() != GameStateControllerScript.GameState.Game)
        {
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
