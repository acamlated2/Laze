using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsScript : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text expText;

    public void ChangeHP(float hp)
    {
        hpText.text = hp.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            ChangeHP(10);
        }
    }
}
