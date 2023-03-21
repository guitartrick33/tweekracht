using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFirstTime : MonoBehaviour
{
    [SerializeField] private GameObject skipButton;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("isFirstTimeBool") && PlayerPrefs.GetInt("isFirstTimeBool") != 1)
        {
            skipButton.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("isFirstTimeBool") == 1)
        {
            skipButton.SetActive(true);
        }
    }

    public void SetFirstTimeTrue()
    {
        PlayerPrefs.SetInt("isFirstTimeBool", 1);
        PlayerPrefs.Save();
    }
}
