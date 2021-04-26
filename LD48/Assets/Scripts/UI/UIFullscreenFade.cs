using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public delegate void DoneCallback();

public class UIFullscreenFade : MonoBehaviour
{
    [SerializeField]
    private Image imgFade;

    [SerializeField]
    private float fadeInDuration = 0.5f;

    [SerializeField]
    private Color fadeInColor;

    [SerializeField]
    private float fadeOutDuration = 0.5f;

    [SerializeField]
    private Color fadeOutColor;

    private Color targetColor;

    private float timer = 0f;

    private bool fadeIn = false;
    private bool fading = false;

    private Color startColor;

    float duration;
    UnityAction currentCallback;

    public void FadeIn(UnityAction callback) {
        currentCallback = callback;
        targetColor = fadeInColor;
        fadeIn = true;
        fading = true;
        duration = fadeInDuration;
        startColor = imgFade.color;
    }
    public void FadeOut(UnityAction callback) {
        currentCallback = callback;
        targetColor = fadeOutColor;
        fadeIn = false;
        fading = true;
        duration = fadeOutDuration;
        startColor = imgFade.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (fading) {
            timer += Time.unscaledDeltaTime;
            imgFade.color = Color.Lerp(startColor, targetColor, timer / duration);
            if (timer > duration) {
                imgFade.color = targetColor;
                timer = 0f;
                fading = false;
                currentCallback.Invoke();
                currentCallback = null;
            }
        }
    }
}
