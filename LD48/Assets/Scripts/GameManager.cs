using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerControls player;
    private int money = 1000;

    public float PlayerDepth { get { return player.Submarine.Depth; } }

    public static GameManager main;

    private void Awake() {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //UIPopupManager.main.ShowPopup("Hi", "Who are you?");
        player = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundPlayer.main.PlaySound(GameSoundType.CreepySubmarineSound);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SoundPlayer.main.PlaySound(GameSoundType.Whale);
        }
    }

    public void UpgradeHealth(float value)
    {
        player.UpgradeMaxHealth(value);
    }

    public int GetMoney()
    {
        return money;
    }

    public void ReduceMoney(int amount)
    {
        money -= amount;
    }

    public void AddMoney(int amount)
    {
        money -= amount;
    }

    public void Upgrade(float value, UpgradeType type)
    {
        Debug.Log("Upgrading " + type.ToString() + " with this much: " + value);
        switch (type)
        {
            case UpgradeType.movementSpeed:
                player.UpgradeMovementSpeed(value);
                break;
            case UpgradeType.attackSpeed:
                player.UpgradeAttackSpeed(value);
                break;
            case UpgradeType.damage:
                player.UpgradeDamage(value);
                break;
            case UpgradeType.health:
                player.UpgradeMaxHealth(value);
                break;
            case UpgradeType.lights:
                player.UpgradeLights(value);
                break;
            default:
                Debug.Log("Unknown UpgradeType found! " + type.ToString());
                break;
        }
    }
}
