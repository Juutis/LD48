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
        PlayHullBreakingSounds();
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
            .Where(damage => playerMaxHealth < damage.HealthBelow && damage.Depth <= playerDepth)
            .OrderByDescending(damage => damage.Depth)
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

    private DepthDamage FindRelevantDepthDamage()
    {
        float playerDepth = GameManager.main.PlayerDepth;
        float playerMaxHealth = playerHurtable.GetMaxHealth();
        return upgradeConfig.DepthDamages
            .Where(damage => playerMaxHealth < damage.HealthBelow)
            .OrderBy(damage => damage.Depth)
            .FirstOrDefault();
    }

    private float soundThreshold = 0.75f;
    private float minSoundDelay = 1.5f;
    private float maxSoundDelay = 10.0f;

    private float soundTimer = 0.0f;

    [SerializeField]
    private GameSoundType breakingSound;

    private void PlayHullBreakingSounds()
    {
        var depthDamage = FindRelevantDepthDamage();
        if (depthDamage != null)
        {
            var depthPercentage = GameManager.main.PlayerDepth / depthDamage.Depth;
            if (depthPercentage > soundThreshold)
            {
                var t = (depthPercentage - soundThreshold) / (1 - soundThreshold);
                var soundDelay = Mathf.Lerp(maxSoundDelay, minSoundDelay, t);
                if (soundTimer <= Time.time - soundDelay)
                {
                    soundTimer = Time.time;
                    SoundPlayer.main.PlaySound(breakingSound);
                }
            }
        }
    }
}
