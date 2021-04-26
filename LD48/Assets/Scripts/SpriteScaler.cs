using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteScaler : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

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
        float height = Camera.main.orthographicSize * 2.0f;
        float aspectRatio = (Screen.width * 1.0f) / (Screen.height * 1.0f);
        float width = height / Screen.height * Screen.width;
        //float paddingX = Screen.width / 1000;
        //float paddingY = paddingX * aspectRatio;
        transform.localScale = new Vector2(
            width / spriteRenderer.sprite.bounds.size.x, //+ paddingX,
            height / spriteRenderer.sprite.bounds.size.y //+ paddingY
        );
        Debug.Log($"({aspectRatio}) {currentScreenWidth} -> {width} && {currentScreenHeight} -> {height}");
        Debug.Log($"boundsX -> {spriteRenderer.sprite.bounds.size.x} && boundsY -> {spriteRenderer.sprite.bounds.size.y}");
    }


    // Update is called once per frame
    void Update()
    {
        if (Screen.width != currentScreenWidth || Screen.height != currentScreenHeight) {
            Debug.Log($"Updated SpriteScaler from {currentScreenWidth}, {currentScreenHeight} -> {Screen.width}, {Screen.height}");
            UpdateBasedOnResolution();
        }
    }
}
