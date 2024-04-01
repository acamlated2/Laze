using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ExpScript : MonoBehaviour
{
    public float value = 5;
    private float _defaultValue = 5;

    private GameControllerScript _gameController;

    [SerializeField] [Min(0.1f)] private float attractSpeed = 5;
    [SerializeField] [Min(0.1f)] private float maxAttractSpeed = 20;

    private ObjectPoolScript pool;

    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();

        pool = GameObject.FindGameObjectWithTag("ExpObjectPool").GetComponent<ObjectPoolScript>();
    }

    public void SetValue(float newValue)
    {
        value = newValue;

        float scale = value / _defaultValue * 5;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameController.AddExp(value);
            pool.ReturnObject(gameObject);
        }

        if (other.gameObject.CompareTag("Exp"))
        {
            int thisIndex = transform.GetSiblingIndex();
            int otherIndex = other.transform.GetSiblingIndex();
            
            if (thisIndex > otherIndex)
            {
                float thisValue = value;
                float otherValue = other.GetComponent<ExpScript>().value;
                
                pool.ReturnObject(gameObject);
                pool.ReturnObject(other.gameObject);

                GameObject newExp = pool.GetObject();
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
