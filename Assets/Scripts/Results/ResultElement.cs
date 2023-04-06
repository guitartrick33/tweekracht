using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultElement : MonoBehaviour
{
    public FinalResultEnum finalResult;
    public CardTitle softSide;
    public CardTitle hardSide;
    public GameType gameType;
    public string friendsName;
    public string date;

    [SerializeField] private TextMeshProUGUI finalTitle;
    [SerializeField] private TextMeshProUGUI gameTypeText;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private Image icon;
    [SerializeField] private Image openCarouselIcon;

    [SerializeField] private List<TextMeshProUGUI> tguis;

    public Sprite energyIcon;
    public Sprite todayIcon;
    public Sprite friendsIcon;

    public Color energyColor;
    public Color todayColor;
    public Color friendsColor;

    public CardDesc softCardDesc;
    public CardDesc hardCardDesc;
    public string softCardTitle;
    public string softCardDescription;
    public string hardCardTitle;
    public string hardCardDescription;

    public void SetMainElementTexts()
    {
        switch (finalResult)
        {
            case FinalResultEnum.INTUITIVE:
                finalTitle.text = LocalizationManager.Localize("Results.IntuitiveTitle");
                break;
            case FinalResultEnum.CHANGER:
                finalTitle.text = LocalizationManager.Localize("Results.SocialChangerTitle");
                break;
            case FinalResultEnum.DOER:
                finalTitle.text = LocalizationManager.Localize("Results.SocialDoerTitle");
                break;
            case FinalResultEnum.PIONEER:
                finalTitle.text = LocalizationManager.Localize("Results.PioneerTitle");
                break;
            case FinalResultEnum.CREATOR:
                finalTitle.text = LocalizationManager.Localize("Results.CreatorTitle");
                break;
            case FinalResultEnum.BUILDER:
                finalTitle.text = LocalizationManager.Localize("Results.ConstructorTitle");
                break;
        }

        switch (gameType)
        {
            case GameType.ENERGY:
                gameTypeText.text = LocalizationManager.Localize("Start.Energy");
                icon.sprite = energyIcon;
                break;
            case GameType.TODAY:
                gameTypeText.text = LocalizationManager.Localize("Start.Today");
                icon.sprite = todayIcon;
                break;
            case GameType.FRIENDS:
                gameTypeText.text = LocalizationManager.Localize("Start.Duo");
                gameTypeText.text += $" - {friendsName}";
                icon.sprite = friendsIcon;
                break;
        }
        dateText.text = date;
        SetColor();
    }

    private void SetColor()
    {
        switch (gameType)
        {
            case GameType.ENERGY:
                foreach (TextMeshProUGUI t in tguis)
                {
                    t.color = energyColor;
                }
                icon.color = energyColor;
                openCarouselIcon.color = energyColor;
                break;
            case GameType.TODAY:
                foreach (TextMeshProUGUI t in tguis)
                {
                    t.color = todayColor;
                }
                icon.color = todayColor;
                openCarouselIcon.color = todayColor;
                break;
            case GameType.FRIENDS:
                foreach (TextMeshProUGUI t in tguis)
                {
                    t.color = friendsColor;
                }
                icon.color = friendsColor;
                openCarouselIcon.color = friendsColor;
                break;
        }
    }

    public void UpdatePage()
    {
        foreach (CardDesc cardDesc in SaveLoadManager.Instance.cardPrefabs)
        {
            if (cardDesc.cardTitle == softSide)
            {
                softCardDesc = cardDesc;
            }
            else if (cardDesc.cardTitle == hardSide)
            {
                hardCardDesc = cardDesc;
            }
        }

        softCardTitle = LocalizationManager.Localize(softCardDesc.localizedTitle.LocalizationKey);
        softCardDescription = LocalizationManager.Localize(softCardDesc.localizedDesc.LocalizationKey);
        hardCardTitle = LocalizationManager.Localize(hardCardDesc.localizedTitle.LocalizationKey);
        hardCardDescription = LocalizationManager.Localize(hardCardDesc.localizedDesc.LocalizationKey);
    }
}
