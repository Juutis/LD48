using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    private LootConfig config;

    private bool dropped = false;

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
        if (dropped)
        {
            return;
        }

        var coins = Random.Range(config.MinCoins, config.MaxCoins + 1);
        Debug.Log("Dropping coins: " + coins);
        for (var i = 0; i < coins; i++)
        {
            var coin = Prefabs.Get<Coin>();
            coin.transform.position = transform.position;
        }

        var hearts = Random.Range(config.MinHearts, config.MaxHearts + 1);
        for (var i = 0; i < hearts; i++)
        {
            var heart = Prefabs.Get<Heart>();
            heart.transform.position = transform.position;
        }

        dropped = true;
    }
}
