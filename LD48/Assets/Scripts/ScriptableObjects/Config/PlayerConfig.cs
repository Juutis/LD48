using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/New PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public HealthConfig HealthConfig;
    public float AccelerationSpeed = 10.0f;
    public float RotationSpeed = 180.0f;
    public float AttackSpeed = 6.5f; // attacks per 10s
    public float Damage = 1f;
}