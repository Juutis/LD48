using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hurtable))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private PlayerConfig config;

    Submarine submarine;
    Renderer rend;
    Weapon weapon;

    private float accelerationSpeed = 10.0f;
    private float rotationSpeed = 180.0f;

    private Hurtable hurtable;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        submarine = GetComponent<Submarine>();
        weapon = GetComponent<Weapon>();
        submarine.Init(rend.transform.parent);

        hurtable = GetComponent<Hurtable>();
        hurtable.Initialize(config.HealthConfig);
    }

    // Update is called once per frame
    void Update()
    {
        submarine.Accelerate(Input.GetAxis("Vertical") * accelerationSpeed * Time.deltaTime);
        submarine.Rotate(Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            if (weapon != null)
            {
                weapon.Shoot(transform, submarine.GetRotation());
            }
        }
    }
}
