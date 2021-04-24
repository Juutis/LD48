using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupManager : MonoBehaviour
{
    public static UIPopupManager main;
    void Awake() {
        main = this;
        popupContainer = this.FindChildObject("container").GetComponent<Transform>();
    }

    private Transform popupContainer;

    void Start() {
        
    }

    public void ShowPopup(string title, string message) {
        UIPopup uiPopup = Prefabs.Get<UIPopup>();
        uiPopup.Init(popupContainer);
        uiPopup.Show(title, message);
    }
}
