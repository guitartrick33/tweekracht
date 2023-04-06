using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTypeController : MonoBehaviour
{
    [SerializeField] private PageType pageType;

    public PageType GetPageType()
    {
        return pageType;
    }
}

public enum PageType
{
    MAIN,
    SETTINGS,
    PLAY,
    PROFILE,
    MOREINFO,
    RESULTS
}
