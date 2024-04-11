using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateControllerScript : MonoBehaviour
{
    public enum GameState
    {
        Menu, 
        Game, 
        Upgrade
    }

    public static GameStateControllerScript Current;

    private GameState _gameState = GameState.Menu;

    private UpgradeScreenManagerScript _upgradeManager;

    private void Awake()
    {
        Current = this;
        
        _upgradeManager = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2)
                                    .GetComponent<UpgradeScreenManagerScript>();
    }

    public void ChangeState(GameState state)
    {
        _gameState = state;

        GameEventControllerScript.current.StateChange();
    }

    public void ChangeState(GameState state, GameObject target)
    {
        _gameState = state;
        _upgradeManager.ShowStats();

        GameEventControllerScript.current.StateChange(target);
    }

    public GameState GetState()
    {
        return _gameState;
    }

    private void OnEnable()
    {
        GameEventControllerScript.current.OnGameStart += StartGame;
    }

    private void OnDisable()
    {
        GameEventControllerScript.current.OnGameStart -= StartGame;
    }

    private void StartGame()
    {
        ChangeState(GameState.Game);
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ChangeState(GameState.Menu);
        }

        if (Input.GetKeyDown("2"))
        {
            ChangeState(GameState.Game);
        }

        if (Input.GetKeyDown("3"))
        {
            GameObject baseObject = GameObject.FindGameObjectWithTag("Base");
            
            ChangeState(GameState.Upgrade, baseObject);
        }
    }
}
