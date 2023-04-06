using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResultController : MonoBehaviour
{
    public GameObject resultElementPrefab;
    public Transform scrollView;
    public int nrOfItems;

    private void Start()
    {
        // PopulateList();
    }

    public void PopulateList()
    {
        foreach (Transform t in scrollView)
        {
            Destroy(t.gameObject);
        }
        nrOfItems = 0;
        foreach (ResultClass rc in SaveLoadManager.Instance.results)
        {
            GameObject element = Instantiate(resultElementPrefab, scrollView);
            element.GetComponent<ResultElement>().finalResult = rc.finalCardTitle;
            element.GetComponent<ResultElement>().softSide = rc.softCardTitle;
            element.GetComponent<ResultElement>().hardSide = rc.hardCardTitle;
            element.GetComponent<ResultElement>().gameType = rc.gameType;
            element.GetComponent<ResultElement>().friendsName = rc.friendName;
            element.GetComponent<ResultElement>().date = rc.date;
            element.GetComponent<ResultElement>().SetMainElementTexts();
            element.GetComponent<ResultElement>().UpdatePage();
            nrOfItems++;
        }

        scrollView.GetComponent<RectTransform>().sizeDelta =
            new Vector2(0, nrOfItems * resultElementPrefab.GetComponent<RectTransform>().rect.height);
    }

    private void OnEnable()
    {
        PopulateList();
    }
}
