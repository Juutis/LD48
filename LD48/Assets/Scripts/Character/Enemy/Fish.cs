using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hurtable), typeof(LootDropper))]
public class Fish : MonoBehaviour
{
    [SerializeField]
    private FishConfig config;

    [SerializeField]
    private GameObject dieEffect;

    private Hurtable hurtable;
    private LootDropper lootDropper;

    

    // Start is called before the first frame update
    void Start()
    {
        hurtable = GetComponent<Hurtable>();
        hurtable.Initialize(config.HealthConfig);

        lootDropper = GetComponent<LootDropper>();
        lootDropper.Initialize(config.LootConfig);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        if (dieEffect != null)
        {
            var effect = Instantiate(dieEffect);
            effect.transform.position = transform.position;
        }
        lootDropper.DropLoot();
        Destroy(gameObject);
    }
}
