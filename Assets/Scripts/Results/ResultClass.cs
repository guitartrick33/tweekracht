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
    public GameType gameType;
    public string friendName;

    public ResultClass(CardTitle softCardTitle, CardTitle hardCardTitle, FinalResultEnum finalCardTitle, GameType gameType)
    {
        this.softCardTitle = softCardTitle;
        this.hardCardTitle = hardCardTitle;
        this.finalCardTitle = finalCardTitle;
        this.gameType = gameType;
    }
    
    public ResultClass(CardTitle softCardTitle, CardTitle hardCardTitle, FinalResultEnum finalCardTitle, GameType gameType, string friendName)
    {
        this.softCardTitle = softCardTitle;
        this.hardCardTitle = hardCardTitle;
        this.finalCardTitle = finalCardTitle;
        this.gameType = gameType;
        this.friendName = friendName;
    }
}
