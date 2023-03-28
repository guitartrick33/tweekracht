using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;

public class SuitabilityController : MonoBehaviour
{
    [SerializeField] private GameObject hardSideButton;
    [SerializeField] private GameObject softSideButton;
    [SerializeField] private GameObject card1Button;
    [SerializeField] private GameObject card2Button;
    [SerializeField] private TextMeshProUGUI card1TMPro;
    [SerializeField] private TextMeshProUGUI card2TMPro;

    public CardController cardController;

    public string localizationChooseCardKey;
    public string localizationChooseSideKey;
    public TextMeshProUGUI questionText;

    private CardDesc chosenCard;

    private void Start()
    {
        // cardController = FindObjectOfType<CardController>();
    }

    public void SetCardButtons()
    {
        questionText.text = LocalizationManager.Localize(localizationChooseCardKey);
        hardSideButton.SetActive(false);
        softSideButton.SetActive(false);
        card1Button.SetActive(true);
        card2Button.SetActive(true);
        SetCardButtonText();
    }

    public void SetSideButtons()
    {
        questionText.text = LocalizationManager.Localize(localizationChooseSideKey);
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
        SetSideButtons();
    }

    public void ChooseCard2Button()
    {
        chosenCard = cardController.resultSoftSide;
        SetSideButtons();
    }

    private void SetCardButtonText()
    {
        chosenCard = null;
        card1TMPro.text = cardController.resultHardSideTitle;
        card2TMPro.text = cardController.resultSoftSideTitle;
    }
}
