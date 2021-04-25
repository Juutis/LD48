using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject openButton;
    [SerializeField]
    private GameObject upgradeUI;
    private bool open = false;

    List<UpgradeStack> upgradeStacks;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        upgradeStacks = GetComponentsInChildren<UpgradeStack>().ToList();
        foreach (var stack in upgradeStacks)
        {
            stack.SetUpgradeUI(this);
        }

        gameManager = FindObjectOfType<GameManager>();
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

    public int GetMoney()
    {
        return gameManager.GetMoney();
    }

    public void ReduceMoney(int amount)
    {
        gameManager.ReduceMoney(amount);
    }

    public void Upgrade(float value, UpgradeType type)
    {
        gameManager.Upgrade(value, type);
    }
}
