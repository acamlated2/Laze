using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float _offset = -10;

    private GameObject _gameController;

    private GameObject _locktarget;

    private GameObject _menuLockTarget;
    private GameObject _player;
    
    private void Awake()
    {
        GameEventControllerScript.current.OnStateChange += ChangeTarget;
        
        _gameController = GameObject.FindGameObjectWithTag("GameController");
        _player = GameObject.FindGameObjectWithTag("Player");
        _menuLockTarget = GameObject.FindGameObjectWithTag("MenuLockTarget");

        _locktarget = _player;
    }

    private void OnDestroy()
    {
        GameEventControllerScript.current.OnStateChange -= ChangeTarget;
    }

    private void ChangeTarget()
    {
        switch (_gameController.GetComponent<GameStateControllerScript>().GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                _locktarget = _menuLockTarget;
                break;
            
            case GameStateControllerScript.GameState.Game:
                _locktarget = _player;
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                break;
        }
    }

    private void Update()
    {
        LockOnTarget();
    }

    private void LockOnTarget()
    {
        if (_player == null)
        {
            return;
        }
        
        Vector3 desiredPos = _locktarget.transform.position;
        desiredPos.z += _offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, 0.05f);
    }
}
