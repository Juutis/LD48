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

    private Rigidbody2D rigidBody;
    private SpriteRenderer renderer;

    private Vector2 direction = Vector2.right;


    // Start is called before the first frame update
    void Start()
    {
        hurtable = GetComponent<Hurtable>();
        hurtable.Initialize(config.HealthConfig);

        lootDropper = GetComponent<LootDropper>();
        lootDropper.Initialize(config.LootConfig);

        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();

        Retarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.velocity.x < -0.1f)
        {
            renderer.flipX = false;
        }
        if (rigidBody.velocity.x > 0.1f)
        {
            renderer.flipX = true;
        }
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

    public void Move()
    {
        Retarget();
        rigidBody.velocity = direction.normalized * config.MoveSpeed;
    }

    public void Retarget()
    {
        if (Random.Range(0.0f, 1.0f) < config.ChangeDirectionChance)
        {
            direction = new Vector2(-direction.x, direction.y);
        }
    }
}
