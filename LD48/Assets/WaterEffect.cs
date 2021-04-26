using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    [SerializeField]
    private bool SetMainCameraLayerMask;
    [SerializeField]
    private Camera renderCamera;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private RenderTexture renderTexture;

    [SerializeField]
    private Material renderMaterial;

    private RenderTexture currentRenderTexture;

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

    }

    void Start()
    {
        currentRenderTexture = Instantiate(renderTexture);
        currentRenderTexture.width = Screen.width;
        currentRenderTexture.height = Screen.height;
        Debug.Log($"Setting up renderTexture with size: {currentRenderTexture.width}, {currentRenderTexture.height}");
        renderCamera.targetTexture = currentRenderTexture;
        renderMaterial.SetTexture("_rt", currentRenderTexture);
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
        int newScreenWidth = Screen.width;
        int newScreenHeight = Screen.height;
        if (newScreenWidth != currentRenderTexture.width || newScreenHeight != currentRenderTexture.height) {
            Debug.Log($"Changed screen width from {currentRenderTexture.width} to {newScreenWidth}");
            Debug.Log($"Changed screen height from {renderTexture.height} to {newScreenHeight}");
            currentRenderTexture = Instantiate(renderTexture);
            currentRenderTexture.height = newScreenHeight;
            currentRenderTexture.width = newScreenWidth;
            renderCamera.targetTexture = currentRenderTexture;
            renderMaterial.SetTexture("_rt", currentRenderTexture);
        }

    }
}
