using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Submarine : MonoBehaviour
{
    [SerializeField]
    private Animator propeller;
    [SerializeField]
    private Light2D headlight;
    [SerializeField]
    private GameObject headlightObject;

    private float maxSpeed = 6.0f;
    private float minSpeed = -4.0f;
    private float speedFalloff = 1.0f;

    private Vector2 direction = Vector2.right;
    private float speed;

    [SerializeField]
    private int lightLevel = 0;

    [SerializeField]
    private ParticleSystem bubbles;

    private Rigidbody2D rigidBody;
    private Transform rotationTarget;
    private Quaternion initialRotation;
    private SpriteRenderer renderer;
    private PlayerConfig config;

    public float Depth { get { return -transform.position.y; } }

    public float Speed { get { return rigidBody.velocity.magnitude; } }

    public void Init(Transform rotationTarget, PlayerConfig playerConfig)
    {
        this.rotationTarget = rotationTarget;
        initialRotation = transform.rotation;
        renderer = rotationTarget.GetComponentInChildren<SpriteRenderer>();
        config = playerConfig;
    }

    public void Stop() {
        transform.rotation = initialRotation;
        rigidBody.velocity = Vector2.zero;
        speed = 0f;
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
        var rotationPerTick = Mathf.Clamp(angleDiff, -720 * Time.deltaTime, 720 * Time.deltaTime);
        rotationTarget.Rotate(Vector3.back, rotationPerTick);

        if (direction.x < -0.1f)
        {
            renderer.transform.localRotation = Quaternion.Euler(180, 0, 0);
        }
        if (direction.x > 0.1f)
        {
            renderer.transform.localRotation = Quaternion.Euler(0, 0, 0);
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

        if (lightLevel == 0)
        {
            headlightObject.SetActive(false);
        }
        else
        {
            headlightObject.SetActive(true);
        }

        if (headlight != null && config.HeadlightConfig != null && config.HeadlightConfig.headlightLevels.Count > 0 && lightLevel > 0)
        {
            HeadlightLevel lightLevelObj = config.HeadlightConfig.headlightLevels[lightLevel-1];
            headlight.intensity = lightLevelObj.intensity;
            headlight.pointLightInnerAngle = lightLevelObj.innerSpotAngle;
            headlight.pointLightOuterAngle = lightLevelObj.outerSpotAngle;
            headlight.pointLightInnerRadius = lightLevelObj.innerRadius;
            headlight.pointLightOuterRadius = lightLevelObj.outerRadius;
        }
        
        propeller.speed = Mathf.Abs(speed);
        var effect = bubbles.emission;
        effect.rateOverTimeMultiplier = Mathf.Abs(speed) * 10;

        if (transform.position.y > 0)
        {
            bubbles.Stop();
        }
        else
        {
            bubbles.Play();
        }
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

    public void AddLightLevel(int value)
    {
        lightLevel += value;
    }

    public float GetHeadlight()
    {
        return lightLevel;
    }
}