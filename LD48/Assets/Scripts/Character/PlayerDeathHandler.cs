using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Submarine))]
[RequireComponent(typeof(Hurtable))]
public class PlayerDeathHandler : MonoBehaviour
{
    Submarine submarine;
    Transform spawn;

    Hurtable hurtable;

    PlayerControls playerControls;
    void Start() {
        spawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        submarine = GetComponent<Submarine>();
        hurtable = GetComponent<Hurtable>();
        playerControls = FindObjectOfType<PlayerControls>();
    }
    public void HandleDeath() {
        int money = GameManager.main.GetMoney();
        Time.timeScale = 0f;
        playerControls.DisableControls();
        string moneyString = $"Your upgrades are intact but the rescue operation and repairs cost all ${money} of your cash!";
        if (money == 0) {
            moneyString = "Your submarine was repaired for free by the kind mechanic who took pity on your beggarlike monetary situation. The rescue team, unpaid, is baffled how one can maintain a submarine without any cash.";
        }
        UIPopupManager.main.ShowPopup(
            "Hull integrity fail!",
            "The hull of your submarine failed because of your reckless diving.\n\n" +
            "Your emergency beacon went on and a rescue team managed to drag you back to surface.\n\n" +
            moneyString,
            delegate {
                submarine.transform.position = spawn.position;
                submarine.Stop();
                GameManager.main.ReduceMoney(money);
                hurtable.HealToFull();
                Camera.main.transform.position = new Vector3(spawn.position.x, spawn.position.y, Camera.main.transform.position.z);
            },
            delegate {
                playerControls.EnableControls();
            }
        );
    }
}
