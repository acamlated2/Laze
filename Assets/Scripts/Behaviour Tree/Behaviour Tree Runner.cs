using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        tree = tree.Clone();
    }

    private void Update()
    {
        if (!_player)
        {
            return;
        }
        
        tree.UpdateTree(transform);
    }
}
