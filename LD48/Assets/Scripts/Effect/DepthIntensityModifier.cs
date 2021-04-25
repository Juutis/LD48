using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DepthIntensityModifier : MonoBehaviour
{
    public float maxDepth = 10.0f;

    private Light2D light;
    private Vector2 startPosition;
    private float startIntensity;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        light = GetComponent<Light2D>();
        startIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        var t = (startPosition.y - transform.position.y) / maxDepth;
        t = Mathf.Log10((t + 0.2f) * 5);
        var intensity = Mathf.Lerp(startIntensity, 0, t);
        light.intensity = intensity;
        Debug.Log(intensity);
    }
}
