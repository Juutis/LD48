using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBorderEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float y;
    [SerializeField]
    Transform target;
    [SerializeField]
    float height = 1f;

    int currentScreenWidth;
    int currentScreenHeight;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateBasedOnResolution();
    }
    
    public void UpdateBasedOnResolution() {
        currentScreenWidth = Screen.width;
        currentScreenHeight = Screen.height;
        float screenHeight = Camera.main.orthographicSize * 2.0f;
        float aspectRatio = (Screen.width * 1.0f) / (Screen.height * 1.0f);
        float screenWidth = screenHeight / Screen.height * Screen.width;
        float paddingX = Screen.width / 1000;
        float paddingY = paddingX * aspectRatio;
        Debug.Log($"WaterBorderEffect: {screenWidth} / {spriteRenderer.sprite.bounds.size.x} + {paddingX}");
        transform.localScale = new Vector2(
            screenWidth + paddingX,
            height
        );
    }


    // Update is called once per frame
    void Update()
    {
        /*if (Screen.width != currentScreenWidth || Screen.height != currentScreenHeight) {
            Debug.Log($"Updated WaterBorderEffect from {currentScreenWidth}, {currentScreenHeight} -> {Screen.width}, {Screen.height}");
            UpdateBasedOnResolution();
        }*/
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
