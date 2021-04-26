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
    }

    // Update is called once per frame
    public void Toggle()
    {
        effectEnabled = !effectEnabled;
        container.gameObject.SetActive(effectEnabled);
    }
}
