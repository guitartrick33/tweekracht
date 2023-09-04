using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.Video;

public class IntroductionPanelController : MonoBehaviour
{
    private int index;
    public List<GameObject> introPanels;
    public List<VideoClip> videoClips;
    public VideoPlayer videoPlayer;
    public List<VideoClip> nlClips;
    public List<VideoClip> enClips;
    private GameObject currentPanel;
    private bool hasPlayedIntroVid;

    private void Awake()
    {
        index = 0;
        foreach (GameObject g in introPanels)
        {
            g.SetActive(false);
        }
    }

    public void UpdatePage(VideoPlayer vp)
    {
        introPanels[index].SetActive(true);
        currentPanel = introPanels[index];
    }

    private void OnEnable()
    {
        hasPlayedIntroVid = false;
        SetLocalizationVids();
        if (!hasPlayedIntroVid)
        {
            videoPlayer.clip = videoClips[0];
            videoPlayer.Play();
            videoPlayer.loopPointReached += UpdatePage;
            if (!AudioManager.Instance.isMusicOn)
            {
                videoPlayer.SetDirectAudioMute(0, true);
            }
            hasPlayedIntroVid = true;
        }
        else
        {
            UpdatePage(videoPlayer);
        }
        
    }

    private void OnDisable()
    {
        index = 0;
        ClosePanels();
    }

    public void SetLocalizationVids() //NEEDS TO BE UPDATED IF MORE LANGUAGES ARE ADDED
    {
        videoClips.Clear();
        switch (LocalizationManager.Language)
        {
            case ("Dutch"):
                videoClips.AddRange(nlClips);
                break;
            default:
                videoClips.AddRange(enClips);
                break;
        }
    }

    public void PlayVideo1()
    {
        ClosePanels();
        videoPlayer.clip = videoClips[1];
        videoPlayer.Play();
        videoPlayer.loopPointReached += ResetPanel;
    }

    public void PlayVideo2()
    {
        ClosePanels();
        videoPlayer.clip = videoClips[2];
        videoPlayer.Play();
        videoPlayer.loopPointReached += ResetPanel;
    }
    
    public void PlayVideo3()
    {
        ClosePanels();
        videoPlayer.clip = videoClips[3];
        videoPlayer.Play();
        videoPlayer.loopPointReached += ResetPanel;
    }

    public void NextPanel()
    {
        index++;
        if (index >= introPanels.Count)
        {
            ViewManager.Instance.SwitchView(ViewType.SidesGame);
        }
        UpdatePage(videoPlayer);
    }

    private void ResetPanel(VideoPlayer vp)
    {
        currentPanel.SetActive(true);
    }

    private void ClosePanels()
    {
        foreach (GameObject g in introPanels)
        {
            g.SetActive(false);
        }
    }

    public void SkipVideo()
    {
        videoPlayer.Stop();
        UpdatePage(videoPlayer);
    }
}
