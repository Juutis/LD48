using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField]
    private Animator propeller;

    private float maxSpeed = 8.0f;
    private float minSpeed = -4.0f;
    private float speedFalloff = 1.0f;

    private Vector2 direction = Vector2.right;
    private float speed;

    private Rigidbody2D rigidBody;
    private Transform rotationTarget;
    private SpriteRenderer renderer;

    public void Init(Transform rotationTarget)
    {
        this.rotationTarget = rotationTarget;
        renderer = rotationTarget.GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float angleDiff = Vector2.SignedAngle(direction, rotationTarget.right);

        if (direction.x < -0.1f)
        {
            renderer.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            rotationTarget.Rotate(Vector3.back, angleDiff + 180);
        }
        if (direction.x > 0.1f)
        {
            renderer.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            rotationTarget.Rotate(Vector3.back, angleDiff);
        }

        if (speed > 0.01f)
        {
            speed -= speedFalloff * Time.deltaTime;
        }
        else if (speed < -0.01f)
        {
            speed += speedFalloff * Time.deltaTime;
        }
        else
        {
            speed = 0.0f;
        }

        propeller.speed = Mathf.Abs(speed);


    }

    void FixedUpdate()
    {

        if (transform.position.y > 0)
        {
            rigidBody.gravityScale = 1.0f;
            direction = rigidBody.velocity.normalized;
            speed = rigidBody.velocity.magnitude;
        }
        else
        {
            rigidBody.gravityScale = 0.0f;
            rigidBody.velocity = direction * speed;
        }
    }

    public void Rotate(float degrees)
    {
        direction = Quaternion.AngleAxis(degrees, Vector3.back) * direction;
        direction.Normalize();
    }

    public void Accelerate(float acceleration)
    {
        speed += acceleration;

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        if (speed < minSpeed)
        {
            speed = minSpeed;
        }
    }
    public Vector2 GetRotation()
    {
        return direction;
    }

    public void AddMaxSpeed(float value)
    {
        maxSpeed += value;
    }
}
