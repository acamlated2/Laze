using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControllerScript : MonoBehaviour
{
    private PlayerInputAction _playerInput;
    // gameplay input action
    private InputAction _move;
    private InputAction _aim;
    private InputAction _interact;
    private InputAction _skill;
    
    // upgrading & menu input action
    private InputAction _moveUpList;
    private InputAction _moveDownList;
    private InputAction _select;
    private InputAction _deselect;
    
    // game objects
    private GameObject _player;
    private GameObject _canvas;

    private void Awake()
    {
        _playerInput = new PlayerInputAction();
        _player = GameObject.FindGameObjectWithTag("Player");
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        
        EnableMenuInput();
    }
    
    private void OnEnable()
    {
        GameEventControllerScript.current.OnStateChange += ChangeInput;
    }

    private void OnDisable()
    {
        GameEventControllerScript.current.OnStateChange -= ChangeInput;
    }

    private void ChangeInput()
    {
        GameStateControllerScript.GameState state = GetComponent<GameStateControllerScript>().GetState();

        if (state == GameStateControllerScript.GameState.Game)
        {
            EnableGameInput();
            DisableMenuInput();
        }
        else
        {
            EnableMenuInput();
            DisableGameInput();
        }
    }
    
    private void EnableGameInput()
    {
        var playerScript = _player.GetComponent<PlayerController>();

        _move = _playerInput.Gameplay.Move;
        _move.Enable();
        _move.performed += playerScript.Move;
        _move.canceled += playerScript.Move;

        _aim = _playerInput.Gameplay.Aim;
        _aim.Enable();
        _aim.performed += playerScript.Aim;
        _aim.canceled += playerScript.Aim;

        _interact = _playerInput.Gameplay.Interact;
        _interact.Enable();
        _interact.performed += playerScript.Interact;

        _skill = _playerInput.Gameplay.Skill;
        _skill.Enable();
        _skill.performed += playerScript.Skill;
    }

    private void EnableMenuInput()
    {
        var menuManagerScript = _canvas.GetComponent<MenuManagerScript>();
        
        _moveUpList = _playerInput.UpgradingMenu.MoveUpList;
        _moveUpList.Enable();
        _moveUpList.performed += menuManagerScript.MoveUpList;

        _moveDownList = _playerInput.UpgradingMenu.MoveDownList;
        _moveDownList.Enable();
        _moveDownList.performed += menuManagerScript.MoveDownList;

        _select = _playerInput.UpgradingMenu.Select;
        _select.Enable();
        _select.performed += menuManagerScript.Select;

        _deselect = _playerInput.UpgradingMenu.Deselect;
        _deselect.Enable();
    }

    private void DisableGameInput()
    {
        // gameplay
        _move.Disable();
        _aim.Disable();
        _interact.Disable();
        _skill.Disable();
    }

    private void DisableMenuInput()
    {
        // upgrading & menu
        _moveUpList.Disable();
        _moveDownList.Disable();
        _select.Disable();
        _deselect.Disable();
    }
}
