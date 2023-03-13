using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ViewType
{
    MainMenu,
    Introduction,
    Game,
    Video,
    EndResults
}

public class ViewManager : MonoBehaviour
{
    //Holds this instance.
    public static ViewManager Instance { get; private set; }

    List<ViewController> viewControllerList;
    ViewController lastActiveView;
    [SerializeField] private ViewType openingViewType;

    internal static ViewManager GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        //Creates one single instance and destroys all future instances. (Singleton)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        viewControllerList = GetComponentsInChildren<ViewController>().ToList();
        viewControllerList.ForEach(x => x.gameObject.SetActive(false));
        SwitchView(openingViewType); //You can set the opening view here
    }

    public void SwitchView(ViewType _type)
    {
        if (lastActiveView != null)
        {
            lastActiveView.gameObject.SetActive(false);
        }

        ViewController desiredView = viewControllerList.Find(x => x.viewType == _type);
        if (desiredView != null)
        {
            desiredView.gameObject.SetActive(true);
            lastActiveView = desiredView;
        }
        else { Debug.LogWarning("The desired view was not found!"); }
    }
    
   
}