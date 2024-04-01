using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class WeaponManagerScript : MonoBehaviour
{
    private Vector2 _playerAim;
    public float angle;

    private List<GameObject> _weaponManagers = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _weaponManagers.Add(transform.GetChild(0).GetChild(i).gameObject);
        }

        _weaponManagers[0].GetComponent<BaseWeaponManagerScript>().unlocked = true;
    }

    public void Aim(Vector2 aim)
    {
        angle = Mathf.Atan2(aim.y, aim.x);
        angle *= Mathf.Rad2Deg;
    }

    public void SetWeaponUnlocked(int index, bool unlocked)
    {
        _weaponManagers[index].GetComponent<BaseWeaponManagerScript>().unlocked = unlocked;
    }
}
