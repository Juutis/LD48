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

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();
        createdTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Input.mousePosition;
        followTarget = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);

        if (Time.time - createdTime <= followTime)
        {
            float angleDiff = Vector2.SignedAngle(followTarget - transform.position, transform.right);
            transform.Rotate(Vector3.back, Mathf.Clamp(angleDiff, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime) );
        }
        else if (Time.time - createdTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = transform.right * speed;
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
