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

        if (config.IsJelly)
        {
            rigidBody.gravityScale = 0.1f;
            direction = Vector2.up;
        }
        Swim();
    }

    // Update is called once per frame
    void Update()
    {
        handleState();
        
        if (!config.IsJelly)
        {
            if (direction.x < -0.1f)
            {
                renderer.transform.localRotation = Quaternion.Euler(180, 0, 0);
            }
            if (direction.x > 0.1f)
            {
                renderer.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
        float angleDiff = Vector2.SignedAngle(direction, renderer.transform.right);
        renderer.transform.Rotate(Vector3.back, angleDiff);

        switch (state)
        {
            case FishState.IDLE:
                anim.speed = config.AnimationSpeed;
                break;
            case FishState.ATTACK:
                anim.speed = config.AggroSpeedScaling;
                break;

        }

        if (transform.position.y > 0)
        {
            rigidBody.gravityScale = 1.0f;
        }
        else
        {
            if (config.IsJelly)
            {
                rigidBody.gravityScale = 0.1f;
            }
            else
            {
                rigidBody.gravityScale = 0.0f;
            }
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
        anim.SetBool("swim", false);
        Retarget();
        var speedScale = state == FishState.ATTACK ? config.AggroSpeedScaling : 1.0f;
        rigidBody.velocity = direction.normalized * config.MoveSpeed * speedScale;

        if (config.MinDelayBetweenMoving > 0.01f || config.MaxDelayBetweenMoving > 0.01f)
        {
            var delay = Random.Range(config.MinDelayBetweenMoving, config.MaxDelayBetweenMoving) / speedScale;
            Invoke("Swim", delay);
        }
        else
        {
            Swim();
        }
    }

    public void Swim()
    {
        anim.SetBool("swim", true);
    }

    public void Retarget()
    {
        if (state == FishState.IDLE)
        {
            if (Random.Range(0.0f, 1.0f) < config.ChangeDirectionChance)
            {
                if (config.IsJelly)
                {
                    direction = new Vector2(-direction.x, 10.0f);
                }
                else
                {
                    direction = new Vector2(-direction.x, 0.0f);
                }
            }
        }

        if (state == FishState.ATTACK)
        {
            direction = player.position - transform.position;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!config.IsJelly)
        {
            direction = new Vector2(-direction.x, 0.0f);
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
