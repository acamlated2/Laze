using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventControllerScript : MonoBehaviour
{
    public static GameEventControllerScript current;

    private Camera _camera;

    private void Awake()
    {
        current = this;
        
        _camera = Camera.main;
    }

    public event Action OnStateChange;
    public void StateChange()
    {
        if (OnStateChange != null)
        {
            OnStateChange();
        }
    }

    public void StateChange(GameObject target)
    {
        if (OnStateChange != null)
        {
            OnStateChange();
        }
        
        _camera.GetComponent<CameraScript>().ChangeUpgradeTarget(target);
    }

    public event Action OnGameStart;
    public void StartGame()
    {
        if (OnGameStart != null)
        {
            OnGameStart();
        }
    }
}
