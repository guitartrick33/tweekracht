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
    [SerializeField] private GameObject closeSoftClipButton;
    [SerializeField] private GameObject closeHardClipButton;
    private VideoClip startClip;
    // [SerializeField] private Animator animController;
    
    [SerializeField] private ScrollRect mainScrollRect;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private RectTransform popUpContainerRT;
    [SerializeField] private Transform texts;
    [SerializeField] private Transform titles;
    private Vector3 textsStartPos;
    [SerializeField] private GameObject circleImages;

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
    public SaveLoadManager saveLoadManager;
    [Space] 
    [SerializeField] private string filename;

    
    private CardTitle saveSoftCardTitle;
    private CardTitle saveHardCardTitle;
    private FinalResultEnum saveFinalResultEnum;
    

    private void Start()
    {
        popUpPanel.SetActive(false);
        mainScrollRect.enabled = true;
        circleImages.SetActive(true);
        clipHard.SetActive(false);
        clipSoft.SetActive(false);
        closeSoftClipButton.SetActive(false);
        closeHardClipButton.SetActive(false);
        finalResultName = String.Empty;
        finalResultDesc = String.Empty;
        finalResultDescDetails = String.Empty;
        balanceDesc = String.Empty;
        matchDesc = String.Empty;
        textsStartPos = texts.position;
        foreach (Transform t in texts)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in titles)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void OpenPopUpDescription(GameObject text)
    {
        popUpPanel.SetActive(true);
        mainScrollRect.enabled = false;
        circleImages.SetActive(false);
        text.SetActive(true);
        Vector2 activeSize = text.GetComponent<RectTransform>().sizeDelta;
        popUpContainerRT.sizeDelta = new Vector2(popUpContainerRT.sizeDelta.x, activeSize.y);
        
        RectTransform parentRect = popUpContainerRT;
        RectTransform titleRect = text.GetComponent<RectTransform>();
        
        float preferredHeight = LayoutUtility.GetPreferredHeight(titleRect);
        
        titleRect.sizeDelta = new Vector2(titleRect.sizeDelta.x, preferredHeight);
        titleRect.anchorMin = new Vector2(titleRect.anchorMin.x, 1);
        titleRect.anchorMax = new Vector2(titleRect.anchorMax.x, 1);
        titleRect.pivot = new Vector2(titleRect.pivot.x, 1);
        titleRect.anchoredPosition = new Vector2(titleRect.anchoredPosition.x, 0);
        
        parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, preferredHeight);
        parentRect.anchoredPosition = new Vector2(parentRect.anchoredPosition.x, parentRect.anchoredPosition.y - preferredHeight/2f);
    }

    public void SetPopUpTitles(GameObject title)
    {
        title.SetActive(true);
    }

    public void ClosePopUp()
    {
        popUpPanel.SetActive(false);
        mainScrollRect.enabled = true;
        circleImages.SetActive(true);
        texts.position = textsStartPos;
        foreach (Transform t in texts)
        {
            t.gameObject.SetActive(false);
        }
        foreach (Transform t in titles)
        {
            t.gameObject.SetActive(false);
        }
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
            GetFinalResult();
        }
        
    }

    public void SetClipSoft()
    {
        clipSoft.SetActive(true);
        closeSoftClipButton.SetActive(true);
        mainScrollRect.enabled = false;
        playerSoft.clip = cardDescSoftFinal.currentClip;
        if (!AudioManager.Instance.isMusicOn)
        {
            playerSoft.SetDirectAudioMute(0, true);  
        }
        playerSoft.Play();
        playerSoft.loopPointReached += (vp) => ResetClipSoft() ;
    }

    public void ResetClipSoft()
    {
        mainScrollRect.enabled = true;
        playerSoft.Stop();
        texture.Release();
        clipSoft.SetActive(false);
        closeSoftClipButton.SetActive(false);
    }

    public void SetClipHard()
    {
        clipHard.SetActive(true);
        closeHardClipButton.SetActive(true);
        mainScrollRect.enabled = false;
        playerHard.clip = cardDescHardFinal.currentClip;
        if (!AudioManager.Instance.isMusicOn)
        {
            playerHard.SetDirectAudioMute(0, true);  
        }
        playerHard.Play();
        playerHard.loopPointReached += (vp) => ResetClipHard() ;
    }

    public void ResetClipHard()
    {
        closeHardClipButton.SetActive(false);
        mainScrollRect.enabled = true;
        playerHard.Stop();
        texture.Release();
        clipHard.SetActive(false);
    }

    public void SaveResults()
    {
        saveSoftCardTitle = cardDescSoftFinal.cardTitle;
        saveHardCardTitle = cardDescHardFinal.cardTitle;
        if (StartViewController.Instance.GameType() != GameType.FRIENDS)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            ResultClass result = new ResultClass(date, saveSoftCardTitle, saveHardCardTitle, saveFinalResultEnum, StartViewController.Instance.GameType());
            saveLoadManager.results.Add(result);
        }
        else
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            ResultClass result = new ResultClass(date, saveSoftCardTitle, saveHardCardTitle, saveFinalResultEnum, StartViewController.Instance.GameType(), StartViewController.Instance.GetFriendsName());
            saveLoadManager.results.Add(result);
        }
        saveLoadManager.SaveToJSON<ResultClass>(saveLoadManager.results, filename);
    }
    
    private void GetFinalResult()
    {
        saveFinalResultEnum = finalResultEnum;
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
        SaveResults();
    }

    private Dictionary<(CardTitle, CardTitle), Action> methodMap = new Dictionary<(CardTitle, CardTitle), Action>()
    {
        //INTUITIVE RESULTS
        { (CardTitle.HELPFUL, CardTitle.CURIOUS), () => SetUpIntuitive(LocalizationManager.Localize("Results.IntuitiveHelpfulCurious"), LocalizationManager.Localize("Results.IntuitiveBalanceHelpfulCurious"), LocalizationManager.Localize("Results.IntuitiveMatchHelpfulCurious"))},
        { (CardTitle.CURIOUS, CardTitle.HELPFUL), () => SetUpIntuitive(LocalizationManager.Localize("Results.IntuitiveHelpfulCurious"), LocalizationManager.Localize("Results.IntuitiveBalanceHelpfulCurious"), LocalizationManager.Localize("Results.IntuitiveBalanceHelpfulCurious")) },
        
        { (CardTitle.HELPFUL, CardTitle.PLAYFUL), () => SetUpIntuitive(LocalizationManager.Localize("Results.IntuitiveHelpfulPlayful"), LocalizationManager.Localize("Results.IntuitiveBalanceHelpfulPlayful"), LocalizationManager.Localize("Results.IntuitiveMatchHelpfulPlayful")) },
        { (CardTitle.PLAYFUL, CardTitle.HELPFUL), () => SetUpIntuitive(LocalizationManager.Localize("Results.IntuitiveHelpfulPlayful"), LocalizationManager.Localize("Results.IntuitiveBalanceHelpfulPlayful"), LocalizationManager.Localize("Results.IntuitiveMatchHelpfulPlayful")) },
        
        { (CardTitle.EMPATHY, CardTitle.CURIOUS), () => SetUpIntuitive(LocalizationManager.Localize("Results.IntuitiveEmpathicCurious"), LocalizationManager.Localize("Results.IntuitiveBalanceEmpathicCurious"), LocalizationManager.Localize("Results.IntuitiveMatchEmpathicCurious")) },
        { (CardTitle.CURIOUS, CardTitle.EMPATHY), () => SetUpIntuitive(LocalizationManager.Localize("Results.IntuitiveEmpathicCurious"), LocalizationManager.Localize("Results.IntuitiveBalanceEmpathicCurious"), LocalizationManager.Localize("Results.IntuitiveMatchEmpathicCurious")) },
        
        { (CardTitle.EMPATHY, CardTitle.PLAYFUL), () => SetUpIntuitive(LocalizationManager.Localize("Results.IntuitiveEmpathicPlayful"), LocalizationManager.Localize("Results.IntuitiveBalanceEmpathicPlayful"), LocalizationManager.Localize("Results.IntuitiveMatchEmpathicPlayful")) },
        { (CardTitle.PLAYFUL, CardTitle.EMPATHY), () => SetUpIntuitive(LocalizationManager.Localize("Results.IntuitiveEmpathicPlayful"), LocalizationManager.Localize("Results.IntuitiveBalanceEmpathicPlayful"), LocalizationManager.Localize("Results.IntuitiveMatchEmpathicPlayful")) },
        
        //SOCIAL CHANGER
        { (CardTitle.HELPFUL, CardTitle.PROGRESSIVE), () => SetUpSocialChanger(LocalizationManager.Localize("Results.SocialChangerHelpfulProgressive"), LocalizationManager.Localize("Results.SocialChangerBalanceHelpfulProgressive"), LocalizationManager.Localize("Results.SocialChangerMatchHelpfulProgressive")) },
        { (CardTitle.PROGRESSIVE, CardTitle.HELPFUL), () => SetUpSocialChanger(LocalizationManager.Localize("Results.SocialChangerHelpfulProgressive"), LocalizationManager.Localize("Results.SocialChangerBalanceHelpfulProgressive"), LocalizationManager.Localize("Results.SocialChangerMatchHelpfulProgressive")) },
        
        { (CardTitle.HELPFUL, CardTitle.PERSEVERE), () => SetUpSocialChanger(LocalizationManager.Localize("Results.SocialChangerHelpfulPersevere"), LocalizationManager.Localize("Results.SocialChangerBalanceHelpfulPersevere"), LocalizationManager.Localize("Results.SocialChangerMatchHelpfulPersevere")) },
        { (CardTitle.PERSEVERE, CardTitle.HELPFUL), () => SetUpSocialChanger(LocalizationManager.Localize("Results.SocialChangerHelpfulPersevere"), LocalizationManager.Localize("Results.SocialChangerBalanceHelpfulPersevere"), LocalizationManager.Localize("Results.SocialChangerMatchHelpfulPersevere")) },
        
        { (CardTitle.EMPATHY, CardTitle.PROGRESSIVE), () => SetUpSocialChanger(LocalizationManager.Localize("Results.SocialChangerEmpathicProgressive"), LocalizationManager.Localize("Results.SocialChangerBalanceEmpathicProgressive"), LocalizationManager.Localize("Results.SocialChangerMatchEmpathicProgressive")) },
        { (CardTitle.PROGRESSIVE, CardTitle.EMPATHY), () => SetUpSocialChanger(LocalizationManager.Localize("Results.SocialChangerEmpathicProgressive"), LocalizationManager.Localize("Results.SocialChangerBalanceEmpathicProgressive"), LocalizationManager.Localize("Results.SocialChangerMatchEmpathicProgressive")) },
        
        { (CardTitle.EMPATHY, CardTitle.PERSEVERE), () => SetUpSocialChanger(LocalizationManager.Localize("Results.SocialChangerEmpathicPersevere"), LocalizationManager.Localize("Results.SocialChangerBalanceEmpathicPersevere"), LocalizationManager.Localize("Results.SocialChangerMatchEmpathicPersevere")) },
        { (CardTitle.PERSEVERE, CardTitle.EMPATHY), () => SetUpSocialChanger(LocalizationManager.Localize("Results.SocialChangerEmpathicPersevere"), LocalizationManager.Localize("Results.SocialChangerBalanceEmpathicPersevere"), LocalizationManager.Localize("Results.SocialChangerMatchEmpathicPersevere")) },
        
        //SOCIAL DOER
        { (CardTitle.HELPFUL, CardTitle.SENSE), () => SetUpSocialDoer(LocalizationManager.Localize("Results.SocialDoerHelpfulSense"), LocalizationManager.Localize("Results.SocialDoerBalanceHelpfulSense"), LocalizationManager.Localize("Results.SocialDoerMatchHelpfulSense")) },
        { (CardTitle.SENSE, CardTitle.HELPFUL), () => SetUpSocialDoer(LocalizationManager.Localize("Results.SocialDoerHelpfulSense"), LocalizationManager.Localize("Results.SocialDoerBalanceHelpfulSense"), LocalizationManager.Localize("Results.SocialDoerMatchHelpfulSense")) },
        
        { (CardTitle.HELPFUL, CardTitle.RIGHT), () => SetUpSocialDoer(LocalizationManager.Localize("Results.SocialDoerHelpfulRight"), LocalizationManager.Localize("Results.SocialDoerBalanceHelpfulRight"), LocalizationManager.Localize("Results.SocialDoerMatchHelpfulRight")) },
        { (CardTitle.RIGHT, CardTitle.HELPFUL), () => SetUpSocialDoer(LocalizationManager.Localize("Results.SocialDoerHelpfulRight"), LocalizationManager.Localize("Results.SocialDoerBalanceHelpfulRight"), LocalizationManager.Localize("Results.SocialDoerMatchHelpfulRight")) },
        
        { (CardTitle.EMPATHY, CardTitle.SENSE), () => SetUpSocialDoer(LocalizationManager.Localize("Results.SocialDoerEmpathicSense"), LocalizationManager.Localize("Results.SocialDoerBalanceEmpathicSense"), LocalizationManager.Localize("Results.SocialDoerMatchEmpathicSense")) },
        { (CardTitle.SENSE, CardTitle.EMPATHY), () => SetUpSocialDoer(LocalizationManager.Localize("Results.SocialDoerEmpathicSense"), LocalizationManager.Localize("Results.SocialDoerBalanceEmpathicSense"), LocalizationManager.Localize("Results.SocialDoerMatchEmpathicSense")) },
        
        { (CardTitle.EMPATHY, CardTitle.RIGHT), () => SetUpSocialDoer(LocalizationManager.Localize("Results.SocialDoerEmpathicRight"), LocalizationManager.Localize("Results.SocialDoerBalanceEmpathicRight"), LocalizationManager.Localize("Results.SocialDoerMatchEmpathicRight")) },
        { (CardTitle.RIGHT, CardTitle.EMPATHY), () => SetUpSocialDoer(LocalizationManager.Localize("Results.SocialDoerEmpathicRight"), LocalizationManager.Localize("Results.SocialDoerBalanceEmpathicRight"), LocalizationManager.Localize("Results.SocialDoerMatchEmpathicRight")) },
        
        //PIONEER
        { (CardTitle.CURIOUS, CardTitle.PROGRESSIVE), () => SetUpPioneer(LocalizationManager.Localize("Results.PioneerCuriousProgressive"), LocalizationManager.Localize("Results.PioneerBalanceCuriousProgressive"), LocalizationManager.Localize("Results.PioneerMatchCuriousProgressive")) },
        { (CardTitle.PROGRESSIVE, CardTitle.CURIOUS), () => SetUpPioneer(LocalizationManager.Localize("Results.PioneerCuriousProgressive"), LocalizationManager.Localize("Results.PioneerBalanceCuriousProgressive"), LocalizationManager.Localize("Results.PioneerMatchCuriousProgressive")) },
        
        { (CardTitle.CURIOUS, CardTitle.PERSEVERE), () => SetUpPioneer(LocalizationManager.Localize("Results.PioneerCuriousPersevere"), LocalizationManager.Localize("Results.PioneerBalanceCuriousPersevere"), LocalizationManager.Localize("Results.PioneerMatchCuriousPersevere")) },
        { (CardTitle.PERSEVERE, CardTitle.CURIOUS), () => SetUpPioneer(LocalizationManager.Localize("Results.PioneerCuriousPersevere"), LocalizationManager.Localize("Results.PioneerBalanceCuriousPersevere"), LocalizationManager.Localize("Results.PioneerMatchCuriousPersevere")) },
        
        { (CardTitle.PLAYFUL, CardTitle.PROGRESSIVE), () => SetUpPioneer(LocalizationManager.Localize("Results.PioneerPlayfulProgressive"), LocalizationManager.Localize("Results.PioneerBalancePlayfulProgressive"), LocalizationManager.Localize("Results.PioneerMatchPlayfulProgressive")) },
        { (CardTitle.PROGRESSIVE, CardTitle.PLAYFUL), () => SetUpPioneer(LocalizationManager.Localize("Results.PioneerPlayfulProgressive"), LocalizationManager.Localize("Results.PioneerBalancePlayfulProgressive"), LocalizationManager.Localize("Results.PioneerMatchPlayfulProgressive")) },
        
        { (CardTitle.PLAYFUL, CardTitle.PERSEVERE), () => SetUpPioneer(LocalizationManager.Localize("Results.PioneerPlayfulPersevere"), LocalizationManager.Localize("Results.PioneerBalancePlayfulPersevere"), LocalizationManager.Localize("Results.PioneerMatchPlayfulPersevere")) },
        { (CardTitle.PERSEVERE, CardTitle.PLAYFUL), () => SetUpPioneer(LocalizationManager.Localize("Results.PioneerPlayfulPersevere"), LocalizationManager.Localize("Results.PioneerBalancePlayfulPersevere"), LocalizationManager.Localize("Results.PioneerMatchPlayfulPersevere")) },
        
        //CREATOR
        { (CardTitle.CURIOUS, CardTitle.SENSE), () => SetUpCreator(LocalizationManager.Localize("Results.CreatorCuriousSense"), LocalizationManager.Localize("Results.CreatorBalanceCuriousSense"), LocalizationManager.Localize("Results.CreatorMatchCuriousSense")) },
        { (CardTitle.SENSE, CardTitle.CURIOUS), () => SetUpCreator(LocalizationManager.Localize("Results.CreatorCuriousSense"), LocalizationManager.Localize("Results.CreatorBalanceCuriousSense"), LocalizationManager.Localize("Results.CreatorMatchCuriousSense")) },
        
        { (CardTitle.CURIOUS, CardTitle.RIGHT), () => SetUpCreator(LocalizationManager.Localize("Results.CreatorCuriousRight"), LocalizationManager.Localize("Results.CreatorBalanceCuriousRight"), LocalizationManager.Localize("Results.CreatorMatchCuriousRight")) },
        { (CardTitle.RIGHT, CardTitle.CURIOUS), () => SetUpCreator(LocalizationManager.Localize("Results.CreatorCuriousRight"), LocalizationManager.Localize("Results.CreatorBalanceCuriousRight"), LocalizationManager.Localize("Results.CreatorMatchCuriousRight")) },
        
        { (CardTitle.PLAYFUL, CardTitle.SENSE), () => SetUpCreator(LocalizationManager.Localize("Results.CreatorPlayfulSense"), LocalizationManager.Localize("Results.CreatorBalancePlayfulSense"), LocalizationManager.Localize("Results.CreatorMatchPlayfulSense")) },
        { (CardTitle.SENSE, CardTitle.PLAYFUL), () => SetUpCreator(LocalizationManager.Localize("Results.CreatorPlayfulSense"), LocalizationManager.Localize("Results.CreatorBalancePlayfulSense"), LocalizationManager.Localize("Results.CreatorMatchPlayfulSense")) },
        
        { (CardTitle.PLAYFUL, CardTitle.RIGHT), () => SetUpCreator(LocalizationManager.Localize("Results.CreatorPlayfulRight"), LocalizationManager.Localize("Results.CreatorBalancePlayfulRight"), LocalizationManager.Localize("Results.CreatorMatchPlayfulRight")) },
        { (CardTitle.RIGHT, CardTitle.PLAYFUL), () => SetUpCreator(LocalizationManager.Localize("Results.CreatorPlayfulRight"), LocalizationManager.Localize("Results.CreatorBalancePlayfulRight"), LocalizationManager.Localize("Results.CreatorMatchPlayfulRight")) },
        
        //BUILDER
        { (CardTitle.PROGRESSIVE, CardTitle.SENSE), () => SetUpBuilder(LocalizationManager.Localize("Results.ConstructorProgressiveSense"), LocalizationManager.Localize("Results.ConstructorBalanceProgressiveSense"), LocalizationManager.Localize("Results.ConstructorMatchProgressiveSense")) },
        { (CardTitle.SENSE, CardTitle.PROGRESSIVE), () => SetUpBuilder(LocalizationManager.Localize("Results.ConstructorProgressiveSense"), LocalizationManager.Localize("Results.ConstructorBalanceProgressiveSense"), LocalizationManager.Localize("Results.ConstructorMatchProgressiveSense")) },
        
        { (CardTitle.PROGRESSIVE, CardTitle.RIGHT), () => SetUpBuilder(LocalizationManager.Localize("Results.ConstructorProgressiveRight"), LocalizationManager.Localize("Results.ConstructorBalanceProgressiveRight"), LocalizationManager.Localize("Results.ConstructorMatchProgressiveRight")) },
        { (CardTitle.RIGHT, CardTitle.PROGRESSIVE), () => SetUpBuilder(LocalizationManager.Localize("Results.ConstructorProgressiveRight"), LocalizationManager.Localize("Results.ConstructorBalanceProgressiveRight"), LocalizationManager.Localize("Results.ConstructorMatchProgressiveRight")) },
        
        { (CardTitle.PERSEVERE, CardTitle.SENSE), () => SetUpBuilder(LocalizationManager.Localize("Results.ConstructorPersevereSense"), LocalizationManager.Localize("Results.ConstructorBalancePersevereSense"), LocalizationManager.Localize("Results.ConstructorMatchPersevereSense")) },
        { (CardTitle.SENSE, CardTitle.PERSEVERE), () => SetUpBuilder(LocalizationManager.Localize("Results.ConstructorPersevereSense"), LocalizationManager.Localize("Results.ConstructorBalancePersevereSense"), LocalizationManager.Localize("Results.ConstructorMatchPersevereSense")) },
        
        { (CardTitle.PERSEVERE, CardTitle.RIGHT), () => SetUpBuilder(LocalizationManager.Localize("Results.ConstructorPersevereRight"), LocalizationManager.Localize("Results.ConstructorBalancePersevereRight"), LocalizationManager.Localize("Results.ConstructorMatchPersevereRight")) },
        { (CardTitle.RIGHT, CardTitle.PERSEVERE), () => SetUpBuilder(LocalizationManager.Localize("Results.ConstructorPersevereRight"), LocalizationManager.Localize("Results.ConstructorBalancePersevereRight"), LocalizationManager.Localize("Results.ConstructorMatchPersevereRight")) }
    };

    private static void SetUpIntuitive(string shortDesc, string balanceDescLoc, string matchDescLoc)
    {
        finalResultEnum = FinalResultEnum.INTUITIVE;
        finalResultName = LocalizationManager.Localize("Results.IntuitiveTitle");
        finalResultDesc = shortDesc;
        finalResultDescDetails = LocalizationManager.Localize("Results.IntuitiveDescDetailed");
        balanceDesc = balanceDescLoc;
        matchDesc = matchDescLoc;
    }

    private static void SetUpSocialChanger(string shortDesc, string balanceDescLoc, string matchDescLoc)
    {
        finalResultEnum = FinalResultEnum.CHANGER;
        finalResultName = LocalizationManager.Localize("Results.SocialChangerTitle").ToUpper();
        finalResultDesc = shortDesc;
        finalResultDescDetails = LocalizationManager.Localize("Results.SocialChangerDescDetailed");
        balanceDesc = balanceDescLoc;
        matchDesc = matchDescLoc;
    }
    
    
    private static void SetUpSocialDoer(string shortDesc, string balanceDescLoc, string matchDescLoc)
    {
        finalResultEnum = FinalResultEnum.DOER;
        finalResultName = LocalizationManager.Localize("Results.SocialDoerTitle").ToUpper();
        finalResultDesc = shortDesc;
        finalResultDescDetails = LocalizationManager.Localize("Results.SocialDoerDescDetailed");
        balanceDesc = balanceDescLoc;
        matchDesc = matchDescLoc;
    }
    
    private static void SetUpPioneer(string shortDesc, string balanceDescLoc, string matchDescLoc)
    {
        finalResultEnum = FinalResultEnum.PIONEER;
        finalResultName = LocalizationManager.Localize("Results.PioneerTitle").ToUpper();
        finalResultDesc = shortDesc;
        finalResultDescDetails = LocalizationManager.Localize("Results.PioneerDescDetailed");
        balanceDesc = balanceDescLoc;
        matchDesc = matchDescLoc;
    }
    
    private static void SetUpCreator(string shortDesc, string balanceDescLoc, string matchDescLoc)
    {
        finalResultEnum = FinalResultEnum.CREATOR;
        finalResultName = LocalizationManager.Localize("Results.CreatorTitle").ToUpper();
        finalResultDesc = shortDesc;
        finalResultDescDetails = LocalizationManager.Localize("Results.CreatorDescDetailed");
        balanceDesc = balanceDescLoc;
        matchDesc = matchDescLoc;
    }
    
    private static void SetUpBuilder(string shortDesc, string balanceDescLoc, string matchDescLoc)
    {
        finalResultEnum = FinalResultEnum.BUILDER;
        finalResultName = LocalizationManager.Localize("Results.ConstructorTitle").ToUpper();
        finalResultDesc = shortDesc;
        finalResultDescDetails = LocalizationManager.Localize("Results.ConstructorDescDetailed");
        balanceDesc = balanceDescLoc;
        matchDesc = matchDescLoc;
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
