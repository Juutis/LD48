using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitch : MonoBehaviour
{


    [SerializeField]
    private string title;

    [SerializeField]
    private Sprite onSprite;

    [SerializeField]
    private string onText;

    [SerializeField]
    private Color onColor = Color.green;

    [SerializeField]
    private Sprite offSprite;

    [SerializeField]
    private string offText;

    [SerializeField]
    private Color offColor = Color.red;

    [SerializeField]
    private Image imgSwitch;

    [SerializeField]
    private Text txtSwitch;

    private bool status;

    public void Init(bool status) {
        this.status = status;
        UpdateBasedOnStatus();
    }

    public void Toggle() {
        status = !status;
        UpdateBasedOnStatus();
        SoundPlayer.main.PlaySound(GameSoundType.ShopOK);
    }

    public void UpdateBasedOnStatus() {
        imgSwitch.sprite = status ? onSprite : offSprite;
        string color = status ? ColorUtility.ToHtmlStringRGB( onColor ) : ColorUtility.ToHtmlStringRGB(offColor);
        txtSwitch.text = $"{title}: <color='#{color}'>{(status ? onText : offText)}</color>";
    }

}
