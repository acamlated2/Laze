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

    [SerializeField] private GameObject expPrefab;

    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");

        var gameControllerScript = _gameController.GetComponent<GameControllerScript>();
        gameControllerScript.AddToList(gameControllerScript.exps, gameObject);
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
            if ((other.transform.position.x > transform.position.x) ||
                (other.transform.position.y > transform.position.y))
            {
                GameObject newExp = Instantiate(expPrefab, transform.position, Quaternion.identity);
                newExp.GetComponent<ExpScript>().SetValue(other.GetComponent<ExpScript>().value + value);
                
                gameControllerScript.RemoveFromList(gameControllerScript.exps, other.gameObject);
                Destroy(other.gameObject);
                
                gameControllerScript.RemoveFromList(gameControllerScript.exps, gameObject);
                Destroy(gameObject);
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
