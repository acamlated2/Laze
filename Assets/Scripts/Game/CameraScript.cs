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
    private GameObject _upgradeLockTarget;

    private float _desiredSize;
    
    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");
        _player = GameObject.FindGameObjectWithTag("Player");
        _menuLockTarget = GameObject.FindGameObjectWithTag("MenuLockTarget");
        _upgradeLockTarget = GameObject.FindGameObjectWithTag("UpgradeLockTarget");

        _locktarget = _menuLockTarget;

        _desiredSize = GetComponent<Camera>().orthographicSize;
    }

    private void OnEnable()
    {
        GameEventControllerScript.current.OnStateChange += ChangeTarget;
    }

    private void OnDisable()
    {
        GameEventControllerScript.current.OnStateChange -= ChangeTarget;
    }

    private void ChangeTarget()
    {
        switch (_gameController.GetComponent<GameStateControllerScript>().GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                ChangeSize(7);
                _locktarget = _menuLockTarget;
                break;
            
            case GameStateControllerScript.GameState.Game:
                ChangeSize(15);
                _locktarget = _player;
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                ChangeSize(7);
                _locktarget = _upgradeLockTarget;
                break;
        }
    }

    public void ChangeUpgradeTarget(GameObject target)
    {
        if (_gameController.GetComponent<GameStateControllerScript>().GetState() != GameStateControllerScript.GameState.Upgrade)
        {
            return;
        }
        
        _locktarget.transform.position = target.transform.position + new Vector3(6, -2, 0);
    }

    private void Update()
    {
        LockOnTarget();
        ControlSize();
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

    private void ControlSize()
    {
        GetComponent<Camera>().orthographicSize =
            Mathf.Lerp(GetComponent<Camera>().orthographicSize, _desiredSize, 0.05f);
    }

    private void ChangeSize(float size)
    {
        _desiredSize = size;
    }
}
