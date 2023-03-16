using System;
using System.Collections;
using System.Collections.Generic;
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
                currentFriendsText = friendsText.GetComponent<TextMeshProUGUI>().text;
                currentFriendsText += $" {StartViewController.Instance.GetFriendsName().ToUpper()}?";
                friendsText.GetComponent<TextMeshProUGUI>().text = currentFriendsText;
                friendsText.SetActive(true);
                break;
        }
    }
}

public enum GameType
{
    ENERGY,
    TODAY,
    FRIENDS
}
