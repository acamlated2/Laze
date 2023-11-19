using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeScript : MonoBehaviour
{
    private TMP_Text _minuteText;
    private TMP_Text _secondText;
    private TMP_Text _dividerText;

    private float _timerMax = 1;
    private float _timer = 1;
    
    public int second;
    public int minute;

    private bool _dividerActive;

    private void Awake()
    {
        _minuteText = FindObjectsOfType<TMP_Text>().FirstOrDefault(t => t.tag == "TimeMinutes");
        _secondText = FindObjectsOfType<TMP_Text>().FirstOrDefault(t => t.tag == "TimeSeconds");
        _dividerText = FindObjectsOfType<TMP_Text>().FirstOrDefault(t => t.tag == "TimeDivider");
    }

    private void Update()
    {
        // time
        _timer -= 1 * Time.deltaTime;

        if (_timer <= 0)
        {
            AddSecond();
            _timer = _timerMax;
            
            ChangeDividerState();
        }
    }

    private void AddSecond()
    {
        second += 1;

        if (second >= 60)
        {
            AddMinute();
            second = 0;
        }

        _secondText.text = second.ToString();

        // add zero in front
        if (_secondText.text.Length < 2)
        {
            string temp = "0";
            _secondText.text = temp + _secondText.text;
        }
    }

    private void AddMinute()
    {
        minute += 1;

        _minuteText.text = minute.ToString();
        
        // add zero in front
        if (_minuteText.text.Length < 2)
        {
            string temp = "0";
            _minuteText.text = temp + _minuteText.text;
        }
    }

    private void ChangeDividerState()
    {
        _dividerActive = !_dividerActive;
        
        _dividerText.gameObject.SetActive(_dividerActive);
    }
}
