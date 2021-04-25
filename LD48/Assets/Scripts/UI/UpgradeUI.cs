using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject openButton;
    [SerializeField]
    private GameObject upgradeUI;
    private bool open = false;
    [SerializeField]
    private Text moneyText;

    List<UpgradeStack> upgradeStacks;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        upgradeStacks = GetComponentsInChildren<UpgradeStack>(true).ToList();
        foreach (var stack in upgradeStacks)
        {
            Debug.Log(this);
            stack.SetUpgradeUI(this);
        }

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        openButton.SetActive(!open);
        upgradeUI.SetActive(open);
        moneyText.text = string.Format("{0:D6}", GetMoney());
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
