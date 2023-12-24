using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventControllerScript : MonoBehaviour
{
    public static GameEventControllerScript current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnStateChange;
    public void SceneChange()
    {
        if (OnStateChange != null)
        {
            OnStateChange();
        }
    }
}
