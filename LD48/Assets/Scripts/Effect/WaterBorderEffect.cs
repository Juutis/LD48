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
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float screenHeight = Camera.main.orthographicSize * 2.0f;
        float screenWidth = screenHeight * Screen.width / Screen.height;
        float aspectRatio = Screen.width / Screen.height;
        float paddingX = Screen.width / 1000;
        float paddingY = paddingX * aspectRatio;
        transform.localScale = new Vector2(
            screenWidth / spriteRenderer.bounds.size.x + paddingX,
            height
        );
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
