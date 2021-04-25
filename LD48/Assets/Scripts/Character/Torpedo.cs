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

    private Rigidbody2D rigidBody;
    private Collider2D collider;
    private Renderer renderer;
    private Vector3 followTarget;

    private float createdTime;

    private Vector2 direction = Vector2.right;

    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();
        createdTime = Time.time;
        direction = transform.right;
    }

    public void Instantiate(float damage)
    {
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Input.mousePosition;
        followTarget = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);

        if (Time.time - createdTime <= followTime)
        {
            float angleDiff = Vector2.SignedAngle(followTarget - transform.position, direction);
            direction = Quaternion.AngleAxis(Mathf.Clamp(angleDiff, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime), Vector3.back) * direction;
        }
        else if (Time.time - createdTime > lifeTime)
        {
            Destroy(gameObject);
        }

        float velocityAngleDiff = Vector2.SignedAngle(rigidBody.velocity, transform.right);
        transform.Rotate(Vector3.back, velocityAngleDiff);
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
            rigidBody.velocity = direction.normalized * speed;
            rigidBody.gravityScale = 0.0f;
            if (alive)
            {
                particles.Play();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var hurtable = collision.gameObject.GetComponent<Hurtable>();
        if (hurtable != null)
        {
            hurtable.Hurt(damage);
        }
        Kill();
    }

    public void Kill()
    {
        alive = false;
        particles.Stop();
        collider.enabled = false;
        renderer.enabled = false;
        Invoke("ReallyKill", 1.5f);
    }

    public void ReallyKill()
    {
        Destroy(gameObject);
    }
}
