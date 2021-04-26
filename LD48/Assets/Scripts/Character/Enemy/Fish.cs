using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.Rendering.Universal;

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

    public FishState state = FishState.IDLE;


    private FishSpawner spawner;
    private FishSpawn spawn;

    private float explodeTimer;
    private List<SpriteRenderer> spriteRenderers;
    private List<Color> origColors;

    [SerializeField]
    private Light2D light;

    [SerializeField]
    private ParticleSystem explosionExplosion;

    private Vector2 startPosition;

    public void Init(FishSpawner fishSpawner, FishSpawn fishSpawn, Transform parent, Vector2 pos) {
        spawner = fishSpawner;
        spawn = fishSpawn;
        transform.SetParent(parent);
        transform.position = pos;
    }

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

        spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        spriteRenderers.AddRange(GetComponents<SpriteRenderer>());
        origColors = spriteRenderers.Select(rend => rend.color).ToList();

        startPosition = transform.position;
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
        
        float angleDiff = Vector2.SignedAngle(direction, transform.right);
        transform.Rotate(Vector3.back, angleDiff);

        switch (state)
        {
            case FishState.IDLE:
                anim.speed = config.AnimationSpeed;
                break;
            case FishState.ATTACK:
                anim.speed = config.AggroSpeedScaling;
                break;

        }

        if (!IsInWater())
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

        if (state == FishState.EXPLODE)
        {
            explodeTint();
        }
    }

    public bool IsInWater()
    {
        return transform.position.y <= 0;
    }

    public void Die()
    {
        if (dieEffect != null)
        {
            var effect = Instantiate(dieEffect);
            effect.transform.position = transform.position;
        }
        lootDropper.DropLoot();
        if (spawner != null) {
            spawner.FishDie(spawn);
        }
        Destroy(gameObject);
    }

    public void Move()
    {
        Move(false);
    }

    public void Move(bool skipRetarget)
    {
        if (state == FishState.EXPLODE)
        {
            return;
        }

        if (config.IsJelly && Vector2.Distance(startPosition, transform.position) > 5.0)
        {
            return;
        }

        anim.SetBool("swim", false);
        if (!skipRetarget)
        {
            Retarget();
        }
        var speedScale = state == FishState.ATTACK ? config.AggroSpeedScaling : 1.0f;

        if (transform.position.y > -1.0f)
        {
            direction = new Vector2(direction.normalized.x, -1.0f);
        }

        if (IsInWater())
        {
            rigidBody.velocity = direction.normalized * config.MoveSpeed * speedScale;
        }

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
        if (!IsInWater())
        {
            return;
        }

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
                if (state == FishState.ATTACK)
                {
                    state = FishState.COOLDOWN;
                    TurnAround();
                    Move(true);
                    Invoke("ReAggro", 1.0f);
                }
            }
        }
    }

    public void TurnAround()
    {
        direction = transform.position - player.position;
    }

    public void ReAggro()
    {
        state = FishState.ATTACK;
    }

    private void handleState()
    {
        if (state == FishState.COOLDOWN || state == FishState.EXPLODE)
        {
            return;
        }

        if (config.ExplodeTriggerDistance > 0.001f && Vector2.Distance(player.position, transform.position) < config.ExplodeTriggerDistance)
        {
            state = FishState.EXPLODE;
            Invoke("Explode", config.ExplodeDelay);
            explodeTimer = Time.time;
        }

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

    public void Explode()
    {
        if (Vector2.Distance(player.position, transform.position) < config.ExplodeRadius)
        {
            player.GetComponent<Hurtable>().Hurt(config.ExplodeDamage);
        }

        if (explosionExplosion != null)
        {
            var effect = Instantiate(explosionExplosion);
            effect.transform.position = transform.position;
        }

        Die();
    }

    private void explodeTint()
    {
        var t = (Mathf.Sin((Time.time - explodeTimer) * Mathf.PI * 10) + 1.0f) / 2.0f;
        var index = 0;
        foreach (var rend in spriteRenderers)
        {
            var origColor = origColors[index++];
            var color = Color.Lerp(config.ExplodeTintColor, origColor, t);
            rend.color = color;
        }

        if (light != null)
        {
            light.intensity = t * 2.0f;
        }
    }

}

public enum FishState
{
    IDLE,
    ATTACK,
    COOLDOWN,
    EXPLODE
}
