using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class FinalPageController : MonoBehaviour
{
    [SerializeField] private VideoPlayer playerSoft;
    [SerializeField] private VideoPlayer playerHard;
    [SerializeField] private RenderTexture texture;

    [SerializeField] private GameObject clipSoft;
    [SerializeField] private GameObject clipHard;
    private VideoClip startClip;
    // [SerializeField] private Animator animController;
    
    [SerializeField] private ScrollRect mainScrollRect;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject circleImages;
    [SerializeField] private GameObject saveButton;

    private static FinalResultEnum finalResultEnum;
    private static string finalResultName;
    private static string finalResultDesc;
    private static string finalResultDescDetails;
    private static string balanceDesc;
    private static string matchDesc;
    public TextMeshProUGUI titleGUI;
    public TextMeshProUGUI descGUI;
    public TextMeshProUGUI detailedDescGUI;
    public TextMeshProUGUI balanceGUI;
    public TextMeshProUGUI matchGUI;
    public Sprite intuitiveCardSprite;
    public Sprite socialChangerCardSprite;
    public Sprite socialDoerCardSprite;
    public Sprite pioneerCardSprite;
    public Sprite creatorCardSprite;
    public Sprite builderCardSprite;

    public List<Image> cardBackgrounds;

    public CardDesc cardDescSoftFinal;
    public CardDesc cardDescHardFinal;
    [Space] 
    [Space]
    public TextMeshProUGUI softSideTitleUI;
    public TextMeshProUGUI softSideDescUI;
    public TextMeshProUGUI hardSideTitleUI;
    public TextMeshProUGUI hardSideDescUI;
    

    private void Start()
    {
        popUpPanel.SetActive(false);
        mainScrollRect.enabled = true;
        circleImages.SetActive(true);
        saveButton.SetActive(true);
        clipHard.SetActive(false);
        clipSoft.SetActive(false);
        finalResultName = String.Empty;
        finalResultDesc = String.Empty;
        finalResultDescDetails = String.Empty;
        balanceDesc = String.Empty;
        matchDesc = String.Empty;
    }

    public void OpenPopUp()
    {
        popUpPanel.SetActive(true);
        mainScrollRect.enabled = false;
        circleImages.SetActive(false);
        saveButton.SetActive(false);
    }

    public void ClosePopUp()
    {
        popUpPanel.SetActive(false);
        mainScrollRect.enabled = true;
        circleImages.SetActive(true);
        saveButton.SetActive(true);
    }


    public void UpdatePage()
    {
        texture.Release();
        softSideDescUI.text = cardDescSoftFinal.description.text;
        softSideTitleUI.text = cardDescSoftFinal.CardText();
        hardSideDescUI.text = cardDescHardFinal.description.text;
        hardSideTitleUI.text = cardDescHardFinal.CardText();
        if (methodMap.TryGetValue((cardDescHardFinal.cardTitle, cardDescSoftFinal.cardTitle), out Action method))
        {
            method();
            GetFinalResult();// Call the appropriate method
        }
    }

    public void SetClipSoft()
    {
        clipSoft.SetActive(true);
        mainScrollRect.enabled = false;
        playerSoft.clip = cardDescSoftFinal.currentClip;
        if (!AudioManager.Instance.isMusicOn)
        {
            playerSoft.SetDirectAudioMute(0, true);  
        }
        playerSoft.Play();
        playerSoft.loopPointReached += (vp) => ResetClipSoft() ;
    }

    private void ResetClipSoft()
    {
        mainScrollRect.enabled = true;
        playerSoft.Stop();
        texture.Release();
        clipSoft.SetActive(false);
    }

    public void SetClipHard()
    {
        clipHard.SetActive(true);
        mainScrollRect.enabled = false;
        playerHard.clip = cardDescHardFinal.currentClip;
        if (!AudioManager.Instance.isMusicOn)
        {
            playerHard.SetDirectAudioMute(0, true);  
        }
        playerHard.Play();
        playerHard.loopPointReached += (vp) => ResetClipHard() ;
    }

    private void ResetClipHard()
    {
        mainScrollRect.enabled = true;
        playerHard.Stop();
        texture.Release();
        clipHard.SetActive(false);
    }


    private void GetFinalResult()
    {
        titleGUI.text = finalResultName;
        descGUI.text = finalResultDesc;
        detailedDescGUI.text = finalResultDescDetails;
        matchGUI.text = matchDesc;
        balanceGUI.text = balanceDesc;
        switch (finalResultEnum)
        {
            case FinalResultEnum.INTUITIVE:
                foreach (Image i in cardBackgrounds)
                {
                    i.sprite = intuitiveCardSprite;
                }
                break;
            case FinalResultEnum.CHANGER:
                foreach (Image i in cardBackgrounds)
                {
                    i.sprite = socialChangerCardSprite;
                }
                break;
            case FinalResultEnum.DOER:
                foreach (Image i in cardBackgrounds)
                {
                    i.sprite = socialDoerCardSprite;
                }
                break;
            case FinalResultEnum.PIONEER:
                foreach (Image i in cardBackgrounds)
                {
                    i.sprite = pioneerCardSprite;
                }
                break;
            case FinalResultEnum.CREATOR:
                foreach (Image i in cardBackgrounds)
                {
                    i.sprite = creatorCardSprite;
                }
                break;
            case FinalResultEnum.BUILDER:
                foreach (Image i in cardBackgrounds)
                {
                    i.sprite = builderCardSprite;
                }
                break;
        }
    }

    private Dictionary<(CardTitle, CardTitle), Action> methodMap = new Dictionary<(CardTitle, CardTitle), Action>()
    {
        //INTUITIVE RESULTS
        { (CardTitle.HELPFUL, CardTitle.CURIOUS), SetUpIntuitive},
        { (CardTitle.CURIOUS, CardTitle.HELPFUL), SetUpIntuitive },
        
        { (CardTitle.HELPFUL, CardTitle.PLAYFUL), SetUpIntuitive },
        { (CardTitle.PLAYFUL, CardTitle.HELPFUL), SetUpIntuitive },
        
        { (CardTitle.EMPATHY, CardTitle.CURIOUS), SetUpIntuitive },
        { (CardTitle.CURIOUS, CardTitle.EMPATHY), SetUpIntuitive },
        
        { (CardTitle.EMPATHY, CardTitle.PLAYFUL), SetUpIntuitive },
        { (CardTitle.PLAYFUL, CardTitle.EMPATHY), SetUpIntuitive },
        
        //SOCIAL CHANGER
        { (CardTitle.HELPFUL, CardTitle.PROGRESSIVE), SetUpSocialChanger },
        { (CardTitle.PROGRESSIVE, CardTitle.HELPFUL), SetUpSocialChanger },
        
        { (CardTitle.HELPFUL, CardTitle.PERSEVERE), SetUpSocialChanger },
        { (CardTitle.PERSEVERE, CardTitle.HELPFUL), SetUpSocialChanger },
        
        { (CardTitle.EMPATHY, CardTitle.PROGRESSIVE), SetUpSocialChanger },
        { (CardTitle.PROGRESSIVE, CardTitle.EMPATHY), SetUpSocialChanger },
        
        { (CardTitle.EMPATHY, CardTitle.PERSEVERE), SetUpSocialChanger },
        { (CardTitle.PERSEVERE, CardTitle.EMPATHY), SetUpSocialChanger },
        
        //SOCIAL DOER
        { (CardTitle.HELPFUL, CardTitle.SENSE), SetUpSocialDoer },
        { (CardTitle.SENSE, CardTitle.HELPFUL), SetUpSocialDoer },
        
        { (CardTitle.HELPFUL, CardTitle.RIGHT), SetUpSocialDoer },
        { (CardTitle.RIGHT, CardTitle.HELPFUL), SetUpSocialDoer },
        
        { (CardTitle.EMPATHY, CardTitle.SENSE), SetUpSocialDoer },
        { (CardTitle.SENSE, CardTitle.EMPATHY), SetUpSocialDoer },
        
        { (CardTitle.EMPATHY, CardTitle.RIGHT), SetUpSocialDoer },
        { (CardTitle.RIGHT, CardTitle.EMPATHY), SetUpSocialDoer },
        
        //PIONEER
        { (CardTitle.CURIOUS, CardTitle.PROGRESSIVE), SetUpPioneer },
        { (CardTitle.PROGRESSIVE, CardTitle.CURIOUS), SetUpPioneer },
        
        { (CardTitle.CURIOUS, CardTitle.PERSEVERE), SetUpPioneer },
        { (CardTitle.PERSEVERE, CardTitle.CURIOUS), SetUpPioneer },
        
        { (CardTitle.PLAYFUL, CardTitle.PROGRESSIVE), SetUpPioneer },
        { (CardTitle.PROGRESSIVE, CardTitle.PLAYFUL), SetUpPioneer },
        
        { (CardTitle.PLAYFUL, CardTitle.PERSEVERE), SetUpPioneer },
        { (CardTitle.PERSEVERE, CardTitle.PLAYFUL), SetUpPioneer },
        
        //CREATOR
        { (CardTitle.CURIOUS, CardTitle.SENSE), SetUpCreator },
        { (CardTitle.SENSE, CardTitle.CURIOUS), SetUpCreator },
        
        { (CardTitle.CURIOUS, CardTitle.RIGHT), SetUpCreator },
        { (CardTitle.RIGHT, CardTitle.CURIOUS), SetUpCreator },
        
        { (CardTitle.PLAYFUL, CardTitle.SENSE), SetUpCreator },
        { (CardTitle.SENSE, CardTitle.PLAYFUL), SetUpCreator },
        
        { (CardTitle.PLAYFUL, CardTitle.RIGHT), SetUpCreator },
        { (CardTitle.RIGHT, CardTitle.PLAYFUL), SetUpCreator },
        
        //BUILDER
        { (CardTitle.PROGRESSIVE, CardTitle.SENSE), SetUpBuilder },
        { (CardTitle.SENSE, CardTitle.PROGRESSIVE), SetUpBuilder },
        
        { (CardTitle.PROGRESSIVE, CardTitle.RIGHT), SetUpBuilder },
        { (CardTitle.RIGHT, CardTitle.PROGRESSIVE), SetUpBuilder },
        
        { (CardTitle.PERSEVERE, CardTitle.SENSE), SetUpBuilder },
        { (CardTitle.SENSE, CardTitle.PERSEVERE), SetUpBuilder },
        
        { (CardTitle.PERSEVERE, CardTitle.RIGHT), SetUpBuilder },
        { (CardTitle.RIGHT, CardTitle.PERSEVERE), SetUpBuilder }
    };

    private static void SetUpIntuitive()
    {
        finalResultEnum = FinalResultEnum.INTUITIVE;
        finalResultName = LocalizationManager.Localize("Results.IntuitiveTitle");
        finalResultDesc = LocalizationManager.Localize("Results.IntuitiveDescSummary");
        finalResultDescDetails = LocalizationManager.Localize("Results.IntuitiveDescDetailed");
        balanceDesc = LocalizationManager.Localize("Results.IntuitiveBalanceDesc");
        matchDesc = LocalizationManager.Localize("Results.IntuitiveMatchDesc");
    }

    private static void SetUpSocialChanger()
    {
        finalResultEnum = FinalResultEnum.CHANGER;
        finalResultName = LocalizationManager.Localize("Results.SocialChangerTitle");
        finalResultDesc = LocalizationManager.Localize("Results.SocialChangerDescSummary");
        finalResultDescDetails = LocalizationManager.Localize("Results.SocialChangerDescDetailed");
        balanceDesc = LocalizationManager.Localize("Results.SocialChangerBalanceDesc");
        matchDesc = LocalizationManager.Localize("Results.SocialChangerMatchDesc");
    }
    
    
    private static void SetUpSocialDoer()
    {
        finalResultEnum = FinalResultEnum.DOER;
        finalResultName = LocalizationManager.Localize("Results.SocialDoerTitle");
        finalResultDesc = LocalizationManager.Localize("Results.SocialDoerDescSummary");
        finalResultDescDetails = LocalizationManager.Localize("Results.SocialDoerDescDetailed");
        balanceDesc = LocalizationManager.Localize("Results.SocialDoerBalanceDesc");
        matchDesc = LocalizationManager.Localize("Results.SocialDoerMatchDesc");
    }
    
    private static void SetUpPioneer()
    {
        finalResultEnum = FinalResultEnum.PIONEER;
        finalResultName = LocalizationManager.Localize("Results.PioneerTitle");
        finalResultDesc = LocalizationManager.Localize("Results.PioneerDescSummary");
        finalResultDescDetails = LocalizationManager.Localize("Results.PioneerDescDetailed");
        balanceDesc = LocalizationManager.Localize("Results.PioneerBalanceDesc");
        matchDesc = LocalizationManager.Localize("Results.PioneerMatchDesc");
    }
    
    private static void SetUpCreator()
    {
        finalResultEnum = FinalResultEnum.CREATOR;
        finalResultName = LocalizationManager.Localize("Results.CreatorTitle");
        finalResultDesc = LocalizationManager.Localize("Results.CreatorDescSummary");
        finalResultDescDetails = LocalizationManager.Localize("Results.CreatorDescDetailed");
        balanceDesc = LocalizationManager.Localize("Results.CreatorBalanceDesc");
        matchDesc = LocalizationManager.Localize("Results.CreatorMatchDesc");
    }
    
    private static void SetUpBuilder()
    {
        finalResultEnum = FinalResultEnum.BUILDER;
        finalResultName = LocalizationManager.Localize("Results.BuilderTitle");
        finalResultDesc = LocalizationManager.Localize("Results.BuilderDescSummary");
        finalResultDescDetails = LocalizationManager.Localize("Results.BuilderDescDetailed");
        balanceDesc = LocalizationManager.Localize("Results.BuilderBalanceDesc");
        matchDesc = LocalizationManager.Localize("Results.BuilderMatchDesc");
    }
    
}

public enum FinalResultEnum
{
    INTUITIVE,
    CHANGER,
    DOER,
    PIONEER,
    CREATOR,
    BUILDER
}
