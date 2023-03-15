using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.Video;

public class IntroductionVideoController : MonoBehaviour
{
    private List<VideoClip> videoClips;
    private VideoPlayer videoPlayer;
    private int videoClipIndex;
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
        videoClipIndex = 0;
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
        }
    }

    private void Start()
    {
        switch (LocalizationManager.Language)
        {
            case ("English"):
                videoClips.AddRange(enClips);
                break;
            case ("Dutch"):
                videoClips.AddRange(nlClips);
                break;
                
        }
        StartVideo();
        foreach (Transform t in texts)
        {
            t.gameObject.SetActive(false);
        }
        foreach (Transform t in titles)
        {
            t.gameObject.SetActive(false);
        }
    } 

    private void StartVideo()
    {
        if (!AudioManager.Instance.isMusicOn)
        {
          videoPlayer.SetDirectAudioMute(0, true);  
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        if (videoClipIndex >= videoClips.Count - 1)
        {
            videoPlayer.loopPointReached += SkipVideo;
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
}