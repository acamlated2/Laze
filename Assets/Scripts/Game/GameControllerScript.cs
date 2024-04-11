using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControllerScript : MonoBehaviour
{
    private GameObject _player;

    // player
    private float _exp;
    private int _level = 1;
    [SerializeField] private float expToLevelUp = 100;

    // canvas
    private GameObject _expBar;

    // exp
    private ObjectPoolScript _expObjectPool;
    [SerializeField] [Min(0.1f)] private float expToPlayerAttractDistance = 5;
    [SerializeField] [Min(0.1f)] private float expToExpAttractDistance = 3;

    // game controller
    private GameStateControllerScript _gameStateControllerScript;
    private UpgradeScreenManagerScript _upgradeManager;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _expBar = GameObject.FindGameObjectWithTag("ExpBarUI");
        _expBar.GetComponent<UIBarScript>().customText = true;

        _gameStateControllerScript = GetComponent<GameStateControllerScript>();

        _expObjectPool = GameObject.FindGameObjectWithTag("ExpObjectPool").GetComponent<ObjectPoolScript>();
        
        _upgradeManager = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2)
                                    .GetComponent<UpgradeScreenManagerScript>();
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
    }

    private void StartGame()
    {
        GetComponent<EnemySpawningScript>().enabled = true;
    }

    private void Update()
    {
        AttractExp();

        if (Input.GetKeyDown("m"))
        {
            AddExp(50);
        }
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
            _expBar.GetComponent<UIBarScript>().ChangeMaxValue(expToLevelUp);

            _upgradeManager.selectedObject = _player;
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

        List<GameObject> exps = new List<GameObject>();
        for (int i = 0; i < _expObjectPool.transform.childCount; i++)
        {
            exps.Add(_expObjectPool.transform.GetChild(i).gameObject);
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
}