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
    private float attackSpeed = 1.5f;
    private float damage = 1f;

    private Hurtable hurtable;

    private float shot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        submarine = GetComponent<Submarine>();
        weapon = GetComponent<Weapon>();
        submarine.Init(rend.transform.parent, config);

        hurtable = GetComponent<Hurtable>();
        hurtable.Initialize(config.HealthConfig);
        ReadConfig();
    }

    // Update is called once per frame
    void Update()
    {
        submarine.Accelerate(Input.GetAxis("Vertical") * accelerationSpeed * Time.deltaTime);
        submarine.Rotate(Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        float delay = 10f / attackSpeed;
        if (Input.GetMouseButton(0) && (Time.time - shot > delay))
        {
            if (weapon != null)
            {
                weapon.Shoot(transform, submarine.GetRotation(), damage);
                shot = Time.time;
            }
        }
    }

    private void ReadConfig()
    {
        accelerationSpeed = config.AccelerationSpeed;
        rotationSpeed = config.RotationSpeed;
        attackSpeed = config.AttackSpeed;
        damage = config.Damage;
    }

    public void UpgradeAttackSpeed(float upgrade)
    {
        attackSpeed = upgrade;
    }
    public void UpgradeMovementSpeed(float upgrade)
    {
        submarine.AddMaxSpeed(upgrade);
    }
    public void UpgradeDamage(float upgrade)
    {
        damage = upgrade;
    }

    public void UpgradeMaxHealth(float upgrade)
    {
        hurtable.UpgradeMaxHealth(upgrade);
    }

    public void UpgradeLights(float value)
    {
        submarine.AddLightLevel(value);
    }
}
