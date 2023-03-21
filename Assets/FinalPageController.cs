using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalPageController : MonoBehaviour
{
    [SerializeField] private ScrollRect mainScrollRect;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject circleImages;
    [SerializeField] private GameObject saveButton;

    private void Start()
    {
        popUpPanel.SetActive(false);
        mainScrollRect.enabled = true;
        circleImages.SetActive(true);
        saveButton.SetActive(true);
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
}
