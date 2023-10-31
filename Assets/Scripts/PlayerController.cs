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
    
    // private NavMeshAgent _agent;
    // public Camera cam;
    
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
        //Debug.Log("aim dir: " + dir.ReadValue<Vector2>());
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
        // _agent = GetComponent<NavMeshAgent>();
        // _agent.updateRotation = false;
        // _agent.updateUpAxis = false;
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("moving player to: " + cam.ScreenToWorldPoint(Input.mousePosition));
        //     _agent.SetDestination(cam.ScreenToWorldPoint(Input.mousePosition));
        // }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        transform.Translate(_moveDir * speed * Time.deltaTime);
    }
}
