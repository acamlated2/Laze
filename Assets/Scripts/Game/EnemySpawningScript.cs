using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawningScript : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
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
    }

    private void Update()
    {
        _timer -= 1 * Time.deltaTime;

        if (_timer <= 0)
        {
            _timer = timerMax;
            SpawnEnemyAtRandom();
        }
    }

    private void SpawnEnemyAtRandom()
    {
        if (_player == null)
        {
            return;
        }
        
        Vector2 spawnPosition;
        bool canSpawn = true;
        
        List<GameObject> towers = new List<GameObject>();
        for (int i = 0; i < _towersObject.transform.childCount; i++)
        {
            towers.Add(_towersObject.transform.GetChild(i).transform.gameObject);
        }

        do
        {
            spawnPosition = new Vector2(Random.Range(-spawnMaxDistance, spawnMaxDistance),
                Random.Range(-spawnMaxDistance, spawnMaxDistance));

            canSpawn = Vector2.Distance(spawnPosition, _player.transform.position) >= spawnMinDistance;
            canSpawn = Vector2.Distance(spawnPosition, _base.transform.position) >= spawnMinDistance;

            foreach (var tower in towers)
            {
                canSpawn = Vector2.Distance(spawnPosition, tower.transform.position) >= spawnMinDistance;
            }
        } while (!canSpawn);

        GameObject newEnemy;
        newEnemy = Instantiate(GetRandomPrefab(), spawnPosition, Quaternion.identity);
    }

    private GameObject GetRandomPrefab()
    {
        int randomNum = Random.Range(0, enemyPrefabs.Length);
        GameObject randomPrefab = enemyPrefabs[randomNum];
        
        return randomPrefab;
    }
}
