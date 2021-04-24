using UnityEngine;

[CreateAssetMenu(fileName = "FishConfig", menuName = "Configs/New FishConfig")]
public class FishConfig : ScriptableObject
{
    public HealthConfig HealthConfig;
    public LootConfig LootConfig;
    public float MoveSpeed;
    public float ChangeDirectionChance = 0.25f;
    public float DamageOnTouch = 1.0f;
    public bool IsAggressive = false;
    public float AggroRange = 3.0f;
    public float AggroSpeedScaling = 2.0f;
}