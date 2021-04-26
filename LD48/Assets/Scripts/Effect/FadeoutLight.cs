using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FadeoutLight : MonoBehaviour
{
    [SerializeField]
    private float duration = 1.0f;

    private float timer;
    private float initialIntensity;
    private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        initialIntensity = light.intensity;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var t = (Time.time - timer) / duration;
        if (t >= 0.0f && t <= 1.0f)
        {
            light.intensity = Mathf.Lerp(initialIntensity, 0.0f, t);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
