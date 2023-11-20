using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private Vector2 _moveDir;
    private Vector2 _aimDir;

    private float _joystickDeadzone = 0.2f;

    private Animator _animator;
    
    public void Move(InputAction.CallbackContext dir)
    {
        //Debug.Log("move dir: " + dir.ReadValue<Vector2>());
        if (CheckDeadZone(dir.ReadValue<Vector2>()))
        {
            _moveDir = dir.ReadValue<Vector2>();
        }
        else
        {
            _moveDir = Vector2.zero;
        }
    }
    public void Aim(InputAction.CallbackContext dir)
    {
        if (CheckDeadZone(dir.ReadValue<Vector2>()))
        {
            _aimDir = dir.ReadValue<Vector2>();
        }
        else
        {
            _aimDir = Vector2.zero;
        }
    }
    public void Interact(InputAction.CallbackContext ctx)
    {
        Debug.Log("interacting");
    }
    public void Skill(InputAction.CallbackContext ctx)
    {
        Debug.Log("using skill");
    }
    
    private bool CheckDeadZone(Vector2 value)
    {
        if ((value.x > _joystickDeadzone) ||
            (value.x < -_joystickDeadzone) ||
            (value.y > _joystickDeadzone) ||
            (value.y < -_joystickDeadzone))
        {
            return true;
        }

        return false;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAnimation();
        
        GetComponent<WeaponManagerScript>().Aim(_aimDir);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        transform.Translate(_moveDir * speed * Time.deltaTime);
    }

    private void HandleAnimation()
    {
        if (_moveDir.x > _joystickDeadzone)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (_moveDir.x < -_joystickDeadzone)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
        if (_moveDir != Vector2.zero)
        {
            _animator.SetFloat("Speed", 1);   
        }
        else
        {
            _animator.SetFloat("Speed", 0);   
        }
    }
}
