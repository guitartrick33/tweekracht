using System.Collections.Generic;
using System.Linq;
using Assets.SimpleLocalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuViewController : MonoBehaviour
{
        
    [SerializeField] private TMP_Dropdown dropDownLanguages;
    [SerializeField] private Sprite mainMenuBackgroundPhones;
    [SerializeField] private Sprite mainMenuBackgroundTablets;
    [Space]
    [SerializeField] private Sprite settingsBackgroundPhones;
    [SerializeField] private Sprite settingsBackgroundTablets;
    [Space]
    [SerializeField] private Sprite profileBackgroundPhones;
    [SerializeField] private Sprite profileBackgroundTablets;
    [Space]
    [SerializeField] private Sprite playBackgroundPhones;
    [SerializeField] private Sprite playBackgroundTablets;
    [Space]
    [SerializeField] private Image actualBackgroundImage;
    [SerializeField] private List<GameObject> pages;
    private GameObject currentPage;
    [SerializeField] private ViewType viewTypeAfterStart;
    private bool isPhone;

    private void Awake()
    {

        if(DeviceDiagonalSizeInInches() > 6.5f && AspectRatio() < 2f)
        {
            actualBackgroundImage.sprite = mainMenuBackgroundTablets;
            isPhone = false;
        }
        else
        {
            actualBackgroundImage.sprite = mainMenuBackgroundPhones;
            isPhone = true;
        }
    }

    private void Start()
    {
        dropDownLanguages.captionText.text = LocalizationManager.Language;
        currentPage = pages[0];
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        currentPage.SetActive(true);
    }

    public void RedirectToWebsite() //Redirects the user upon touching it to Tweekracht's webpage
    {
        if (LocalizationManager.Language == "Dutch")
        {
            Application.OpenURL("https://www.tweekracht.nl/nl/home/");
        }
        else
        {
            Application.OpenURL("https://www.tweekracht.nl/en/home-2/");
        }
    }

    public void RedirectToShop()
    {
        if (LocalizationManager.Language == "Dutch")
        {
            Application.OpenURL("https://www.tweekracht.nl/nl/product-categorie/spel/");
        }
        else
        {
            Application.OpenURL("https://www.tweekracht.nl/en/shop/");
        }
    }
    
    public void RedirectToOurView()
    {
        if (LocalizationManager.Language == "Dutch")
        {
            Application.OpenURL("https://www.tweekracht.nl/nl/individueel/");
        }
        else
        {
            Application.OpenURL("https://www.tweekracht.nl/en/background-2/");
        }
    }

    public void SetLocalization() //Usage of this method can be seen for the OnValueChanged event in the language dropdown component
    {
        LocalizationController.Instance.SetLocalization(dropDownLanguages.options[dropDownLanguages.value].text);
    }

    public static float DeviceDiagonalSizeInInches() //Returns the initial size of the screen of the current device
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt (Mathf.Pow (screenWidth, 2) + Mathf.Pow (screenHeight, 2));
        return diagonalInches;
    }

    public static int AspectRatio() //Returns the aspect ratio of the current device
    {
        var aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        return aspectRatio;
    }

    public void ChangePanel(GameObject targetPage) //Function used to switch between different pages, can be found in every button in main menu
    {
        currentPage.SetActive(false);
        targetPage.SetActive(true);
        currentPage = targetPage;
        foreach (GameObject page in pages)
        {
            if (ReferenceEquals(page, currentPage))
            {
                if (page.GetComponent<PageTypeController>().GetPageType() == PageType.SETTINGS)
                {
                    if (isPhone)
                    {
                        actualBackgroundImage.sprite = settingsBackgroundPhones;
                    }
                    else
                    {
                        actualBackgroundImage.sprite = settingsBackgroundTablets;
                    }
                }
                else  if (page.GetComponent<PageTypeController>().GetPageType() == PageType.MAIN)
                {
                    if (isPhone)
                    {
                        actualBackgroundImage.sprite = mainMenuBackgroundPhones;
                    }
                    else
                    {
                        actualBackgroundImage.sprite = mainMenuBackgroundTablets;
                    }
                }
                else  if (page.GetComponent<PageTypeController>().GetPageType() == PageType.PROFILE)
                {
                    if (isPhone)
                    {
                        actualBackgroundImage.sprite = profileBackgroundPhones;
                    }
                    else
                    {
                        actualBackgroundImage.sprite = profileBackgroundTablets;
                    }
                }
                else  if (page.GetComponent<PageTypeController>().GetPageType() == PageType.PLAY)
                {
                    if (isPhone)
                    {
                        actualBackgroundImage.sprite = playBackgroundPhones;
                    }
                    else
                    {
                        actualBackgroundImage.sprite = playBackgroundTablets;
                    }
                }
            }
        }
    }

    public void StartGame()
    {
        ViewManager.Instance.SwitchView(viewTypeAfterStart);
    }
}
