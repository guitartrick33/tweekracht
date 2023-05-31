using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SideMiniGameController : MonoBehaviour
{
    private CardController cardController;
    [SerializeField] private Image screenImage;
    public List<Sprite> imageSprites;
    private int softSideScore;
    private int hardSideScore;
    private int index;
    private CardTypeEnum currentCardType;
    public GameObject panelDesc;
    public GameObject panelChoose;
    public GameObject softButton;
    public GameObject hardButton;
    private void Awake()
    {
        cardController = FindObjectOfType<CardController>();
    }

    void Start()
    {
        softSideScore = 0;
        hardSideScore = 0;
        index = 0;
        screenImage.gameObject.SetActive(false);
        panelChoose.SetActive(false);
        panelDesc.SetActive(false);
        SpawnImage();
    }

    private void SpawnImage()
    {
        if (index < imageSprites.Count)
        {
            //RANDOMIZED PICS FOR THE MINI GAME
            
            // index = Random.Range(0, imageSprites.Count);
            // imageSprites.RemoveAt(index);
            
            screenImage.sprite = imageSprites[index];
            screenImage.gameObject.SetActive(true);
        }
        else
        {
            if (hardSideScore > softSideScore)
            {
                currentCardType = CardTypeEnum.HARD;
            }
            else
            {
                currentCardType = CardTypeEnum.SOFT;
            }
            panelDesc.SetActive(true);
        }
        
    }

    public void SelectHard()
    {
        hardSideScore++;
        index++;
        SpawnImage();
    }

    public void SelectSoft()
    {
        softSideScore++;
        index++;
        SpawnImage();
    }

    public void ContinueToChoice()
    {
        panelChoose.SetActive(true);
        if (currentCardType == CardTypeEnum.HARD)
        {
            softButton.SetActive(true);
            hardButton.SetActive(false);
        }
        else
        {
            hardButton.SetActive(true);
            softButton.SetActive(false);
        }
    }

    public void ChooseHardSide()
    {
        cardController.side = CardTypeEnum.HARD;
        cardController.hasChosenASide = true;
        ViewManager.Instance.SwitchView(ViewType.Game);
        Reset();
    }

    public void ChooseSoftSide()
    {
        cardController.side = CardTypeEnum.SOFT;
        cardController.hasChosenASide = true;
        ViewManager.Instance.SwitchView(ViewType.Game);
        Reset();
    }

    public void FreeToChoose()
    {
        cardController.side = CardTypeEnum.NONE;
        cardController.hasChosenASide = false;
        ViewManager.Instance.SwitchView(ViewType.Game);
        Reset();
    }

    public void Reset()
    {
        index = 0;
        softSideScore = 0;
        hardSideScore = 0;
        panelChoose.SetActive(false);
        panelDesc.SetActive(false);
    }

    public void BackToMenu()
    {
        Reset();
        ViewManager.Instance.SwitchView(ViewType.MainMenu);
    }
}
