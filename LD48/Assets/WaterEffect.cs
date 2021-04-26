using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    [SerializeField]
    private bool SetMainCameraLayerMask;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private RenderTexture renderTexture;

    [SerializeField]
    private Transform container;

    [SerializeField]
    private bool effectEnabled = true;
    public bool Status { get { return effectEnabled; } }


    [SerializeField]
    private LayerMask optimizedLayerMask;
    private LayerMask origLayerMask;
    public bool Optimize = false;

    void Awake()
    {
        renderTexture.width = Screen.width;
        renderTexture.height = Screen.height;
    }
    void Start()
    {
        if (SetMainCameraLayerMask)
        {
            Camera.main.cullingMask = layerMask.value;
        }
        container.gameObject.SetActive(effectEnabled);

        origLayerMask = Camera.main.cullingMask;
    }

    // Update is called once per frame
    public void Toggle()
    {
        effectEnabled = !effectEnabled;
        container.gameObject.SetActive(effectEnabled);
    }

    public void Update()
    {
        // hax optimization
        if (Optimize)
        {
            var camera = Camera.main;
            if (effectEnabled && camera.transform.position.y < -20)
            {
                camera.cullingMask = optimizedLayerMask;
            }
            else
            {
                camera.cullingMask = origLayerMask;
            }
        }
    }
}
