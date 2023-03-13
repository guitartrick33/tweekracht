using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartViewController : MonoBehaviour
{
    private static StartViewController instance;
    private GameType gameType;
    private string friendsName;
    [SerializeField] private GameObject friendsNamePanel;
    [SerializeField] private GameObject buttons;
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private List<GameObject> popups;

    public GameType GameType()
    {
        return gameType;
    }
    
    public static StartViewController Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        friendsNamePanel.SetActive(false);
        buttons.SetActive(true);

        foreach (GameObject g in popups)
        {
            g.SetActive(false);
        }
    }
    
    public void SetGameTypeEnergy()
    {
        gameType = global::GameType.ENERGY;
    }

    public void SetGameTypeToday()
    {
        gameType = global::GameType.TODAY;
    }

    public void OpenPopUpPanel(GameObject panel)
    {
        panel.SetActive(true);
        buttons.SetActive(false);
    }

    public void ClosePopUpPanel(GameObject panel)
    {
        panel.SetActive(false);
        buttons.SetActive(true);
    }

    public void OpenFriendNamePanel()
    {
        friendsNamePanel.SetActive(true);
        buttons.SetActive(false);
    }

    public void CloseFriendNamePanel()
    {
        friendsNamePanel.SetActive(false);
        buttons.SetActive(true);
        inputText.text = String.Empty;
    }

    public void SetGameTypeFriends(TextMeshProUGUI inputText)
    {
        gameType = global::GameType.FRIENDS;
        friendsName = inputText.text;
    }

    public String GetFriendsName()
    {
        return friendsName;
    }

    private void OnDisable()
    {
        CloseFriendNamePanel();
    }
}
