using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CardVideoPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer clipPlayer;

    [SerializeField] private VideoClip clipEN;
    [SerializeField] private VideoClip clipNL;
    [SerializeField] private RenderTexture texture;

    [SerializeField] private GameObject clipRawImage;

    [SerializeField] private GameObject closeClipButton;

    [SerializeField] private ScrollRect mainScrollRect;

    [SerializeField] private GameObject button;


    public void Start()
    {
        clipRawImage.SetActive(false);
        closeClipButton.SetActive(false);
    }

    public void SetClip()
    {
        clipRawImage.SetActive(true);
        clipRawImage.GetComponent<RawImage>().raycastTarget = true;
        closeClipButton.SetActive(true);
        mainScrollRect.enabled = false;
        button.SetActive(false);
        switch (LocalizationManager.Language)
        {
            case "Dutch":
                clipPlayer.clip = clipNL;
                break;
            default:
                clipPlayer.clip = clipEN;
                break;
        }

        if (!AudioManager.Instance.isMusicOn)
        {
            clipPlayer.SetDirectAudioMute(0, true);
        }

        clipPlayer.Play();
        clipPlayer.loopPointReached += (vp) => ResetClip();
    }

    public void ResetClip()
    {
        closeClipButton.SetActive(false);
        mainScrollRect.enabled = true;
        clipPlayer.Stop();
        texture.Release();
        clipRawImage.GetComponent<RawImage>().raycastTarget = false;
        clipRawImage.SetActive(false);
        button.SetActive(true);
    }

    private void OnEnable()
    {
        ResetClip();
    }
}

