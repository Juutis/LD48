using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWarningBlinker : MonoBehaviour
{
    [SerializeField]
    private Image imgBlinker;

    private bool isBlinking = false;

    private float blinkTimer = 0f;
    [SerializeField]
    private float blinkDuration = 0.5f;

    [SerializeField]
    private Color blinkColor = Color.red;
    private Color startColor;

    private void Start() {
        startColor = imgBlinker.color;
    }

    public void StartBlinking() {
        if (!isBlinking) {
            isBlinking = true;
            blinkTimer = 0f;
        }
    }
    public void StopBlinking() {
        if (isBlinking) {
            imgBlinker.color = startColor;
            isBlinking = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlinking) {
            blinkTimer += Time.deltaTime;
            imgBlinker.color = Color.Lerp(startColor, blinkColor, blinkTimer / blinkDuration);
            if (blinkTimer > blinkDuration) {
                blinkTimer = 0f;
                imgBlinker.color = blinkColor;
            }
        }
    }
}
