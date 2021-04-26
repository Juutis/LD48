using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "FishSpawnConfig", menuName = "Config/FishSpawnConfig")]
public class FishSpawnConfig : ScriptableObject
{
    [field: SerializeField]
    public List<FishSpawn> Spawns {get; private set;}

    public void Init() {
        foreach(FishSpawn spawn in Spawns) {
            spawn.SpawnedCount = 0;
            spawn.RespawnTimer = 0;
        }
    }

}

[System.Serializable]
public class FishSpawn
{
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    [field: Range(5, 200)]
    public float RespawnInterval { get; private set; }

    [SerializeField]
    [Range(0, 50)]
    private int amount = 5;
    public int Amount { get { return amount; } }

    [HideInInspector]
    public int SpawnedCount = 0;

    [HideInInspector]
    public float RespawnTimer = 0;
}
