using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTicket : MonoBehaviour
{
    [SerializeField]
    Text lore;
    [SerializeField]
    Text description;
    [SerializeField]
    Text descriptionShadow;
    [SerializeField]
    Text priceUI;

    public void Initialize(string descriptionText, string loreText, float price)
    {
        lore.text = loreText;
        description.text = descriptionText;
        descriptionShadow.text = descriptionText;
        string specifier = "C";
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        priceUI.text = price.ToString(specifier, culture);

        // scale the size based on text length
        float heightCoef = Mathf.Max(1, 12f * (loreText.Length - 20f) / 10f);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float width = rectTransform.sizeDelta.x;
        float newHeight = rectTransform.sizeDelta.y + heightCoef;
        rectTransform.sizeDelta = new Vector2(width, newHeight);

        RectTransform loreTransform = lore.GetComponent<RectTransform>();
        loreTransform.sizeDelta = new Vector2(loreTransform.sizeDelta.x, loreTransform.sizeDelta.y + heightCoef);

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
