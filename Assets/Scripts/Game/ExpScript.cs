using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ExpScript : MonoBehaviour
{
    public float value = 5;
    private float _defaultValue = 5;

    private GameObject _gameController;

    [SerializeField] [Min(0.1f)] private float attractSpeed = 5;
    [SerializeField] [Min(0.1f)] private float maxAttractSpeed = 20;

    private GameObject _expPrefab;

    public int index;

    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");

        var gameControllerScript = _gameController.GetComponent<GameControllerScript>();
        gameControllerScript.AddToList(gameControllerScript.exps, gameObject);

        _expPrefab = _gameController.GetComponent<GameControllerScript>().expPrefab;
    }

    public void SetValue(float newValue)
    {
        value = newValue;

        float scale = value / _defaultValue * 5;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var gameControllerScript = _gameController.GetComponent<GameControllerScript>();
        
        if (other.gameObject.CompareTag("Player"))
        {
            gameControllerScript.RemoveFromList(gameControllerScript.exps, gameObject);
            Destroy(gameObject);
            
            gameControllerScript.AddExp(value);
        }

        if (other.gameObject.CompareTag("Exp"))
        {
            int thisIndex = gameControllerScript.GetIndex(gameControllerScript.exps, gameObject);
            int otherIndex = gameControllerScript.GetIndex(gameControllerScript.exps, other.gameObject);

            if ((thisIndex == -1) || (otherIndex == -1))
            {
                return;
            }
            
            if (thisIndex > otherIndex)
            {
                float thisValue = value;
                float otherValue = other.GetComponent<ExpScript>().value;
                
                gameControllerScript.RemoveFromList(gameControllerScript.exps, other.gameObject);
                Destroy(other.gameObject);
                
                gameControllerScript.RemoveFromList(gameControllerScript.exps, gameObject);
                Destroy(gameObject);
                
                GameObject newExp = Instantiate(_expPrefab, transform.position, Quaternion.identity);
                newExp.GetComponent<ExpScript>().SetValue(thisValue + otherValue);
            }
        }
    }

    public void AttractToPoint(Vector3 point)
    {
        Vector3 dir = point - transform.position;
        dir.Normalize();

        transform.position += dir * attractSpeed * Time.deltaTime;

        if (attractSpeed < maxAttractSpeed)
        {
            attractSpeed += 1 * Time.deltaTime;
        }
    }
}
