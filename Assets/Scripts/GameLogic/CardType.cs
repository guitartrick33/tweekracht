using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardType : MonoBehaviour
{
    [SerializeField] private CardTypeEnum cardType;

    public CardTypeEnum GetCardType()
    {
        return cardType;
    }
}

public enum CardTypeEnum{
    SOFT,
    HARD,
    NONE
}
