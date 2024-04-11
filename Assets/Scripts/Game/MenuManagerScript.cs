using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManagerScript : MonoBehaviour
{
    private GameObject _gameController;

    [SerializeField] [Min(1)] private int menuOptionAmount = 3;
    private int _currentMenuOption;

    [SerializeField] [Min(1)] private int upgradeOptionAmount = 4;
    private int _currentUpgradeOption;
    private UpgradeScreenManagerScript _upgradeManager;
    
    private List<GameObject> _options = new List<GameObject>();

    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");

        GameObject optionsParent = transform.GetChild(0).transform.GetChild(0).transform.gameObject;
        for (int i = 0; i < optionsParent.transform.childCount; i++)
        {
            _options.Add(optionsParent.transform.GetChild(i).gameObject);
        }

        _upgradeManager = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2)
                                    .GetComponent<UpgradeScreenManagerScript>();
    }

    public void MoveUpList(InputAction.CallbackContext ctx)
    {
        switch (GameStateControllerScript.Current.GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                MoveUpMenuList();
                break;
            
            case GameStateControllerScript.GameState.Game:
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                break;
        }
        
        UpdateTextString();
    }

    public void MoveDownList(InputAction.CallbackContext ctx)
    {
        switch (GameStateControllerScript.Current.GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                MoveDownMenuList();
                break;
            
            case GameStateControllerScript.GameState.Game:
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                break;
        }
        
        UpdateTextString();
    }
    
    public void MoveLeftList(InputAction.CallbackContext ctx)
    {
        switch (GameStateControllerScript.Current.GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                break;
            
            case GameStateControllerScript.GameState.Game:
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                MoveLeftUpgradeList();
                break;
        }
    }
    
    public void MoveRightList(InputAction.CallbackContext ctx)
    {
        switch (GameStateControllerScript.Current.GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                break;
            
            case GameStateControllerScript.GameState.Game:
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                MoveRightUpgradeList();
                break;
        }
    }

    private void MoveUpMenuList()
    {
        _currentMenuOption -= 1;

        if (_currentMenuOption < 0)
        {
            _currentMenuOption = menuOptionAmount - 1;
        }
    }

    private void MoveDownMenuList()
    {
        _currentMenuOption += 1;

        if (_currentMenuOption > menuOptionAmount - 1)
        {
            _currentMenuOption = 0;
        }
    }

    private void MoveRightUpgradeList()
    {
        _currentUpgradeOption += 1;

        if (_currentUpgradeOption > upgradeOptionAmount - 1)
        {
            _currentUpgradeOption = 0;
        }
        
        _upgradeManager.ChangeSelection(_currentUpgradeOption);
    }
    
    private void MoveLeftUpgradeList()
    {
        _currentUpgradeOption -= 1;

        if (_currentUpgradeOption < 0)
        {
            _currentUpgradeOption = upgradeOptionAmount - 1;
        }
        
        _upgradeManager.ChangeSelection(_currentUpgradeOption);
    }

    private void UpdateTextString()
    {
        switch (_gameController.GetComponent<GameStateControllerScript>().GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                HandleMenuTexts();
                break;
            
            case GameStateControllerScript.GameState.Game:
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                break;
        }
    }

    private void HandleMenuTexts()
    {
        switch (_currentMenuOption)
        {
            case 0:
                _options[0].GetComponent<TMP_Text>().text = ">Start<";
                _options[1].GetComponent<TMP_Text>().text = "Settings";
                _options[2].GetComponent<TMP_Text>().text = "Exit";
                break;
            
            case 1:
                _options[0].GetComponent<TMP_Text>().text = "Start";
                _options[1].GetComponent<TMP_Text>().text = ">Settings<";
                _options[2].GetComponent<TMP_Text>().text = "Exit";
                break;
            
            case 2:
                _options[0].GetComponent<TMP_Text>().text = "Start";
                _options[1].GetComponent<TMP_Text>().text = "Settings";
                _options[2].GetComponent<TMP_Text>().text = ">Exit<";
                break;
        }
    }

    public void Select(InputAction.CallbackContext ctx)
    {
        switch (_gameController.GetComponent<GameStateControllerScript>().GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                MenuSelect();
                break;
            
            case GameStateControllerScript.GameState.Game:
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                UpgradeSelect();
                break;
        }
    }

    private void MenuSelect()
    {
        switch (_currentMenuOption)
        {
            case 0:
                GameEventControllerScript.current.StartGame();
                break;
            
            case 1:
                break;
            
            case 2:
                Application.Quit();
                break;
        }
    }

    private void UpgradeSelect()
    {
        _upgradeManager.UpgradeObject(_currentUpgradeOption);
        _currentUpgradeOption = 0;
        _upgradeManager.ChangeSelection(_currentUpgradeOption);
    }
}
