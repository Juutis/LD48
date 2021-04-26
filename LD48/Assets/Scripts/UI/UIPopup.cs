using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour
{
    private Animator animator;

    private bool showing = false;
    private bool hiding = false;

    private Text txtTitle;
    private Text txtMessage;

    private UIPopupManager manager;

    public void Init(Transform container, UIPopupManager manager)
    {
        this.manager = manager;
        txtTitle = this.FindChildObject("titleText").GetComponent<Text>();
        txtMessage = this.FindChildObject("messageText").GetComponent<Text>();
        transform.SetParent(container, false);
        animator = GetComponent<Animator>();
    }

    public void Show(string title, string message)
    {
        if (!showing)
        {
            Time.timeScale = 0f;
            showing = true;
            txtTitle.text = title;
            txtMessage.text = message;
            animator.SetTrigger("Show");
        }
    }

    public void ShowFinished()
    {
        showing = false;
        manager.ShowFinished();
    }

    public void Hide()
    {
        if (!hiding)
        {
            hiding = true;
            animator.SetTrigger("Hide");
        }
    }

    public void HideFinished()
    {
        Time.timeScale = 1f;
        hiding = false;
        manager.HideFinished();
    }
}
