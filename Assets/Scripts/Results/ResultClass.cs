using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ResultClass
{
    public string date;
    public CardTitle softCardTitle;
    public CardTitle hardCardTitle;
    public FinalResultEnum finalCardTitle;
    public GameType gameType;
    public string friendName;

    public ResultClass(string date, CardTitle softCardTitle, CardTitle hardCardTitle, FinalResultEnum finalCardTitle, GameType gameType)
    {
        this.date = date;
        this.softCardTitle = softCardTitle;
        this.hardCardTitle = hardCardTitle;
        this.finalCardTitle = finalCardTitle;
        this.gameType = gameType;
    }
    
    public ResultClass(string date, CardTitle softCardTitle, CardTitle hardCardTitle, FinalResultEnum finalCardTitle, GameType gameType, string friendName)
    {
        this.date = date;
        this.softCardTitle = softCardTitle;
        this.hardCardTitle = hardCardTitle;
        this.finalCardTitle = finalCardTitle;
        this.gameType = gameType;
        this.friendName = friendName;
    }
}
