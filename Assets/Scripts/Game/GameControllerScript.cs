using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControllerScript : MonoBehaviour
{
    private GameObject _player;
    
    private GameObject _base;
    private GameObject[] _towers;

    public List<GameObject> targetables = new List<GameObject>();
    
    // player
    private float _playerExp = 100;
    
    // canvas
    private GameObject _canvas;
    
    // exp
    public List<GameObject> exps = new List<GameObject>();
    [SerializeField] [Min(0.1f)] private float expToPlayerAttractDistance = 5;
    [SerializeField] [Min(0.1f)] private float expToExpAttractDistance = 3;
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _base = GameObject.FindGameObjectWithTag("Base");
        _towers = GameObject.FindGameObjectsWithTag("Tower");

        targetables.Add(_player);
        targetables.Add(_base);
        for (int i = 0; i < _towers.Length; i++)
        {
            targetables.Add(_towers[i]);
        }
        
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    private void Update()
    {
        AttractExp();
    }

    public GameObject GetClosestTarget(Transform enemyTransform)
    {
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;
        
        for (int i = 0; i < targetables.Count; i++)
        {
            float distance = Vector2.Distance(enemyTransform.position, targetables[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = targetables[i];
            }
        }

        return closestObject;
    }

    public void RemoveTarget(GameObject target)
    {
        for (int i = 0; i < targetables.Count; i++)
        {
            if (targetables[i] == target)
            {
                targetables.RemoveAt(i);
            }
        }
    }

    public void AddExp(float exp)
    {
        _playerExp += exp;
        
        _canvas.GetComponent<StatsScript>().ChangeExp(_playerExp);
    }

    private void AttractExp()
    {
        for (int i = 0; i < exps.Count; i++)
        {
            float distanceToPlayer = Vector2.Distance(exps[i].transform.position, _player.transform.position);

            if (distanceToPlayer <= expToPlayerAttractDistance)
            {
                exps[i].GetComponent<ExpScript>().AttractToPoint(_player.transform.position);
                continue;
            }

            for (int j = 0; j < exps.Count; j++)
            {
                if (exps[j] == exps[i])
                {
                    continue;
                }
                
                float distanceToExp = Vector2.Distance(exps[i].transform.position, exps[j].transform.position);

                if (distanceToExp <= expToExpAttractDistance)
                {
                    exps[i].GetComponent<ExpScript>().AttractToPoint(exps[j].transform.position);
                }
            }
        }
    }

    public void AddToList(List<GameObject> list, GameObject gameobject)
    {
        list.Add(gameobject);
    }

    public void RemoveFromList(List<GameObject> list, GameObject gameobject)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == gameobject)
            {
                list.RemoveAt(i);
            }
        }
    }
}
