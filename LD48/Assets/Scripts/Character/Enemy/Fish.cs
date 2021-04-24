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
    private Animator anim;

    private Vector2 direction = Vector2.right;

    private int playerLayer;
    private Transform player;

    private FishState state = FishState.IDLE;




    // Start is called before the first frame update
    void Start()
    {
        hurtable = GetComponent<Hurtable>();
        hurtable.Initialize(config.HealthConfig);

        lootDropper = GetComponent<LootDropper>();
        lootDropper.Initialize(config.LootConfig);

        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();

        Retarget();

        playerLayer = LayerMask.NameToLayer("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        handleState();

        if (rigidBody.velocity.x < -0.1f)
        {
            renderer.flipY = true;
        }
        if (rigidBody.velocity.x > 0.1f)
        {
            renderer.flipY = false;
        }

        float angleDiff = Vector2.SignedAngle(rigidBody.velocity, renderer.transform.right);
        renderer.transform.Rotate(Vector3.back, angleDiff);

        switch (state)
        {
            case FishState.IDLE:
                anim.speed = 1.0f;
                break;
            case FishState.ATTACK:
                anim.speed = config.AggroSpeedScaling;
                break;

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
        var moveSpeed = state == FishState.ATTACK ? config.MoveSpeed * config.AggroSpeedScaling : config.MoveSpeed;
        rigidBody.velocity = direction.normalized * moveSpeed;
    }

    public void Retarget()
    {
        if (state == FishState.IDLE)
        {
            if (Random.Range(0.0f, 1.0f) < config.ChangeDirectionChance)
            {
                direction = new Vector2(-direction.x, 0.0f);
            }
        }

        if (state == FishState.ATTACK)
        {
            direction = player.position - transform.position;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        var hurtable = collision.gameObject.GetComponent<Hurtable>();
        if (hurtable != null)
        {
            if (collision.gameObject.layer == playerLayer)
            {
                hurtable.Hurt(config.DamageOnTouch);
            }
        }
    }

    private void handleState()
    {
        if (!config.IsAggressive)
        {
            return;
        }

        if (Vector2.Distance(player.position, transform.position) < config.AggroRange)
        {
            state = FishState.ATTACK;
        }
        else
        {
            state = FishState.IDLE;
        }
    }
    
}

public enum FishState
{
    IDLE,
    ATTACK
}
