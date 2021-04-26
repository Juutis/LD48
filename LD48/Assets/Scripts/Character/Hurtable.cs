using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Hurtable : MonoBehaviour
{
    private HealthConfig config;

    [SerializeField]
    private UnityEvent<float> damagedCallback;

    [SerializeField]
    private UnityEvent deadAction;

    private float maxHealth;
    private float currentHealth;

    private bool invulnerable = false;
    private List<SpriteRenderer> spriteRenderers;
    private List<Color> origColors;

    private float damaged = -100f;

    public void Start()
    {
        spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        spriteRenderers.AddRange(GetComponents<SpriteRenderer>());
        origColors = spriteRenderers.Select(rend => rend.color).ToList();

        Initialize(config);
    }

    public void Initialize(HealthConfig healthConfig)
    {
        config = healthConfig;
        if (config != null)
        {
            currentHealth = config.MaxHealth;
            maxHealth = config.MaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        tint();
    }

    public void Hurt(float damage)
    {
        if (!invulnerable)
        {
            if (damagedCallback != null)
            {
                damagedCallback.Invoke(damage);
            }
            SoundPlayer.main.PlaySound(GameSoundType.SubmarineGetHit);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                if (config.InvulnerabilityDuration > 0.001f)
                {
                    invulnerable = true;
                    Invoke("DisableInvulnerability", config.InvulnerabilityDuration);
                }
                damaged = Time.time;
            }
        }
    }

    public void Heal(float health)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + health);
    }

    public void HealToFull() {
        currentHealth = maxHealth;
    }

    public void UpgradeMaxHealth(float upgrade)
    {
        maxHealth += upgrade;
    }

    public void Die() {
        if (deadAction != null)
        {
            SoundPlayer.main.PlaySound(GameSoundType.SubmarineCrash);
            deadAction.Invoke();
        }
    }

    public void DisableInvulnerability()
    {
        invulnerable = false;
    }

    public float GetHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    private void tint()
    {
        var t = (Time.time - damaged) / config.DamageTintDuration;
        if (t > 0.0f && t <= 1.0f)
        {
            var index = 0;
            foreach (var rend in spriteRenderers)
            {
                var origColor = origColors[index++];
                var color = Color.Lerp(config.DamageTintColor, origColor, t);
                rend.color = color;
            }
        }
        else
        {
            var index = 0;
            foreach (var rend in spriteRenderers)
            {
                var origColor = origColors[index++];
                rend.color = origColor;
            }
        }

    }

}
