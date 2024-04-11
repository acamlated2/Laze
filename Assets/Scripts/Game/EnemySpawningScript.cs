using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawningScript : MonoBehaviour
{
    private List<GameObject> _enemyPools = new List<GameObject>();
    
    [SerializeField] private float spawnMinDistance = 20;
    [SerializeField] private float spawnMaxDistance = 30;
    
    [SerializeField] private float timerMax = 1;
    private float _timer = 1;

    private GameObject _player;
    private GameObject _base;
    private GameObject _towersObject;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _base = GameObject.FindGameObjectWithTag("Base");
        _towersObject = GameObject.FindGameObjectWithTag("Towers");
        GameObject[] enemyPools = GameObject.FindGameObjectsWithTag("EnemyObjectPool");
        foreach (var pool in enemyPools)
        {
            _enemyPools.Add(pool);
        }
    }

    private void Update()
    {
        if (GameStateControllerScript.Current.GetState() != GameStateControllerScript.GameState.Game)
        {
            return;
        }
        
        _timer -= 1 * Time.deltaTime;

        if (_timer <= 0)
        {
            _timer = timerMax;
            SpawnEnemyAtRandom();
        }
    }

    private void SpawnEnemyAtRandom()
    {
        if (!_player)
        {
            return;
        }

        if (!_base)
        {
            return;
        }
        
        Vector3 spawnPosition;
        bool canSpawn = true;
        
        List<GameObject> towers = new List<GameObject>();
        for (int i = 0; i < _towersObject.transform.childCount; i++)
        {
            towers.Add(_towersObject.transform.GetChild(i).transform.gameObject);
        }

        do
        {
            spawnPosition = new Vector3(Random.Range(-spawnMaxDistance, spawnMaxDistance),
                Random.Range(-spawnMaxDistance, spawnMaxDistance), 0);

            canSpawn = Vector3.Distance(spawnPosition, _player.transform.position) >= spawnMinDistance;
            canSpawn = Vector3.Distance(spawnPosition, _base.transform.position) >= spawnMinDistance;

            foreach (var tower in towers)
            {
                canSpawn = Vector3.Distance(spawnPosition, tower.transform.position) >= spawnMinDistance;
            }
        } while (!canSpawn);

        int randInt = Random.Range(0, _enemyPools.Count);

        GameObject newEnemy = _enemyPools[randInt].GetComponent<ObjectPoolScript>().GetObject();
        newEnemy.GetComponent<NavMeshAgent>().Warp(spawnPosition);
    }
    
    public void ReturnEnemy(GameObject enemyToReturn)
    {
        foreach (var enemyPool in _enemyPools)
        {
            enemyPool.GetComponent<ObjectPoolScript>().ReturnObject(enemyToReturn);
        }
    }
}
