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
    
    private List<GameObject> _options = new List<GameObject>();

    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");

        GameObject optionsParent = transform.GetChild(0).transform.GetChild(0).transform.gameObject;
        for (int i = 0; i < optionsParent.transform.childCount; i++)
        {
            _options.Add(optionsParent.transform.GetChild(i).gameObject);
        }
    }

    public void MoveUpList(InputAction.CallbackContext ctx)
    {
        switch (_gameController.GetComponent<GameStateControllerScript>().GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                MoveUpMenuList();
                break;
            
            case GameStateControllerScript.GameState.Game:
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                MoveUpUpgradeList();
                break;
        }
        
        UpdateTextString();
    }

    public void MoveDownList(InputAction.CallbackContext ctx)
    {
        switch (_gameController.GetComponent<GameStateControllerScript>().GetState())
        {
            case GameStateControllerScript.GameState.Menu:
                MoveDownMenuList();
                break;
            
            case GameStateControllerScript.GameState.Game:
                break;
            
            case GameStateControllerScript.GameState.Upgrade:
                MoveDownUpgradeList();
                break;
        }
        
        UpdateTextString();
    }

    private void MoveUpMenuList()
    {
        _currentMenuOption -= 1;

        if (_currentMenuOption < 0)
        {
            _currentMenuOption = menuOptionAmount - 1;
        }
    }

    private void MoveUpUpgradeList()
    {
        _currentUpgradeOption -= 1;

        if (_currentUpgradeOption < 0)
        {
            _currentUpgradeOption = upgradeOptionAmount - 1;
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

    private void MoveDownUpgradeList()
    {
        _currentMenuOption += 1;

        if (_currentMenuOption > upgradeOptionAmount - 1)
        {
            _currentMenuOption = 0;
        }
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
                HandleUpgradeTexts();
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

    private void HandleUpgradeTexts()
    {
        switch (_currentUpgradeOption)
        {
            case 0:
                break;
            
            case 1:
                break;
            
            case 2:
                break;
            
            case 3:
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
        switch (_currentUpgradeOption)
        {
            case 0:
                break;
            
            case 1:
                break;
            
            case 2:
                break;
            
            case 3:
                break;
        }
    }
}
