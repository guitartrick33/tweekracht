using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class FinalPageController : MonoBehaviour
{
    [SerializeField] private ScrollRect mainScrollRect;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject circleImages;
    [SerializeField] private GameObject saveButton;

    public string finalResultName;
    public string finalResultDesc;
    public string hardSideTitle;
    public string hardSideDesc;
    public string softSideTitle;
    public string softSideDesc;
    public string balanceDesc;
    public string matchDesc;

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

    public void ClearResults()
    {
        hardSideDesc = String.Empty;
        softSideDesc = String.Empty;
        hardSideTitle = String.Empty;
        softSideTitle = String.Empty;
    }
}
