using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hurtable))]
public class Stalagmite : MonoBehaviour
{
    [SerializeField]
    private HealthConfig healthConfig;

    private Hurtable hurtable;

    private Rigidbody2D rb;
    private Collider2D collider;

    [SerializeField]
    private GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        hurtable = GetComponent<Hurtable>();
        hurtable.Initialize(healthConfig);

        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
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
        Invoke("ReallyKill", 5.0f);
        collider.enabled = false;
        rb.isKinematic = false;
        rb.gravityScale = 0.5f;
        rb.AddTorque(Random.Range(-3.0f, 3.0f), ForceMode2D.Impulse);
        destroyEffect.SetActive(true);
    }

    public void ReallyKill()
    {
        Destroy(gameObject);
    }
}
