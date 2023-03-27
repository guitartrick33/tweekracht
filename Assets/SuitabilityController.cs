using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SuitabilityController : MonoBehaviour
{
    [SerializeField] private GameObject hardSideButton;
    [SerializeField] private GameObject softSideButton;
    [SerializeField] private GameObject card1Button;
    [SerializeField] private GameObject card2Button;

    private CardController cardController;
    public List<GameObject> buttons;

    private CardDesc chosenCard;

    private void Start()
    {
        cardController = FindObjectOfType<CardController>();
        SetCardButtons();
    }

    public void SetCardButtons()
    {
        hardSideButton.SetActive(false);
        softSideButton.SetActive(false);
        card1Button.SetActive(true);
        card2Button.SetActive(true);
    }

    public void SetSideButtons()
    {
        hardSideButton.SetActive(true);
        softSideButton.SetActive(true);
        card1Button.SetActive(false);
        card2Button.SetActive(false);
    }

    public void ChooseHardSideButtonCoroutine()
    {
        if (cardController.suitCounter == 1)
        {
            if (cardController.side == CardTypeEnum.SOFT)
            {
                cardController.resultSoftSide = chosenCard;
                cardController.resultSoftSideTitle = chosenCard.CardText();
            }

            if (cardController.side == CardTypeEnum.HARD)
            {
                cardController.resultHardSide = chosenCard;
                cardController.resultHardSideTitle = chosenCard.CardText();
            }
        }
        cardController.resultSoftSide = null;
        cardController.resultSoftSideTitle = String.Empty;
        cardController.side = CardTypeEnum.HARD;
        StartCoroutine(cardController.ContinuePath());
    }

    public void ChooseSoftSideButtonCourotine()
    {
        if (cardController.suitCounter == 1)
        {
            if (cardController.side == CardTypeEnum.SOFT)
            {
                cardController.resultSoftSide = chosenCard;
                cardController.resultSoftSideTitle = chosenCard.CardText();
            }

            if (cardController.side == CardTypeEnum.HARD)
            {
                cardController.resultHardSide = chosenCard;
                cardController.resultHardSideTitle = chosenCard.CardText();
            }
        }
        cardController.resultHardSide = null;
        cardController.resultHardSideTitle = String.Empty;
        cardController.side = CardTypeEnum.SOFT;
        StartCoroutine(cardController.ContinuePath());
    }

    public void ChooseCard1Button()
    {
        chosenCard = cardController.resultHardSide;
        Debug.Log(chosenCard.CardText());
        SetSideButtons();
    }

    public void ChooseCard2Button()
    {
        chosenCard = cardController.resultSoftSide;
        Debug.Log(chosenCard.CardText());
        SetSideButtons();
    }

    private void OnEnable()
    {
        chosenCard = null;
        card1Button.GetComponentInChildren<TextMeshProUGUI>().text = cardController.resultHardSideTitle;
        card2Button.GetComponentInChildren<TextMeshProUGUI>().text = cardController.resultSoftSideTitle;
    }
}
