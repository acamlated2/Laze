using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBarScript : MonoBehaviour
{
    private Image _image;
    private TMP_Text _text;

    private float _displayedValue;
    private float _actualValue;
    [SerializeField] private float maxValue = 100;

    public bool customText;

    private void Awake()
    {
        _image = transform.GetChild(0).GetComponent<Image>();
        _text = transform.GetChild(1).GetComponent<TMP_Text>();

        _displayedValue = _image.fillAmount;
    }

    private void Start()
    {
        _displayedValue = _actualValue;
        _image.fillAmount = _displayedValue / maxValue;
        _text.text = _displayedValue.ToString();
    }

    private void Update()
    {
        if (_displayedValue != _actualValue)
        {
            _displayedValue = Mathf.Lerp(_displayedValue, _actualValue, 0.01f);
            _image.fillAmount = _displayedValue / maxValue;

            if (!customText)
            {
                _text.text = ((int)_displayedValue).ToString();
            }
        }
    }

    public void ChangeValue(float newValue)
    {
        _actualValue = newValue;
    }

    public void ChangeMaxValue(float newMaxValue)
    {
        maxValue = newMaxValue;
    }

    public void ChangeText(string newText)
    {
        _text.text = newText;
    }
}
