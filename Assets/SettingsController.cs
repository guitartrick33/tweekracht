using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public GameObject settingsPanel;
    public bool isOpen;

    private void Awake()
    {
        isOpen = false;
        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        isOpen = true;
        Time.timeScale = 0;
        settingsPanel.SetActive(true);
        if (GetComponent<CardController>().vp != null)
        {
            GetComponent<CardController>().vp.Pause();
        }
    }

    public void CloseSettings()
    {
        isOpen = false;
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
        if (GetComponent<CardController>().vp is not null)
        {
            GetComponent<CardController>().vp.Play();
        }
    }

    public void SwitchSToggle(SwitchToggleSound s)
    {
        s.SoundFXSwitcher(AudioManager.Instance.isSoundOn);
    }
    
    public void SwitchMToggle(SwitchToggleMusic s)
    {
        s.MusicSwitcher(AudioManager.Instance.isMusicOn);
    }

    public void GoBackToMainMenu()
    {
        CloseSettings();
        GetComponent<CardController>().ResetSidesCompletely();
        ViewManager.Instance.SwitchView(ViewType.MainMenu);
    }

    public void VisitTweekracht()
    {
        Application.OpenURL("https://www.tweekracht.nl/");
    }
}
