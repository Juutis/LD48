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
        else if (amount > 0f && GameManager.main.FirstHitNormal)
        {
            UIPopupManager.main.ShowPopup("Breaking stalagmites", "It seems you need a stronger torpedo to get through these rock formations.");
            GameManager.main.FirstHitNormal = false;
        }
        else if (amount > 10.0f && GameManager.main.FirstHitMedium)
        {
            GameManager.main.FirstHitMedium = false;
            UIPopupManager.main.ShowPopup("Breaking stalagmites", "Still not enough. Get the strongest torpedo and you may get through!");
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
