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
    
    public void UpdateBasedOnResolution() {
        Debug.Log("proo");
        currentScreenWidth = Screen.width;
        currentScreenHeight = Screen.height;
        Debug.Log($"Screen.width {Screen.width}");
        Debug.Log($"Screen.height {Screen.height}");
        Debug.Log($"currentScreenWidth {currentScreenWidth}");
        Debug.Log($"currentScreenHeight {currentScreenHeight}");
        Debug.Log("three");
        float height = Camera.main.orthographicSize * 2.0f;
        Debug.Log($"height: {height}");
        float aspectRatio = (Screen.width * 1.0f) / (Screen.height * 1.0f);
        Debug.Log($"aspectRatio: {aspectRatio}");
        float width = height / Screen.height * Screen.width;
        Debug.Log($"width: {width}");
        //float paddingX = Screen.width / 1000;
        //float paddingY = paddingX * aspectRatio;
        Debug.Log($"spriteRenderer.sprite.bounds.size.x: {spriteRenderer.sprite.bounds.size.x}");
        Debug.Log($"spriteRenderer.sprite.bounds.size.y: {spriteRenderer.sprite.bounds.size.y}");
        transform.localScale = new Vector2(
            width / spriteRenderer.sprite.bounds.size.x, //+ paddingX,
            height / spriteRenderer.sprite.bounds.size.y //+ paddingY
        );
        Debug.Log($"({aspectRatio}) {currentScreenWidth} -> {width} && {currentScreenHeight} -> {height}");
        Debug.Log($"boundsX -> {spriteRenderer.sprite.bounds.size.x} && boundsY -> {spriteRenderer.sprite.bounds.size.y}");
    }


    // Update is called once per frame
    /*void Update()
    {
        Debug.Log($"SpriteScaler Screen {currentScreenWidth}, {currentScreenHeight} -> {Screen.width}, {Screen.height}");
        if (Screen.width != currentScreenWidth || Screen.height != currentScreenHeight) {
            Debug.Log($"Updated SpriteScaler from {currentScreenWidth}, {currentScreenHeight} -> {Screen.width}, {Screen.height}");
            UpdateBasedOnResolution();
        }
    }*/
}
