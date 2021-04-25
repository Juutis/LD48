using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDepthDisplay : MonoBehaviour
{
    [SerializeField]
    private Text txtDepthDisplay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtDepthDisplay.text = GameManager.main.PlayerDepth.ToString();
    }
}
