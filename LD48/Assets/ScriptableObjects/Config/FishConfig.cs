using UnityEngine;

[CreateAssetMenu(fileName = "FishConfig", menuName = "Configs/New FishConfig")]
public class FishConfig : ScriptableObject
{
    public HealthConfig HealthConfig;
    public float MoveSpeed;
}