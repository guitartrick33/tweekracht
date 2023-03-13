using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public bool isMusicOn;
    public bool isSoundOn;
    public AudioSource buttonClickSound;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        isMusicOn = true;
        isSoundOn = true;
    }
    public static AudioManager Instance
    {
        get { return instance; }
    }

    public void PlayButtonClick()
    {
        if (isSoundOn)
        {
            buttonClickSound.Play();
        }
    }
}
