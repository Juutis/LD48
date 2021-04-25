using UnityEngine;

[CreateAssetMenu(fileName = "FishConfig", menuName = "Configs/New FishConfig")]
public class FishConfig : ScriptableObject
{
    public HealthConfig HealthConfig;
    public LootConfig LootConfig;
    public bool IsJelly;
    public float MoveSpeed;
    public float MinDelayBetweenMoving = 0.0f;
    public float MaxDelayBetweenMoving = 0.0f;
    public float ChangeDirectionChance = 0.25f;
    public float DamageOnTouch = 1.0f;
    public bool IsAggressive = false;
    public float AggroRange = 3.0f;
    public float AnimationSpeed = 1.0f;
    public float AggroSpeedScaling = 2.0f;
}