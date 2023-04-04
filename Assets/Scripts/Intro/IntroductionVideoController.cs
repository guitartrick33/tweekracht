using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroductionVideoController : MonoBehaviour
{
    public List<VideoClip> videoClips;
    private VideoPlayer videoPlayer;
    public int videoClipIndex;
    public List<VideoClip> nlClips;
    public List<VideoClip> enClips;

    [SerializeField] private GameObject backgroundPanel;
    
    ViewManager viewManager;
    [SerializeField] private ViewType viewTypeAfterIntro;
    public Transform texts;
    public Transform titles;

    private void Awake()
    {
        videoClips = new List<VideoClip>();
        videoPlayer = GetComponent<VideoPlayer>();
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
        }
    }

    public void SetLocalizationVids()
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

    private void Start()
    {
        videoClipIndex = 0;
        SetLocalizationVids();
        StartVideo();
        
        if (texts is not null)
        {
            foreach (Transform t in texts)
            {
                t.gameObject.SetActive(false);
            } 
        }

        if (titles is not null)
        {
            foreach (Transform t in titles)
            {
                t.gameObject.SetActive(false);
            } 
        }
    }

    private void OnEnable()
    {
        StartVideo();
        SetLocalizationVids();
    }


    private void StartVideo()
    {
        Debug.Log(videoClips[0].name);
        if (!AudioManager.Instance.isMusicOn)
        {
          videoPlayer.SetDirectAudioMute(0, true);  
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        if (videoClipIndex >= videoClips.Count - 1)
        {
            videoPlayer.loopPointReached += OpenPanel;
        }
        else
        {
            videoPlayer.loopPointReached += OpenPanel;
        }
    }

    public void SkipVideo(VideoPlayer vp)
    {
        if (videoClipIndex >= videoClips.Count - 1)
        {
            videoPlayer.Stop();
            if (TryGetComponent(out CheckFirstTime firstTime))
            {
                if (!PlayerPrefs.HasKey("isFirstTimeBool") && PlayerPrefs.GetInt("isFirstTimeBool") != 1)
                {
                    firstTime.SetFirstTimeTrue();
                }
            }
            ViewManager.Instance.SwitchView(viewTypeAfterIntro);
        }
        else
        {
            videoPlayer.Stop();
            videoClipIndex++;
        }
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
            foreach (Transform t in texts)
            {
                t.gameObject.SetActive(false);
            }
            foreach (Transform t in titles)
            {
                t.gameObject.SetActive(false);
            }
        }
        
        StartVideo();
    }
    
    private void OpenPanel(VideoPlayer vp)
    {
        backgroundPanel.SetActive(true);
        texts.GetChild(videoClipIndex).gameObject.SetActive(true);
        titles.GetChild(videoClipIndex).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        videoClipIndex = 0;
    }
}