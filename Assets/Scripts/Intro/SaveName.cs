using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SaveName : MonoBehaviour
{
    public TMP_InputField inputField;

    public TextMeshProUGUI titleText;

    public TextMeshProUGUI jokeText;
    public TextMeshProUGUI jokeTextMain;

    public GameObject skipButton;

    public List<String> localizationJokeKeys;

    private void Awake()
    {
        // PlayerPrefs.DeleteKey("myNameKey");
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("myNameKey") && !string.IsNullOrEmpty(PlayerPrefs.GetString("myNameKey")))
        {
            titleText.text = "Hi, " + PlayerPrefs.GetString("myNameKey").ToUpper() + "!";
            inputField.gameObject.SetActive(false);
            skipButton.SetActive(true);
            // jokeTextMain.gameObject.SetActive(true);
            jokeText.gameObject.SetActive(true);
            jokeText.text =
                LocalizationManager.Localize(localizationJokeKeys[Random.Range(0, localizationJokeKeys.Count)]);
        }
        else
        {
            titleText.text = "Please fill out your name!";
            skipButton.SetActive(false);
            jokeText.gameObject.SetActive(false);
            jokeTextMain.gameObject.SetActive(false);
            inputField.gameObject.SetActive(true);
        }
    }

    public void SetMyName(TMP_InputField inputField)
    {
        if (!PlayerPrefs.HasKey("myNameKey") && inputField.text != String.Empty || inputField.text != "")
        {
            PlayerPrefs.SetString("myNameKey", inputField.text);
            PlayerPrefs.Save();
        }
    }
}
