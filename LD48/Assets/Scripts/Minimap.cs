using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    private Texture2D fogTex;
    private Texture2D runtimeTex;
    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private Transform mainCamera;
    private Submarine sub;

    // Start is called before the first frame update
    void Start()
    {
        runtimeTex = new Texture2D(fogTex.width, fogTex.height);
        runtimeTex.SetPixels(fogTex.GetPixels());
        runtimeTex.Apply();
        rawImage.texture = runtimeTex;
    }

    // Update is called once per frame
    void Update()
    {
        if (sub == null)
        {
            sub = FindObjectOfType<Submarine>();
        }
        else if (sub != null)
        {
            Vector3 cameraPos = mainCamera.transform.position;
            Vector3 playerPos = sub.transform.position;
            int x = Mathf.RoundToInt(playerPos.x*1.72f) + 512 + 48;
            int y = Mathf.RoundToInt(playerPos.y*1.72f) + 512 + 48;
            Debug.Log("X: " + x + "; Y: " + y);
            runtimeTex = DrawCircle(runtimeTex, Color.clear, x, y, 30);
            runtimeTex.Apply();
            rawImage.texture = runtimeTex;
            Rect uv = rawImage.uvRect;
            uv.x = 0.5f + cameraPos.x / 600f;
            uv.y = 0.5f + cameraPos.y / 600f;
            rawImage.uvRect = uv;
        }
    }

    private Texture2D DrawCircle(Texture2D tex, Color color, int x, int y, int radius = 3)
    {
        float rSquared = radius * radius;

        for (int u = x - radius; u < x + radius + 1; u++)
            for (int v = y - radius; v < y + radius + 1; v++)
                if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                    tex.SetPixel(u, v, color);

        return tex;
    }
}
