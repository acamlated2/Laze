using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshairPivotScript : MonoBehaviour
{
    private GameObject _player;
    private GameObject _gameController;
    
    private void Awake()
    {
        _player = transform.parent.gameObject;
        _gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void Update()
    {
        float angle = _gameController.GetComponent<WeaponManagerScript>().angle;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = rotation;
    }
}
