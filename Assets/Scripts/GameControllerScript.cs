using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControllerScript : MonoBehaviour
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

    private GameObject _base;
    private GameObject[] _towers;

    private List<GameObject> _targetables = new List<GameObject>();
    
    private void Awake()
    {
        _playerInput = new PlayerInputAction();
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _base = GameObject.FindGameObjectWithTag("Base");
        _towers = GameObject.FindGameObjectsWithTag("Tower");

        _targetables.Add(_player);
        _targetables.Add(_base);
        for (int i = 0; i < _towers.Length; i++)
        {
            _targetables.Add(_towers[i]);
        }
    }

    private void OnEnable()
    {
        var playerScript = _player.GetComponent<PlayerController>();
        
        // gameplay
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
        
        // upgrading & menu
        _moveUpList = _playerInput.UpgradingMenu.MoveUpList;
        _moveUpList.Enable();

        _moveDownList = _playerInput.UpgradingMenu.MoveDownList;
        _moveDownList.Enable();

        _select = _playerInput.UpgradingMenu.Select;
        _select.Enable();

        _deselect = _playerInput.UpgradingMenu.Deselect;
        _deselect.Enable();
    }

    private void OnDisable()
    {
        // gameplay
        _move.Disable();
        _aim.Disable();
        _interact.Disable();
        _skill.Disable();
        
        // upgrading & menu
        _moveUpList.Disable();
        _moveDownList.Disable();
        _select.Disable();
        _deselect.Disable();
    }

    public GameObject GetClosestTarget(Transform enemyTransform)
    {
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;
        
        for (int i = 0; i < _targetables.Count; i++)
        {
            float distance = Vector2.Distance(enemyTransform.position, _targetables[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = _targetables[i];
            }
        }

        return closestObject;
    }
}
