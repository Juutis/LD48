using UnityEngine;

[CreateAssetMenu(fileName = "LootConfig", menuName = "Configs/New LootConfig")]
public class LootConfig : ScriptableObject
{
    public int MinCoins = 0;
    public int MaxCoins = 2;
    public int MinHearts = 0;
    public int MaxHearts = 1;
}