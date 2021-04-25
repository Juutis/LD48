using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteScaler : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        float aspectRatio = Screen.width / Screen.height;
        float paddingX = Screen.width / 1000;
        float paddingY = paddingX * aspectRatio;
        transform.localScale = new Vector2(
            width / spriteRenderer.bounds.size.x + paddingX,
            height / spriteRenderer.bounds.size.y + paddingY
        );

    }
}
