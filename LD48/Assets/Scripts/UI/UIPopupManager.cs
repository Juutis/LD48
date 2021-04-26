using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIPopupManager : MonoBehaviour
{
    public static UIPopupManager main;

    [SerializeField]

    private GameObject optionsButton;
    void Awake() {
        main = this;
        popupContainer = this.FindChildObject("container").GetComponent<Transform>();
        EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
        if (eventSystem == null) {
            Prefabs.Get<EventSystem>();
        }
    }

    [SerializeField]
    UpgradeUI upgradeUI;

    void Start() {
        //upgradeUI = GameObject.FindObjectOfType<UpgradeUI>();
    }

    private bool PauseMenuCanBeOpened = true;

    private Transform popupContainer;

    [SerializeField]
    private UIFullscreenFade fader;

    private bool canShowPopup = true;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q) && PauseMenuCanBeOpened) {
            ShowPauseMenu();
        }
    }

    private UnityAction afterFadeOut;
    private UIPauseMenu currentPauseMenu;

    public void UpgradeUIWasClosed() {
        PauseMenuCanBeOpened = true;
        optionsButton.SetActive(true);
        canShowPopup = true;
    }
    public void UpgradeUIWasOpened() {
        PauseMenuCanBeOpened = false;
        optionsButton.SetActive(false);
        canShowPopup = false;
    }

    public void ShowPopup(string title, string message) {
        if (!canShowPopup) {
            return;
        }
        PauseMenuCanBeOpened = false;
        optionsButton.SetActive(false);
        upgradeUI.HideToggle();
        fader.FadeIn(delegate {
            UIPopup uiPopup = Prefabs.Get<UIPopup>();
            uiPopup.Init(popupContainer, this);
            uiPopup.Show(title, message);
        });
    }
    public void ShowPopup(string title, string message, UnityAction afterFadeInCallback, UnityAction afterFadeOutCallback) {
        if (!canShowPopup) {
            return;
        }
        PauseMenuCanBeOpened = false;
        optionsButton.SetActive(false);
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

    public void ShowPauseMenu() {
        SoundPlayer.main.PlaySound(GameSoundType.ShopOK);
        if (!PauseMenuCanBeOpened) {
            return;
        }
        canShowPopup = false;
        optionsButton.SetActive(false);
        Time.timeScale = 0f;
        PauseMenuCanBeOpened = false;
        upgradeUI.HideToggle();
        fader.FadeIn(delegate {
            UIPauseMenu uiPauseMenu = Prefabs.Get<UIPauseMenu>();
            uiPauseMenu.Init(popupContainer, this);
            uiPauseMenu.Show("Options", "");
            currentPauseMenu = uiPauseMenu;
        });
    }

    public void HideFinished(UIPopup popup) {
        Destroy(popup.gameObject);
        if (currentPauseMenu != null) {
            Destroy(currentPauseMenu.gameObject);
            currentPauseMenu = null;
        }
        fader.FadeOut(delegate {
            if (afterFadeOut != null) {
                afterFadeOut.Invoke();
                afterFadeOut = null;
            }
            upgradeUI.ShowToggle();
            PauseMenuCanBeOpened = true;
            optionsButton.SetActive(true);
            canShowPopup = true;
        });
    }
}
