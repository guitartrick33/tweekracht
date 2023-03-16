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
    private MainMenuViewController mainMenuController;
    private GameObject currentPanel;

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

        mainMenuController = FindObjectOfType<MainMenuViewController>();
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
        currentPanel = panel;
    }

    public void ClosePopUpPanel(GameObject panel)
    {
        panel.SetActive(false);
        buttons.SetActive(true);
        currentPanel = null;
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
        if (this.inputText.text != "" && this.inputText.text != String.Empty)
        {
            gameType = global::GameType.FRIENDS;
            friendsName = inputText.text;
            mainMenuController.StartGame();
        }
    }

    public String GetFriendsName()
    {
        return friendsName;
    }

    private void OnDisable()
    {
        CloseFriendNamePanel();
        if (currentPanel is not null)
        {
            ClosePopUpPanel(currentPanel);
        }
    }
}
