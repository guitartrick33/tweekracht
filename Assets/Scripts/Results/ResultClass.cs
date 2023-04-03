using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ResultClass
{
    public CardTitle softCardTitle;
    public CardTitle hardCardTitle;
    public FinalResultEnum finalCardTitle;

    public ResultClass(CardTitle softCardTitle, CardTitle hardCardTitle, FinalResultEnum finalCardTitle)
    {
        this.softCardTitle = softCardTitle;
        this.hardCardTitle = hardCardTitle;
        this.finalCardTitle = finalCardTitle;
    }
}
