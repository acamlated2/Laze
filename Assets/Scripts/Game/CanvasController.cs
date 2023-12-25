using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private GameObject _gameController;

    private GameObject _activateInMenu;
    private GameObject _activateInGame;
    private GameObject _activateInUpgrade;
    
    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");
        _activateInMenu = transform.GetChild(0).transform.gameObject;
        _activateInGame = transform.GetChild(1).transform.gameObject;
        _activateInUpgrade = transform.GetChild(2).transform.gameObject;
        
        EnableMenuObjects();
    }

    private void OnEnable()
    {
        GameEventControllerScript.current.OnStateChange += ChangeActive;
    }

    private void OnDisable()
    {
        GameEventControllerScript.current.OnStateChange -= ChangeActive;
    }

    private void ChangeActive()
    {
        switch (_gameController.GetComponent<GameStateControllerScript>().GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                EnableMenuObjects();
                break;
            
            case GameStateControllerScript.GameState.Game:
                EnableGameObjects();
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                EnableUpgradeObjects();
                break;
        }
    }

    private void EnableMenuObjects()
    {
        ActivateObjects(_activateInMenu);
        DeactivateObjects(_activateInGame);
        DeactivateObjects(_activateInUpgrade);
    }

    private void EnableGameObjects()
    {
        DeactivateObjects(_activateInMenu);
        ActivateObjects(_activateInGame);
        DeactivateObjects(_activateInUpgrade);
    }

    private void EnableUpgradeObjects()
    {
        DeactivateObjects(_activateInMenu);
        DeactivateObjects(_activateInGame);
        ActivateObjects(_activateInUpgrade);
    }

    private void ActivateObjects(GameObject objectToActivate)
    {
        objectToActivate.SetActive(true);
    }

    private void DeactivateObjects(GameObject objectToDeactivate)
    {
        objectToDeactivate.SetActive(false);
    }
}
