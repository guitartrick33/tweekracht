using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class CardDesc : MonoBehaviour
{
    [SerializeField] private string cardName;
    [SerializeField] private TextMeshProUGUI cardText;
    public GameObject softSideText;
    public GameObject hardSideText;
    public GameObject softSideDiamond;
    public GameObject hardSideDiamond;
    
    [Space]
    [Header("Check if this is a final choice")]
    [Space]
    [Tooltip("If this is checked, then NextCard1 and NextCard2 will be ignored")]
    public bool isLast;
    public GameObject nextCard1;
    public GameObject nextCard2;
    [Space]
    public VideoClip hardClip;
    public VideoClip softClip;
    public VideoPlayer videoPlayer;
    public GameObject texture;

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
        hardSideText.SetActive(false);
        hardSideDiamond.SetActive(false);
        softSideText.SetActive(true);
        softSideDiamond.SetActive(true);
    }

    public void SetHardSide()
    {
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
}
