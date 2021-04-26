using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

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

    UpgradeUI upgradeUI;

    void Start() {
        upgradeUI = GameObject.FindObjectOfType<UpgradeUI>();
    }

    private Transform popupContainer;

    [SerializeField]
    private UIFullscreenFade fader;

    void Update() {
    }

    private UnityAction afterFadeOut;

    public void ShowPopup(string title, string message) {
        upgradeUI.HideToggle();
        fader.FadeIn(delegate {
            UIPopup uiPopup = Prefabs.Get<UIPopup>();
            uiPopup.Init(popupContainer, this);
            uiPopup.Show(title, message);
        });
    }
    public void ShowPopup(string title, string message, UnityAction afterFadeInCallback, UnityAction afterFadeOutCallback) {
        upgradeUI.HideToggle();
        afterFadeOut = afterFadeOutCallback;
        fader.FadeIn(delegate {
            UIPopup uiPopup = Prefabs.Get<UIPopup>();
            uiPopup.Init(popupContainer, this);
            uiPopup.Show(title, message);
            afterFadeInCallback.Invoke();
        });
    }
    public void ShowFinished() {
    }

    public void HideFinished() {
        fader.FadeOut(delegate {
            if (afterFadeOut != null) {
                afterFadeOut.Invoke();
                afterFadeOut = null;
            }
            upgradeUI.ShowToggle();
        });
    }
}
