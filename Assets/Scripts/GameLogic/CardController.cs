using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class CardController : MonoBehaviour
{
    public GameObject titlePickSide;
    public List<GameObject> cards;
    public List<String> card1Names;
    public List<String> card2Names;
    public GameObject spawnCard1;
    public GameObject spawnCard2;

    public GameObject spawnCard1Location;
    public GameObject spawnCard2Location;

    public GameObject backButton;

    private string currentChoice;

    public GameObject currentCard1;
    public GameObject currentCard2;

    public Sprite softSideBackground;
    public Sprite hardSideBackground;

    public CardTypeEnum side;

    public string resultSoftSide;
    public string resultHardSide;
    public GameObject resultPanel;
    public GameObject resultCard;

    public RenderTexture texture;

    private Camera mainCamera;

    public bool interactive;
    private bool isPlayingVideo;
    private bool isInfoPanelOpen;
    public VideoPlayer vp;

    public GameObject finalPanel;

    private void Awake()
    {
        card1Names = new List<string>();
        card2Names = new List<string>();
        
        side = CardTypeEnum.NONE;
        
        resultSoftSide = String.Empty;
        resultHardSide = String.Empty;
        resultPanel.SetActive(false);
        finalPanel.SetActive(false);
        
        backButton.SetActive(false);
        
        texture.Release();
        
        isPlayingVideo = false;
        isInfoPanelOpen = false;
        interactive = false;
        
        mainCamera = Camera.main;
        vp = null;
        ResetSidesCompletely();
    }

    private void OnEnable() //Method used to reset titles when going back from the game to the main menu and then back to the game
    {
        titlePickSide.SetActive(true);
        GetComponent<GameTypeController>().DisableTexts();
    }

    private void Update()
    {
        if (card1Names.Count > 0 && card2Names.Count > 0 && !resultPanel.activeSelf && !isPlayingVideo && !isInfoPanelOpen)
        {
            backButton.SetActive(true);
        }
        else
        {
            backButton.SetActive(false);
        }
    }

    public void PickASide(GameObject card) //Acts as the event for selecting the side and initiating the Spawn Coroutine 
    {
        Debug.Log("0");
        if (interactive)
        {
            Debug.Log("1");
            AudioManager.Instance.PlayButtonClick();
            spawnCard1Location.GetComponent<Animator>().SetTrigger("SwipeInTrigger");
            spawnCard2Location.GetComponent<Animator>().SetTrigger("SwipeInTrigger");
            Debug.Log("2");
            if (card.GetComponent<CardType>().GetCardType() == CardTypeEnum.SOFT)
            {
                side = CardTypeEnum.SOFT;
            }
            else if (card.GetComponent<CardType>().GetCardType() == CardTypeEnum.HARD)
            {
                side = CardTypeEnum.HARD;
            }
            StartCoroutine(Spawn());
            interactive = false;
        }
    }
    
    IEnumerator  Spawn() //Spawn the default 2 cards after selecting the first side
    {
        yield return new WaitForSeconds(1.4f);
        titlePickSide.SetActive(false);
        
        GetComponent<GameTypeController>().EnableText();
        
        spawnCard1Location.SetActive(true);
        spawnCard2Location.SetActive(true);
        
        GameObject card1 = Instantiate(spawnCard1, spawnCard1Location.transform.position, spawnCard1Location.transform.rotation, spawnCard1Location.transform);
        GameObject card2 = Instantiate(spawnCard2, spawnCard2Location.transform.position, spawnCard2Location.transform.rotation,  spawnCard2Location.transform);
        
        MakeCardPickable(card1);
        MakeCardPickable(card2);

        CheckBackground(card1);
        CheckBackground(card2);

        currentCard1 = card1;
        currentCard2 = card2;
        currentChoice = String.Empty;
                
    }

    public void PickCard(GameObject card) //Pick a card event - picks a card and initiates animations
    {
        if (interactive)
        {
            AudioManager.Instance.PlayButtonClick();
            CardDesc cardDesc = card.GetComponent<CardDesc>();
            cardDesc.videoPlayer.targetCamera = mainCamera;
            cardDesc.SetVideoClip();
            isPlayingVideo = true;

            if (ReferenceEquals(card, currentCard1))
            {
                spawnCard2Location.SetActive(false);
                spawnCard1Location.GetComponent<Animator>().SetBool("SetToLarge", true);
            }
            else if (ReferenceEquals(card, currentCard2))
            {
                spawnCard1Location.SetActive(false);
                spawnCard2Location.GetComponent<Animator>().SetBool("SetToLarge", true);
            }

            StartCoroutine(WaitBeforePlayingVideo(cardDesc, card));
            interactive = false;
        }
    } 

    IEnumerator WaitBeforePlayingVideo(CardDesc cardDesc, GameObject card)
    {
        yield return new WaitForSeconds(1);
        cardDesc.videoPlayer.Play();
        vp = cardDesc.videoPlayer;
        yield return new WaitForSeconds(0.5f); //Added this because on the APK emulator, the textures didn't load as intended so that refrains from showing them as empty
        cardDesc.EnableTexture();
        cardDesc.videoPlayer.loopPointReached += (vp) => StartCoroutine(RefreshCards(card));
    }

    IEnumerator RefreshCards(GameObject card) //Refreshes the cards after the video from PickCard is done
    {
        isPlayingVideo = false;
        
        if (!card.GetComponent<CardDesc>().isLast)
        {
            spawnCard1Location.GetComponent<Animator>().SetBool("SetToLarge", false);
            spawnCard2Location.GetComponent<Animator>().SetBool("SetToLarge", false);
        
            yield return new WaitForSeconds(0.6f);
        
            spawnCard1Location.SetActive(true);
            spawnCard2Location.SetActive(true);
            
            texture.Release();
            card1Names.Add(currentCard1.GetComponent<CardDesc>().CardName());
            card2Names.Add(currentCard2.GetComponent<CardDesc>().CardName());
            
            Destroy(currentCard1);
            Destroy(currentCard2);
            
            GameObject card1 = Instantiate(card.GetComponent<CardDesc>().nextCard1, spawnCard1Location.transform.position, spawnCard1Location.transform.rotation,
                spawnCard1Location.transform);
            GameObject card2 = Instantiate(card.GetComponent<CardDesc>().nextCard2, spawnCard2Location.transform.position, spawnCard2Location.transform.rotation,
                spawnCard2Location.transform);
            
            MakeCardPickable(card1);
            MakeCardPickable(card2);
            
            CheckBackground(card1);
            CheckBackground(card2);
            
            currentChoice = card.GetComponent<CardDesc>().CardName();
            currentCard1 = card1;
            currentCard2 = card2;
        }
        else
        {
            StartCoroutine(GetResult(card));
        }
    }
    
    private void MakeCardPickable(GameObject card) //Makes the instantiated cards buttons as they require the CardController script from the Game View object
    {
        Button b = card.GetComponent<Button>();
        b.onClick.AddListener(delegate { PickCard(card); });
    }

    IEnumerator GetResult(GameObject card) //Result view opens up with the result of each side
    {
        if (ReferenceEquals(card, currentCard1))
        {
            spawnCard1Location.GetComponent<Animator>().SetBool("SwipeOutBool", true);
            spawnCard2Location.SetActive(false);
            
        }
        else
        {
            spawnCard2Location.GetComponent<Animator>().SetBool("SwipeOutBool", true);
            spawnCard1Location.SetActive(false);
        }

        yield return new WaitForSeconds(1);
        spawnCard1Location.SetActive(false);
        spawnCard2Location.SetActive(false);
        currentChoice = card.GetComponent<CardDesc>().CardName();
        if (side == CardTypeEnum.SOFT && resultSoftSide == String.Empty)
        {
            resultSoftSide = currentChoice;
            finalPanel.GetComponent<FinalPageController>().cardDescSoftFinal = card.GetComponent<CardDesc>();
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<ResultController>().SetSoftSide(resultSoftSide);
            CheckBackground(resultCard);
        }
        else if(side == CardTypeEnum.HARD && resultHardSide == String.Empty)
        {
            resultHardSide = currentChoice;
            finalPanel.GetComponent<FinalPageController>().cardDescHardFinal = card.GetComponent<CardDesc>();
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<ResultController>().SetHardSide(resultHardSide);
            CheckBackground(resultCard);
        }
    }
    
    public void CloseResultPanel() //Closing result panel and resetting the cards
    {
        if (resultHardSide == String.Empty || resultSoftSide == String.Empty)
        {
            StartCoroutine(ContinuePath());
        }
        else
        {
            gameObject.SetActive(false);
            resultPanel.SetActive(false);
            finalPanel.SetActive(true);
        }
    }
    
    IEnumerator ContinuePath() //Continues path after you have completed the first side (e.g. you chose soft first, reset the cards to display hard side only
    {
        resultPanel.GetComponentInChildren<Animator>().SetBool("SwipeOut", true);
        yield return new WaitForSeconds(1.5f);
        resultPanel.SetActive(false);
        texture.Release();
        Destroy(currentCard1);
        Destroy(currentCard2);
        card1Names.Clear();
        card2Names.Clear();
        if (side == CardTypeEnum.SOFT)
        {
            spawnCard2Location.SetActive(true);
            spawnCard2Location.GetComponent<Animator>().SetTrigger("ContinuePathTrigger");
        }

        if (side == CardTypeEnum.HARD)
        {
            spawnCard1Location.SetActive(true);
            spawnCard1Location.GetComponent<Animator>().SetTrigger("ContinuePathTrigger");
        }
    }
    
    public void ResetSidesCompletely() //Method used to reset both sides
    {
        resultPanel.SetActive(false);
        Destroy(currentCard1);
        Destroy(currentCard2);
        spawnCard1Location.SetActive(true);
        spawnCard2Location.SetActive(true);
        card1Names.Clear();
        card2Names.Clear();
        resultSoftSide = String.Empty;
        resultHardSide = String.Empty;
        side = CardTypeEnum.NONE;
        isPlayingVideo = false;
        isInfoPanelOpen = false;
        interactive = false;
    }

    private void CheckBackground(GameObject card) //Based on the current side, check the sprite of a specific card
    {
        if (side == CardTypeEnum.SOFT)
        {
            card.GetComponent<Image>().sprite = softSideBackground;
            if (card.TryGetComponent(out CardDesc cardDesc))
            {
                cardDesc.SetSoftSide();
            }
            else
            {
                Debug.Log("CardDesc component not found!");
            }

        }

        if (side == CardTypeEnum.HARD)
        {
            card.GetComponent<Image>().sprite = hardSideBackground;
            if (card.TryGetComponent(out CardDesc cardDesc))
            {
                cardDesc.SetHardSide();
            }
            else
            {
                Debug.Log("CardDesc component not found!");
            }
        }
    }

    public void GoBack()
    {
        interactive = false;
        AudioManager.Instance.PlayButtonClick();
        spawnCard1Location.GetComponent<Animator>().SetTrigger("SwipeInTrigger");
        spawnCard2Location.GetComponent<Animator>().SetTrigger("SwipeInTrigger");
        StartCoroutine(RollBack());
    }
    
    IEnumerator RollBack() //Rollback - pressing a button will enable you to go back to the previous choice - does not work for sides!
    {
        yield return new WaitForSeconds(1.2f);
        // interactive = true;
        if (card1Names.Count >= 1 && card2Names.Count >= 1)
        {
            Destroy(currentCard1);
            Destroy(currentCard2);
            GameObject card1;
            GameObject card2;
            foreach (GameObject card in cards)
            {
                if (card.GetComponent<CardDesc>().CardName() == card1Names[card1Names.Count - 1])
                {
                    card1 = Instantiate(card, spawnCard1Location.transform.position, spawnCard1Location.transform.rotation,
                        spawnCard1Location.transform);
                    MakeCardPickable(card1);
                    CheckBackground(card1);
                    currentCard1 = card1;
                    card1Names.Remove(card1Names[card1Names.Count - 1]);
                    break;
                }
            }
            foreach (GameObject card in cards)
            {
                if (card.GetComponent<CardDesc>().CardName() == card2Names[card2Names.Count - 1])
                {
                    card2 = Instantiate(card, spawnCard2Location.transform.position, spawnCard2Location.transform.rotation,
                        spawnCard2Location.transform);
                    MakeCardPickable(card2);
                    CheckBackground(card2);
                    currentCard2 = card2;
                    card2Names.Remove(card2Names[card2Names.Count - 1]);
                    break;
                }
            }
        }
    }

    public void ShowInfoText(GameObject card)
    {
        if (interactive && !isPlayingVideo)
        {
            interactive = false;
            isInfoPanelOpen = true;
            if (ReferenceEquals(card, currentCard1))
            {
                spawnCard2Location.SetActive(false);
                spawnCard1Location.GetComponent<Animator>().SetBool("SetToLarge", true);
            }
            else if (ReferenceEquals(card, currentCard2))
            {
                spawnCard1Location.SetActive(false);
                spawnCard2Location.GetComponent<Animator>().SetBool("SetToLarge", true);
            }
        }
    }

    public void CloseInfoText()
    {
        if (!isPlayingVideo)
        {
            interactive = true;
            isInfoPanelOpen = false;
            spawnCard1Location.GetComponent<Animator>().SetBool("SetToLarge", false);
            spawnCard2Location.GetComponent<Animator>().SetBool("SetToLarge", false);
            spawnCard1Location.SetActive(true);
            spawnCard2Location.SetActive(true);
        }
    }
}
