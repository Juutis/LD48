using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/New HealthConfig")]
public class HealthConfig : ScriptableObject
{
    public float MaxHealth = 10;
    public Color DamageTintColor = Color.red;
    public float InvulnerabilityDuration = 1.0f;
    public float DamageTintDuration = 1.0f;
}