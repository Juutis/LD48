using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerControls player;
    private int money = 0;

    public float PlayerDepth { get { return player.Submarine.Depth; } }

    public static GameManager main;


    public bool FirstHitNormal = true;
    public bool FirstHitMedium = true;

    private void Awake() {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        UIPopupManager.main.ShowPopup(
            "Sunken Cost",
            "You are a small-time submarine enterpreneur. " +
            "Recently the waters have become more and more dangerous and the locals are scared of going into the water. "+
            "You are at risk of going bankrupt as no customers wish to board your vessel, " +
            "so you decide to get to the bottom of this troubling situation..."
        );
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetHealth()
    {
        return player.GetHealth();
    }
    public void Heal(float amount)
    {
        player.Heal(amount);
    }

    public float GetMaxHealth()
    {
        return player.GetMaxHealth();
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
        money += amount;
    }

    public float GetHeadlight()
    {
        return player.GetHeadlight();
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
            case UpgradeType.homingTorpedos:
                player.EnableHomingTorpedos();
                break;
            default:
                Debug.Log("Unknown UpgradeType found! " + type.ToString());
                break;
        }
    }
}
