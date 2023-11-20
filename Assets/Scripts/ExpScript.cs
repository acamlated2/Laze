using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpScript : MonoBehaviour
{
    private float _value = 5;
    private float _defaultValue = 5;

    private GameObject _gameController;

    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    public void SetValue(float newValue)
    {
        _value = newValue;

        float scale = _value / _defaultValue * 5;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            
            _gameController.GetComponent<GameControllerScript>().AddExp(_value);
        }
    }
}
