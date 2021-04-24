using UnityEngine;

[CreateAssetMenu(fileName = "ChestConfig", menuName = "Configs/New ChestConfig")]
public class ChestConfig : ScriptableObject
{
    public HealthConfig HealthConfig;
    public LootConfig LootConfig;
}