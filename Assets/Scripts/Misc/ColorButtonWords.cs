using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ColorButtonWords : MonoBehaviour
{
    public Color letterColor;
    public Color letterColorTest;

    private void OnEnable()
    {
        Debug.Log("lol");
        ColorWords();
    }

    private void ColorWords()
    {
        TextMeshProUGUI tgui = GetComponent<TextMeshProUGUI>();
        var s = tgui.text;
        bool containsOnlyOneWord = s.Split().Length == 1;
        if (!containsOnlyOneWord)
        {
            string[] words = s.Split(' ');
            string word = $"<color=#{letterColor.ToHexString()}>" + words[0] +
                          $"</color=#{letterColor.ToHexString()}>";
            for (int i = 1; i < words.Length; i++)
            {
                word += " " + $"<color=#{letterColorTest.ToHexString()}>" + words[i] +
                        $"</color=#{letterColorTest.ToHexString()}>";
            }
            tgui.text = word;
        }
        else
        {
            string word = $"<color=#{letterColor.ToHexString()}>" + tgui.text +
                          $"</color=#{letterColor.ToHexString()}> ";
            tgui.text = word;
        }
    }
}
