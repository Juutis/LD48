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
        moneyText.text = string.Format("{0:D6}", GetMoney());
    }

    public void HideToggle() {
        openButton.SetActive(false);
    }

    public void ShowToggle() {
        openButton.SetActive(true);
    }

    public void Close()
    {
        Time.timeScale = 1f;
        open = false;
        upgradeUI.SetActive(open);
        openButton.SetActive(!open);
        UIPopupManager.main.UpgradeUIWasClosed();
    }

    public void Open()
    {
        Time.timeScale = 0f;
        open = true;
        upgradeUI.SetActive(open);
        openButton.SetActive(!open);
        UIPopupManager.main.UpgradeUIWasOpened();
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
