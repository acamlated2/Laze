using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class WeaponManagerScript : MonoBehaviour
{
    public enum Type
    {
        ThrowingKnife,
        IceStaff,
        WindStaff,
        Boulder,
        BaseballBat
    };

    [SerializeField] private Type[] _types = new Type[5];
    private bool[] _unlocked = new bool[5];

    [SerializeField] private float[] timerMax = new float[5];
    private float[] _timer = new float[5];

    [SerializeField] private GameObject[] prefabs;

    private bool[] _canBurst = new bool[5];
    private bool[] _bursting = new bool[5];
    [SerializeField] private float[] burstTimerMax = new float[5];
    private float[] _burstTimer = new float[5];
    [SerializeField] private int[] burstAmount = new int[5];
    private int[] _burstCount = new int[5];
    
    private Vector2 _playerAim;
    private float _angle;

    private void Awake()
    {
        _types[0] = Type.ThrowingKnife;
        _types[1] = Type.IceStaff;
        _types[2] = Type.WindStaff;
        _types[3] = Type.Boulder;
        _types[4] = Type.BaseballBat;
        
        _unlocked[0] = true;
        _unlocked[1] = false;
        _unlocked[2] = false;
        _unlocked[3] = false;
        _unlocked[4] = false;
        
        _canBurst[0] = true;
        _canBurst[1] = false;
        _canBurst[2] = false;
        _canBurst[3] = false;
        _canBurst[4] = false;
        
        _burstCount[0] = burstAmount[0];
        _burstCount[1] = burstAmount[1];
        _burstCount[2] = burstAmount[2];
        _burstCount[3] = burstAmount[3];
        _burstCount[4] = burstAmount[4];
    }

    private void Update()
    {
        for (int i = 0; i < _unlocked.Length; i++)
        {
            if (!_unlocked[i])
            {
                continue;
            }
            
            if (_bursting[i])
            {
                HandleBursting(i);
                continue;
            }
            
            _timer[i] -= 1 * Time.deltaTime;
            if (_timer[i] <= 0)
            {
                _timer[i] = timerMax[i];
            
                if (_canBurst[i])
                {
                    StartBurst(i);
                    continue;
                }
            
                Shoot(i);
            }
        }
    }

    private void Shoot(int typeIndex)
    {
        switch (_types[typeIndex])
        {
            case Type.ThrowingKnife:
                ThrowingKnife();
                break;
            
            case Type.IceStaff:
                IceStaff();
                break;
            
            case Type.WindStaff:
                WindStaff();
                break;
            
            case Type.Boulder:
                Boulder();
                break;
            
            case Type.BaseballBat:
                BaseballBat();
                break;
        }  
    }

    private void ThrowingKnife()
    {
        GameObject newKnife;
        newKnife = Instantiate(prefabs[0], transform.position, quaternion.identity);
        newKnife.GetComponent<WeaponScript>().Initiate(_angle);
    }

    private void IceStaff()
    {
        
    }

    private void WindStaff()
    {
        
    }

    private void Boulder()
    {
        
    }

    private void BaseballBat()
    {
        
    }

    public void Aim(Vector2 aim)
    {
        _angle = Mathf.Atan2(aim.y, aim.x);

        _angle *= Mathf.Rad2Deg;
    }

    private void StartBurst(int i)
    {
        _bursting[i] = true;
    }

    private void HandleBursting(int i)
    {
        _burstTimer[i] -= 1 * Time.deltaTime;

        if (_burstTimer[i] <= 0)
        {
            _burstTimer[i] = burstTimerMax[i];

            _burstCount[i] -= 1;
            
            Shoot(i);
            
            if (_burstCount[i] <= 0)
            {
                _burstCount[i] = burstAmount[i];

                _bursting[i] = false;
            }
        }
    }
}
