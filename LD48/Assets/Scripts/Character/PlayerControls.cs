using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Hurtable))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private PlayerConfig config;

    Submarine submarine;

    public Submarine Submarine { get { return submarine; } }
    Renderer rend;
    Weapon weapon;

    private float accelerationSpeed = 10.0f;
    private float rotationSpeed = 180.0f;
    private float attackSpeed = 1.5f;
    private float damage = 1f;

    private Hurtable hurtable;

    private bool controlsEnabled = true;

    private float shot = -10f;
    private bool yAxisInverted = false;

    public bool YAxisInverted { get { return yAxisInverted;} }

    private FollowMouse crosshair;

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

        crosshair = GetComponentInChildren<FollowMouse>();
    }

    public void EnableControls() {
        controlsEnabled = true;
    }

    public void DisableControls() {
        controlsEnabled = false;
    }

    public void ToggleYAxis() {
        yAxisInverted = !yAxisInverted;
    }

    // Update is called once per frame
    void Update()
    {
        if (!controlsEnabled) {
            return;
        }
        float yAxis = Input.GetAxis("Horizontal");
        if (yAxisInverted) {
            yAxis = -yAxis;
        }
        submarine.Accelerate(Input.GetAxis("Vertical") * accelerationSpeed * Time.deltaTime);
        submarine.Rotate(yAxis * rotationSpeed * Time.deltaTime);

        float delay = 10f / attackSpeed;
        var readyToShoot = Time.time - shot > delay;
        crosshair.ReadyToShoot = readyToShoot;

        if (readyToShoot && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)))
        {
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
            if (weapon != null)
            {
                weapon.Shoot(transform, submarine.GetRotation(), damage);
                SoundPlayer.main.PlaySound(GameSoundType.TorpedoShoot);
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
        hurtable.Heal(upgrade);
    }

    public void UpgradeLights(float value)
    {
        submarine.AddLightLevel(Mathf.RoundToInt(value));
    }

    public float GetHeadlight()
    {
        return submarine.GetHeadlight();
    }

    public void EnableHomingTorpedos()
    {
        weapon.SetTracking(true);
        crosshair.Follow = true;
    }

    public float GetHealth()
    {
        return hurtable.GetHealth();
    }

    public float GetMaxHealth()
    {
        return hurtable.GetMaxHealth();
    }

    public void Heal(float amount)
    {
        hurtable.Heal(amount);
    }
}
