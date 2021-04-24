using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    private float maxSpeed = 8.0f;
    private float minSpeed = -4.0f;
    private float speedFalloff = 1.0f;

    private Vector2 direction = Vector2.right;
    private float speed;

    private Rigidbody2D rigidBody;
    private Transform rotationTarget;

    public void Init(Transform rotationTarget)
    {
        this.rotationTarget = rotationTarget;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

        float angleDiff = Vector2.SignedAngle(direction, rotationTarget.right);
        rotationTarget.Rotate(Vector3.back, angleDiff);
    }

    void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
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


}
