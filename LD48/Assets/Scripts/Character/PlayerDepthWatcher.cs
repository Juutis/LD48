using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDepthWatcher : MonoBehaviour
{
    [SerializeField]
    private UpgradeConfig upgradeConfig;
    [SerializeField]
    private Hurtable playerHurtable;

    private DepthDamage currentDepthDamage;

    private List<DepthDamage> seenDamages = new List<DepthDamage>();
    private float damageTimer = 0f;
    void Update() {
        FindCurrentDepthDamage();
        ApplyCurrentDepthDamage();
    }

    private void ApplyCurrentDepthDamage() {
        if (currentDepthDamage != null) {
            if (!seenDamages.Contains(currentDepthDamage)) {
                UIPopupManager.main.ShowPopup("Going too deep", currentDepthDamage.Message);
                seenDamages.Add(currentDepthDamage);
            }
            damageTimer += Time.deltaTime;
            if (damageTimer > currentDepthDamage.DamageInterval) {
                //Debug.Log($"Depthdmg: [{currentDepthDamage.DamageTaken}] player vs depthDmg -> ({GameManager.main.PlayerDepth} > {currentDepthDamage.Depth}) playerHp vs depthBelowHp -> ({playerHurtable.GetMaxHealth()} < {currentDepthDamage.HealthBelow})");
                playerHurtable.Hurt(currentDepthDamage.DamageTaken);
                damageTimer = 0f;
            }
        }
    }

    private void FindCurrentDepthDamage() {
        float playerDepth = GameManager.main.PlayerDepth;
        float playerMaxHealth = playerHurtable.GetMaxHealth();
        DepthDamage depthDamage = upgradeConfig.DepthDamages
            .Where(depthDamage => playerMaxHealth < depthDamage.HealthBelow && depthDamage.Depth <= playerDepth)
            .OrderByDescending(depthDamage => depthDamage.Depth)
            .FirstOrDefault();
        if (depthDamage != null) {
            currentDepthDamage = depthDamage;
            HUDUI.main.StartPressureWarning();
        } else {
            HUDUI.main.StopPressureWarning();
            currentDepthDamage = null;
            damageTimer = 0f;
        }
    }
}
