using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeIndicatorScript : MonoBehaviour
{
    private GameObject image;
    private Vector3 _defaultPos;

    [SerializeField] private float amplitude = 15;
    [SerializeField] private float frequency = 3;

    private void Awake()
    {
        image = transform.GetChild(0).gameObject;

        _defaultPos = image.transform.position;
    }

    private void Update()
    {
        float y = _defaultPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        image.transform.position = new Vector3(_defaultPos.x, y, _defaultPos.z);
    }

    public void ChangePosition(Vector3 newPosition)
    {
        _defaultPos.x = newPosition.x;
    }
}
