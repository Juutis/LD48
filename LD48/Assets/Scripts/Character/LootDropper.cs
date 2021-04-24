using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    private LootConfig config;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(LootConfig config)
    {
        this.config = config;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropLoot()
    {
        var coins = Random.Range(config.MinCoins, config.MaxCoins + 1);
        for (var i = 0; i < coins; i++)
        {
            var coin = Prefabs.Get<Coin>();
            coin.transform.position = transform.position;
        }
    }
}
