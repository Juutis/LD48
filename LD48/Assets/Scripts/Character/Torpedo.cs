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
    private float createdTime;

    private Rigidbody2D rigidBody;
    private Vector3 followTarget;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        createdTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = transform.right * speed;
        var mousePosition = Input.mousePosition;
        followTarget = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void FixedUpdate()
    {

        if (Time.time - createdTime <= followTime)
        {
            float angleDiff = Vector2.SignedAngle(followTarget - transform.position, transform.right);
            transform.Rotate(Vector3.back, Mathf.Clamp(angleDiff, -5, 5));
        }
        else if (Time.time - createdTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
