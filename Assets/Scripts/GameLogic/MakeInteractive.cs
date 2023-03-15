using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInteractive : MonoBehaviour
{
    private CardController _cardController;
    private void Awake()
    {
        _cardController = FindObjectOfType<CardController>();
    }

    public void SwitchOn()
    {
        _cardController.interactive = true;
    }
}
