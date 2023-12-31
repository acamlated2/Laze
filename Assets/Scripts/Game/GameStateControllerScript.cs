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

    private GameState _gameState = GameState.Menu;

    public void ChangeState(GameState state)
    {
        _gameState = state;

        GameEventControllerScript.current.StateChange();
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
            ChangeState(GameState.Upgrade);
        }
    }
}
