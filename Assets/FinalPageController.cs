using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
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

    public string finalResultName;
    public string finalResultDesc;
    public string balanceDesc;
    public string matchDesc;

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
        if (cardDescHardFinal != null && cardDescSoftFinal != null)
        {
            softSideDescUI.text = cardDescSoftFinal.description.text;
            softSideTitleUI.text = cardDescSoftFinal.CardText();
            hardSideDescUI.text = cardDescHardFinal.description.text;
            hardSideTitleUI.text = cardDescHardFinal.CardText();
        }
    }

    // public void ClickVideoClip(bool isSoft)
    // {
    //     StartCoroutine(VideoCoroutine(isSoft));
    //     
    // }
    //
    // IEnumerator VideoCoroutine(bool isSoft)
    // {
    //     circleImages.SetActive(false);
    //     saveButton.SetActive(false);
    //     animController.SetBool("DropDownBool", true);
    //     yield return new WaitForSeconds(1.5f);
    //     if (isSoft)
    //     {
    //         SetClipSoft();
    //     }
    //     else
    //     {
    //         SetClipHard();
    //     }
    //
    // }

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

    public void OnEnable()
    {
        UpdatePage();
        texture.Release();
    }
}
