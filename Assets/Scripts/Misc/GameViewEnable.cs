using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewEnable : MonoBehaviour
{
    public GameObject gameView;

    private void OnEnable()
    {
        gameView.SetActive(true);
    }
}
