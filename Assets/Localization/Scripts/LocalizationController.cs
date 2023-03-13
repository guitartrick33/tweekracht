using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;

public class LocalizationController : MonoBehaviour
{
    private static LocalizationController instance;

    public static LocalizationController Instance
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
    }

    public void CheckSystemLocalization()
    {
        LocalizationManager.Read();

        switch (Application.systemLanguage)
        {
            case SystemLanguage.Dutch:
                LocalizationManager.Language = "Dutch";
                break;
            default:
                LocalizationManager.Language = "English";
                break;
        }
    }

    public void SetLocalization(string localization)
    {
        LocalizationManager.Language = localization;
    }
}
