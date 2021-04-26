using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hurtable))]
public class Stalagmite : MonoBehaviour
{
    [SerializeField]
    private HealthConfig healthConfig;

    private Hurtable hurtable;

    // Start is called before the first frame update
    void Start()
    {
        hurtable = GetComponent<Hurtable>();
        hurtable.Initialize(healthConfig);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damaged(float amount)
    {
        if (amount > 0)
        {
            hurtable.Hurt(-amount);
        }
        if (amount > 21.0f)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
