using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    [SerializeField] private GameObject softSideText;
    [SerializeField] private GameObject hardSideText;
    [SerializeField] private GameObject softDiamond;
    [SerializeField] private GameObject hardDiamond;
    [SerializeField] private TextMeshProUGUI resultText;

    public void Awake()
    {
        softSideText.SetActive(false);
        hardSideText.SetActive(false);
        resultText.text = String.Empty;
    }

    public void SetSoftSide(string result)
    {
        hardDiamond.SetActive(false);
        softDiamond.SetActive(true);
        hardSideText.SetActive(false);
        softSideText.SetActive(true);
        resultText.text = result;
    }

    public void SetHardSide(string result)
    {
        hardDiamond.SetActive(true);
        softDiamond.SetActive(false);
        softSideText.SetActive(false);
        hardSideText.SetActive(true);
        resultText.text = result;
    }
}
