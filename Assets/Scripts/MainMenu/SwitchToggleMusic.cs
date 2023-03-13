using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SwitchToggleMusic : MonoBehaviour
{
    public Toggle soundToggle;
    [SerializeField] private RectTransform soundHandle;
    private Vector2 soundHandlePosition;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Image soundSprite;
    public Color offBackgroundColor;
    public Color onBackgroundColor;
    void Start()
    {
        soundHandlePosition = soundHandle.anchoredPosition;
        soundToggle.onValueChanged.AddListener(MusicSwitcher);
        if (soundToggle.isOn)
        {
            MusicSwitcher(true);
        }
    }
    
    public void MusicSwitcher(bool on)
    {
        if (on)
        {
            AudioManager.Instance.isMusicOn = true;
            soundHandle.anchoredPosition = soundHandlePosition * -1;
            soundSprite.sprite = soundOnSprite;
            GetComponent<Image>().color = onBackgroundColor;
        }
        else
        {
            AudioManager.Instance.isMusicOn = false;
            soundHandle.anchoredPosition = soundHandlePosition;
            soundSprite.sprite = soundOffSprite;
            GetComponent<Image>().color = offBackgroundColor;
        }
    }
}
