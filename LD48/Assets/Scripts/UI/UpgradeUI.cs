using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject openButton;
    [SerializeField]
    private GameObject upgradeUI;
    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        openButton.SetActive(!open);
        upgradeUI.SetActive(open);
    }

    public void Close()
    {
        open = false;
    }

    public void Open()
    {
        open = true;
    }
}
