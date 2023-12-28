using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private TMP_Text text;
    
    private float _healthMax = 100;

    private GameObject _canvas;

    public GameObject owner;

    private Camera _camera;

    private void Awake()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        GameObject parentInCanvas = _canvas.transform.GetChild(1).transform.gameObject;
        
        transform.SetParent(parentInCanvas.transform);

        transform.localScale = new Vector3(1, 1, 1);

        _camera = Camera.main;
    }

    public void ChangeHealth(float newHealth)
    {
        if (newHealth < 0 || newHealth > _healthMax)
        {
            return;
        }
        
        bar.fillAmount = newHealth / _healthMax;
        text.text = newHealth + " / " + _healthMax;
    }

    private void Update()
    {
        Vector3 position = owner.transform.position + new Vector3(0, 2, 0);
        
        transform.position = _camera.WorldToScreenPoint(position);
    }

    public void ChangeMaxHealth(float newMaxHealth)
    {
        _healthMax = newMaxHealth;

        ChangeHealth(owner.GetComponent<ObjectWithStatsScript>().health);
    }
}
