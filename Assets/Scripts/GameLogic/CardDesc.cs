using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CardDesc : MonoBehaviour
{
    [SerializeField] private string cardName;
    [SerializeField] private TextMeshProUGUI cardText;
    public CardTitle cardTitle;
    public GameObject softSideText;
    public GameObject hardSideText;
    public GameObject softSideDiamond;
    public GameObject hardSideDiamond;
    public TextMeshProUGUI description;
    

    [Space]
    [Header("Check if this is a final choice")]
    [Space]
    [Tooltip("If this is checked, then NextCard1 and NextCard2 will be ignored")]
    public bool isLast;
    public GameObject nextCard1;
    public GameObject nextCard2;
    [Space]
    public VideoClip hardClipNL;
    public VideoClip softClipNL;
    [Space]
    public VideoClip hardClipEN;
    public VideoClip softClipEN;
    
    public VideoClip currentClip;
    public VideoPlayer videoPlayer;
    public GameObject texture;
    
    private CardController cardController;
    public GameObject infoPanel;
    private bool isSoft;
    

    private void Update()
    {
        //Since each card has its own videoplayer, this is the best way to mute/unmute audio based on the audio settings
        if (videoPlayer.isPlaying && !AudioManager.Instance.isMusicOn)
        {
            videoPlayer.SetDirectAudioMute(0, true);  
        }
        else
        {
            videoPlayer.SetDirectAudioMute(0, false);
        }
    }

    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Texture")
            {
                texture = transform.GetChild(i).gameObject;
            }
        }
        if (isLast)
        {
            nextCard1 = null;
            nextCard2 = null;
        }

        if (!AudioManager.Instance.isMusicOn)
        {
            videoPlayer.SetDirectAudioMute(0, true);
        }
        
        DisableTexture();
        
        cardController = FindObjectOfType<CardController>();
        infoPanel.SetActive(false);
        
        //Checks for localization - needs to be changed if more languages are added
        switch (LocalizationManager.Language)
        {
            case ("English"):
                if (isSoft)
                {
                    currentClip = softClipEN;
                }
                else
                {
                    currentClip = hardClipEN;
                }
                break;
            case ("Dutch"):
                if (isSoft)
                {
                    currentClip = softClipNL;
                }
                else
                {
                    currentClip = hardClipNL;
                }
                break;
        }
    }

    public void SetVideoClip()
    {
        videoPlayer.clip = currentClip;
    }

    public String CardName()
    {
        return this.cardName;
    }

    public String CardText()
    {
        return this.cardText.text;
    }

    public void SetSoftSide()
    {
        isSoft = true;
        hardSideText.SetActive(false);
        hardSideDiamond.SetActive(false);
        softSideText.SetActive(true);
        softSideDiamond.SetActive(true);
    }

    public void SetHardSide()
    {
        isSoft = false;
        softSideText.SetActive(false);
        softSideDiamond.SetActive(false);
        hardSideText.SetActive(true);
        hardSideDiamond.SetActive(true);
    }

    public void DisableTexture()
    {
        texture.SetActive(false);
    }

    public void EnableTexture()
    {
        texture.SetActive(true);
    }

    public void ShowTextDesc()
    {
        cardController.ShowInfoText(gameObject);
        infoPanel.SetActive(true);
        if (!isSoft)
        {
            infoPanel.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void CloseTextDesc()
    {
        cardController.CloseInfoText();
        infoPanel.SetActive(false);
    }
}

public enum CardTitle
{
    ACTIVE,
    CREATE,
    CURIOUS,
    EMPATHY,
    GROWTH,
    HELPFUL,
    PERSEVERE,
    PLAYFUL,
    PROGRESSIVE,
    RIGHT,
    SENSE,
    SOCIAL,
    SURPRISING,
    WARM
}
