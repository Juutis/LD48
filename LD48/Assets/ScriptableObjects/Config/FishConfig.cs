using UnityEngine;

[CreateAssetMenu(fileName = "FishConfig", menuName = "Configs/New FishConfig")]
public class FishConfig : ScriptableObject
{
    public HealthConfig HealthConfig;
    public LootConfig LootConfig;
    public float MoveSpeed;
    public float ChangeDirectionChance = 0.25f;
}