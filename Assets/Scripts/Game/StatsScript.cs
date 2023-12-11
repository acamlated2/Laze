using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsScript : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text expText;

    public void ChangeHp(float hp)
    {
        hpText.text = hp.ToString();
    }
    
    public void ChangeExp(float exp)
    {
        expText.text = exp.ToString();
    }
}
