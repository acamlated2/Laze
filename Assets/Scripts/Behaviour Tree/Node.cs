using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        Running, 
        Failure, 
        Success
    }
    
    public enum AIBehaviour
    {
        GoToPlayer, 
        GoToLocation
    }

    [HideInInspector] public State state = State.Running;
    [HideInInspector] public bool started = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    
    
    // variables used in enemy AI
    protected GameObject Player;
    protected GameObject GameController;
    protected NavMeshAgent Agent;

    public State UpdateNode(Transform ownerTransform)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate(ownerTransform);

        if (state == State.Failure || state == State.Success)
        {
            OnStop();
            started = false;
        }

        return state;
    }

    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    protected abstract void OnStart();
    protected abstract void OnStop();

    protected virtual State OnUpdate(Transform ownerTransform)
    {
        return State.Success;
    }
}
