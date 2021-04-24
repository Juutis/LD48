using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteScaler : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private float GetScreenToWorldHeight()
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        var height = edgeVector.y * 2;
        return height;
    }

    private float GetScreenToWorldWidth()
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        var width = edgeVector.x * 2;
        return width;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        float screenWidth = GetScreenToWorldWidth();
        float screenHeight = GetScreenToWorldHeight();
        Sprite sprite = spriteRenderer.sprite;

        float spriteWidth = sprite.rect.width / sprite.pixelsPerUnit;
        float spriteHeight = sprite.rect.height / sprite.pixelsPerUnit;
        transform.localScale = new Vector2(screenWidth / spriteWidth, screenHeight / spriteHeight);

    }
}
