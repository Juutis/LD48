using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float lifeTime = 10f;
    [SerializeField]
    private float followTime = 5f;
    [SerializeField]
    private float turnSpeed = 180;
    [SerializeField]
    private float damage = 1;

    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    private GameObject trackingIndicator;

    [SerializeField]
    private ParticleSystem explosion;

    [SerializeField]
    private ParticleSystem bigExplosion;

    private Rigidbody2D rigidBody;
    private Collider2D collider;
    private Renderer renderer;
    private Vector3 followTarget;

    private float createdTime;

    private Vector2 direction = Vector2.right;

    private bool alive = true;
    private bool follows = false;

    private bool explodeSoundPlayed = false;
    private bool hitSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();
        createdTime = Time.time;
        direction = transform.right;
    }

    public void Instantiate(float damage, bool follows)
    {
        this.damage = damage;
        this.follows = follows;

        Debug.Log(damage);

        if (damage < 15.0f)
        {
            collider = GetComponent<Collider2D>();
            collider.isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Input.mousePosition;
        followTarget = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);

        if (Time.time - createdTime <= followTime && alive && follows)
        {
            float angleDiff = Vector2.SignedAngle(followTarget - transform.position, direction);
            direction = Quaternion.AngleAxis(Mathf.Clamp(angleDiff, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime), Vector3.back) * direction;
            trackingIndicator.SetActive(true);
        }
        else
        {
            trackingIndicator.SetActive(false);
        }

        if (Time.time - createdTime > lifeTime)
        {
            Kill();
        }

        if (alive)
        {
            float velocityAngleDiff = Vector2.SignedAngle(rigidBody.velocity, transform.right);
            transform.Rotate(Vector3.back, velocityAngleDiff);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y > 0)
        {
            rigidBody.gravityScale = 1.0f;
            direction = rigidBody.velocity;
            particles.Stop();
        }
        else
        {
            if (alive)
            {
                rigidBody.velocity = direction.normalized * speed;
                rigidBody.gravityScale = 0.0f;
                particles.Play();
            }
            else
            {
                rigidBody.gravityScale = 0.2f;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var hurtable = collision.gameObject.GetComponent<Hurtable>();
        if (hurtable != null)
        {
            hurtable.Hurt(damage);
            if (!hitSoundPlayed) {
                hitSoundPlayed = true;
                SoundPlayer.main.PlaySound(GameSoundType.TorpedoHitFish);
            }
        }
        var lever = collision.gameObject.GetComponent<Lever>();
        if (lever != null)
        {
            lever.Push();
        }
        if (hurtable == null && lever == null) {
            if (!hitSoundPlayed) {
                hitSoundPlayed = true;
                SoundPlayer.main.PlaySound(GameSoundType.TorpedoHitWall);
            }
        }
        Kill();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var hurtable = collision.gameObject.GetComponent<Hurtable>();
        if (hurtable != null)
        {
            hurtable.Hurt(damage);
            if (!hitSoundPlayed) {
                hitSoundPlayed = true;
                SoundPlayer.main.PlaySound(GameSoundType.TorpedoHitFish);
            }
        }
        var lever = collision.gameObject.GetComponent<Lever>();
        if (lever != null)
        {
            lever.Push();
        }
        if (hurtable == null && lever == null) {
            if (!hitSoundPlayed) {
                hitSoundPlayed = true;
                SoundPlayer.main.PlaySound(GameSoundType.TorpedoHitWall);
            }
        }
        Kill();
    }

    public void Kill()
    {
        alive = false;
        particles.Stop();
        if (damage > 25.0f)
        {
            var expl = Instantiate(bigExplosion);
            expl.transform.position = transform.position;
            renderer.enabled = false;
            if (!explodeSoundPlayed) {
                explodeSoundPlayed = true;
                SoundPlayer.main.PlaySound(GameSoundType.TorpedoExplodeStrong);
            }
            Invoke("ReallyKill", 1.5f);
        }
        else if (damage > 15.0f)
        {
            var expl = Instantiate(explosion);
            expl.transform.position = transform.position;
            if (!explodeSoundPlayed) {
                explodeSoundPlayed = true;
                SoundPlayer.main.PlaySound(GameSoundType.TorpedoExplodeWeak);
            }
            renderer.enabled = false;
            Invoke("ReallyKill", 1.5f);
        }
        else
        {
            Invoke("ReallyKill", 10.0f);
        }
        collider.enabled = false;
        trackingIndicator.SetActive(false);
    }

    public void ReallyKill()
    {
        Destroy(gameObject);
    }
}
