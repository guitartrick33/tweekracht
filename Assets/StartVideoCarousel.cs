using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVideoCarousel : MonoBehaviour
{
    [SerializeField] private Carousel carousel;

    private bool isPlayed;
    [SerializeField] private int currentPage;
    private void Start()
    {
        isPlayed = false;
    }

    private void Update()
    {
        if (!isPlayed && carousel._currentPage == currentPage)
        {
            GetComponent<CardVideoPlayer>().SetClip();
            isPlayed = true;
        }
    }
}
