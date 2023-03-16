using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggleSound : MonoBehaviour
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
        soundToggle.onValueChanged.AddListener(SoundFXSwitcher);
        soundToggle.isOn = AudioManager.Instance.isSoundOn;
        if (soundToggle.isOn)
        {
            SoundFXSwitcher(true);
        }
    }

    private void Update()
    {
        if (AudioManager.Instance.isSoundOn)
        {
            soundToggle.isOn = true;
        }
        else
        {
            soundToggle.isOn = false;
        }
    }

    public void SoundFXSwitcher(bool on)
    {
        if (on)
        {
            AudioManager.Instance.isSoundOn = true;
            soundHandle.anchoredPosition = soundHandlePosition * -1;
            soundSprite.sprite = soundOnSprite;
            GetComponent<Image>().color = onBackgroundColor;
        }
        else
        {
            AudioManager.Instance.isSoundOn = false;
            soundHandle.anchoredPosition = soundHandlePosition;
            soundSprite.sprite = soundOffSprite;
            GetComponent<Image>().color = offBackgroundColor;
        }
    }
}
