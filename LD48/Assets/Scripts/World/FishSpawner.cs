using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private FishSpawnConfig config;

    [SerializeField]
    private SpriteRenderer area;

    [SerializeField]
    private Transform fishContainer;

    private Vector3 min;
    private Vector3 max;

    private GameObject player;

    public void Start() {
        if (area == null || area.sprite == null) {
            Debug.Log($"Area must have a sprite! ({name})");
            return;
        }
        Renderer spriteR = area.GetComponent<Renderer>();
        config.Init();
        min = spriteR.bounds.min;
        max = spriteR.bounds.max;
        area.enabled = false;
        SpawnAllFish();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void FishDie(FishSpawn spawn) {
        spawn.SpawnedCount -= 1;
    }

    public void SpawnAllFish() {
        foreach(FishSpawn spawn in config.Spawns) {
            for(int index = 0; index < spawn.Amount; index += 1) {
                SpawnSingleFish(spawn);
            }
        }
    }

    public void SpawnSingleFish(FishSpawn spawn) {
        if (spawn.Prefab == null) {
            Debug.Log($"Spawn must have a prefab! ({name})");
            return;
        }

        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        Fish fish = Instantiate(spawn.Prefab).GetComponent<Fish>();
        fish.Init(this, spawn, fishContainer, new Vector2(x, y));
        spawn.SpawnedCount += 1;
        spawn.RespawnTimer = 0f;
    }

    public void Update() {
        if (area == null || area.sprite == null) {
            return;
        }
        foreach(FishSpawn spawn in config.Spawns) {
            if (spawn.SpawnedCount < spawn.Amount && PlayerIsFarEnough()) {
                spawn.RespawnTimer += Time.deltaTime;
                if (spawn.RespawnTimer > spawn.RespawnInterval) {
                    SpawnSingleFish(spawn);
                }
            }
        }
    }

    public bool PlayerIsFarEnough()
    {
        return Vector2.Distance(transform.position, player.transform.position) > 50.0f;
    }
}
