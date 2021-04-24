using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public FollowTargetConfig config;

    private Transform target = null;
    private Vector3 curVel;
    private bool initialized = false;
    private float minimumDifference = 0.01f;

    public void Start()
    {
        Initialize(true);
    }

    public void Initialize(bool instantPosition = true)
    {
        target = GameObject.FindGameObjectWithTag("CameraFollowTarget").transform;
        initialized = true;
        if (instantPosition) {
            transform.position = new Vector3(
                config.FollowX ? target.position.x : transform.position.x,
                config.FollowY ? target.position.y : transform.position.y,
                config.FollowZ ? target.position.z : transform.position.z
            );
        }
    }

    public void SetPositionToTarget() {
        transform.position = new Vector3(
            config.FollowX ? target.position.x : transform.position.x,
            config.FollowY ? target.position.y : transform.position.y,
            config.FollowZ ? target.position.z : transform.position.z
        );
    }

    void Update()
    {
        if (initialized && !config.UseLateUpdate)
        {
            UpdateTransform();
        }
    }

    void LateUpdate()
    {
        if (initialized && config.UseLateUpdate)
        {
            UpdateTransform();
        }
    }

    void UpdateTransform()
    {
        if (target == null || !(config.FollowX || config.FollowY || config.FollowZ))
        {
            return;
        }

        Vector3 newPosition = new Vector3(
            config.FollowX ? target.position.x : transform.position.x,
            config.FollowY ? target.position.y : transform.position.y,
            config.FollowZ ? target.position.z : transform.position.z
        );

        if (config.UseSmoothDamp)
        {
            newPosition = Vector3.SmoothDamp(transform.position, newPosition, ref curVel, config.Speed * Time.deltaTime, config.Speed);
        }
        else
        {
            newPosition = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * config.Speed);
        }

        newPosition.x = GetPositionValue(config.FollowX, target.position.x, newPosition.x);
        newPosition.y = GetPositionValue(config.FollowY, target.position.y, newPosition.y);
        newPosition.z = GetPositionValue(config.FollowZ, target.position.z, newPosition.z);

        transform.position = newPosition;

    }

    float GetPositionValue(bool follow, float targetValue, float currentValue) {
        if (follow) {
            if (Mathf.Abs(currentValue - targetValue) < minimumDifference) {
                return targetValue;
            }
        }
        return currentValue;
    }
}
