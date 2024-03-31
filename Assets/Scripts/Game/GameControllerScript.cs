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
    private float _exp;
    private int _level = 1;
    [SerializeField] private float expToLevelUp = 100;
    
    // canvas
    private GameObject _expBar;
    
    // exp
    public GameObject expPrefab;
    public List<GameObject> exps = new List<GameObject>();
    [SerializeField] [Min(0.1f)] private float expToPlayerAttractDistance = 5;
    [SerializeField] [Min(0.1f)] private float expToExpAttractDistance = 3;
    
    // enemies
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> chocolates = new List<GameObject>();
    
    // game controller
    private GameStateControllerScript _gameStateControllerScript;
    
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
        
        _expBar = GameObject.FindGameObjectWithTag("ExpBarUI");
        _expBar.GetComponent<UIBarScript>().customText = true;
        
        _gameStateControllerScript = GetComponent<GameStateControllerScript>();
    }

    private void Start()
    {
        _expBar.GetComponent<UIBarScript>().ChangeText(_level.ToString());
    }

    private void OnEnable()
    {
        GameEventControllerScript.current.OnGameStart += StartGame;
    }

    private void OnDisable()
    {
        GameEventControllerScript.current.OnGameStart -= StartGame;
        targetables.Clear();
        exps.Clear();
        enemies.Clear();
        chocolates.Clear();
    }

    private void StartGame()
    {
        GetComponent<EnemySpawningScript>().enabled = true;
    }

    private void OnApplicationQuit()
    {
        targetables.Clear();
        exps.Clear();
        enemies.Clear();
        chocolates.Clear();
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
        List<GameObject> tempList = new List<GameObject>();
        for (int i = 0; i < targetables.Count; i++)
        {
            if (targetables[i] == target)
            {
                continue;
            }
            
            tempList.Add(targetables[i].gameObject);
        }

        targetables = tempList;
    }

    public void AddExp(float addedExp)
    {
        _exp += addedExp;

        if (_exp >= expToLevelUp)
        {
            float extraExp = _exp - expToLevelUp;
            _exp = extraExp;

            _level += 1;

            expToLevelUp += expToLevelUp * 20 / 100;
            
            _expBar.GetComponent<UIBarScript>().ChangeText(_level.ToString());
            
            _gameStateControllerScript.ChangeState(GameStateControllerScript.GameState.Upgrade, _player);
        }
        
        _expBar.GetComponent<UIBarScript>().ChangeValue(_exp);
    }

    private void AttractExp()
    {
        if (_player == null)
        {
            return;
        }
        
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
        List<GameObject> tempList = new List<GameObject>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == gameobject)
            {
                continue;
            }
            
            tempList.Add(list[i].gameObject);
        }

        list.Clear();
        list = tempList;
    }

    public int GetIndex(List<GameObject> list, GameObject gameobject)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == gameobject)
            {
                return i;
            }
        }

        return -1;
    }
}
