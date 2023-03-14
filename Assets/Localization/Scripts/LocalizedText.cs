using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    public class LocalizedText : MonoBehaviour
    {
        public string LocalizationKey;
        public bool isCapital = true;

        public void Awake()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            if (isCapital)
            {
                GetComponent<TextMeshProUGUI>().text= LocalizationManager.Localize(LocalizationKey).ToUpper();
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = LocalizationManager.Localize(LocalizationKey);
            }
        }
    }
}