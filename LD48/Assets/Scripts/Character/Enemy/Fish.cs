using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hurtable))]
public class Fish : MonoBehaviour
{
    [SerializeField]
    private FishConfig config;

    [SerializeField]
    private GameObject dieEffect;

    private Hurtable hurtable;

    

    // Start is called before the first frame update
    void Start()
    {
        hurtable = GetComponent<Hurtable>();
        hurtable.Initialize(config.HealthConfig);
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
        Destroy(gameObject);
    }
}
