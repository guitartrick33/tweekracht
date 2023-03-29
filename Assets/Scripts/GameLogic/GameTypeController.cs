using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;

public class GameTypeController : MonoBehaviour
{

    [SerializeField] private GameObject energyText;
    [SerializeField] private GameObject todayText;
    [SerializeField] private GameObject friendsText;
    private String currentFriendsText;
    public GameType gameType;

    private void Start()
    {
        DisableTexts();
    }

    public void DisableTexts()
    {
        energyText.SetActive(false);
        todayText.SetActive(false);
        friendsText.SetActive(false);
    }

    public void EnableText()
    {
        DisableTexts();
        switch (StartViewController.Instance.GameType())
        {
            case (GameType.ENERGY):
                energyText.SetActive(true);
                break;
            case(GameType.TODAY):
                todayText.SetActive(true);
                break;
            case(GameType.FRIENDS):
                currentFriendsText = $" {StartViewController.Instance.GetFriendsName().ToUpper()}?";
                friendsText.GetComponent<TextMeshProUGUI>().text = $"{LocalizationManager.Localize("Start.DTitle")}  {currentFriendsText}";
                friendsText.SetActive(true);
                break;
        }
        Debug.Log(StartViewController.Instance.GetFriendsName());
    }
}

public enum GameType
{
    ENERGY,
    TODAY,
    FRIENDS
}
