using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : ObjectWithStatsScript
{
    [SerializeField] private float speed = 10;
    
    private Vector2 _moveDir;
    private Vector2 _aimDir;

    private float _joystickDeadzone = 0.2f;

    private Animator _animator;
    
    public GameObject _hpBar;

    public bool usingMouse = true;
    private Camera _camera;
    
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

    protected override void Awake()
    {
        base.Awake();
        
        health = 100;
        handleAttack = false;
        
        _animator = transform.GetChild(0).GetComponent<Animator>();

        _hpBar = GameObject.FindGameObjectWithTag("HpBarUI");
        _hpBar.GetComponent<UIBarScript>().ChangeValue(health);
        
        _camera = Camera.main;
    }

    protected override void Update()
    {
        HandleAnimation();

        if (usingMouse)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 dirToMouse = mousePos - _camera.WorldToScreenPoint(transform.position);
            dirToMouse.Normalize();

            _aimDir = dirToMouse;
        }
        
        gameController.GetComponent<WeaponManagerScript>().Aim(_aimDir);
        
        Vector3 searchPosition = transform.position;

        // Find all colliders within the search radius and with the target tag
        Collider[] colliders = Physics.OverlapSphere(searchPosition, 10, LayerMask.GetMask("Enemy")); // Adjust LayerMask if needed

        // Loop through the colliders and check their tags
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                // Do something with the found object
                Debug.Log("Found object " + collider.gameObject.name + " with tag " + "Enemy");
                // You can access the GameObject through collider.gameObject
            }
        }
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
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        }
        if (_moveDir.x < -_joystickDeadzone)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
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

    public override void Damage(float damage)
    {
        base.Damage(damage);
        _hpBar.GetComponent<UIBarScript>().ChangeValue(health);
    }
}
