using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/New PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public HealthConfig HealthConfig;
    public float AccelerationSpeed = 10.0f;
    public float RotationSpeed = 180.0f;
}