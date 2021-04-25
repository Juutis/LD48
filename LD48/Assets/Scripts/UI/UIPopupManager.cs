using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPopupManager : MonoBehaviour
{
    public static UIPopupManager main;
    void Awake() {
        main = this;
        popupContainer = this.FindChildObject("container").GetComponent<Transform>();
        EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
        if (eventSystem == null) {
            Prefabs.Get<EventSystem>();
        }
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
