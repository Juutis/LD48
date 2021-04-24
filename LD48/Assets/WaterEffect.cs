using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    [SerializeField]
    private bool SetMainCameraLayerMask;
    [SerializeField]
    private LayerMask layerMask;
    void Start()
    {
        if (SetMainCameraLayerMask) {
            Camera.main.cullingMask = layerMask.value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
