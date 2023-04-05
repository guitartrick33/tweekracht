using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackButtonScript : MonoBehaviour
{
    public GameObject feedbackPanel;
    // Start is called before the first frame update
    void Start()
    {
        feedbackPanel.SetActive(false);
    }

    public void OpenFeedbackPanel()
    {
        feedbackPanel.SetActive(true);
    }

    public void CloseFeedbackPanel()
    {
        feedbackPanel.SetActive(false);
    }

    public void LinkToForms()
    {
        Application.OpenURL("https://forms.gle/J5MohjraVsQAgUN4A");
    }
}
