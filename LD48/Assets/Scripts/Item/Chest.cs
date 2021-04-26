using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hurtable), typeof(LootDropper))]
public class Chest : MonoBehaviour
{
    [SerializeField]
    private ChestConfig chestConfig;

    private LootDropper lootDropper;

    // Start is called before the first frame update
    void Start()
    {
        lootDropper = GetComponent<LootDropper>();
        lootDropper.Initialize(chestConfig.LootConfig);

        GetComponent<Hurtable>().Initialize(chestConfig.HealthConfig);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        lootDropper.DropLoot();
        SoundPlayer.main.PlaySound(GameSoundType.OpenChest);
        Destroy(gameObject);
    }
}
