using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffectMask : MonoBehaviour
{
    [SerializeField]
    Transform target;
    float y;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    float height = 10;

    int currentScreenWidth;
    int currentScreenHeight;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateBasedOnResolution();
    }

    void UpdateBasedOnResolution() {
        currentScreenWidth = Screen.width;
        currentScreenHeight = Screen.height;
        float screenHeight = Camera.main.orthographicSize * 2.0f;
        float aspectRatio = (Screen.width * 1.0f) / (Screen.height * 1.0f);
        float screenWidth = screenHeight / Screen.height * Screen.width;
        float paddingX = Screen.width / 1000;
        float paddingY = paddingX * aspectRatio;
        transform.localScale = new Vector2(
            screenWidth / spriteRenderer.sprite.bounds.size.x + paddingX,
            height
        );
    }


    // Update is called once per frame
    void Update()
    {
        if (Screen.width != currentScreenWidth || Screen.height != currentScreenHeight) {
            Debug.Log($"Updated waterEffectMask from {currentScreenWidth}, {currentScreenHeight} -> {Screen.width}, {Screen.height}");
            UpdateBasedOnResolution();
        }
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
}
