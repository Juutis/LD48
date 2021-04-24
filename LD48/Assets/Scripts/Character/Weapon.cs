using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    GameObject ammoPrefab;

    private Vector2 direction = Vector2.right;
    private Transform rotationTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot(Transform shooter, Vector3 dir)
    {
        GameObject ammo = Instantiate(ammoPrefab);
        ammo.transform.parent = null;
        ammo.transform.position = shooter.transform.position + dir;
        direction = dir;

        float angleDiff = Vector2.SignedAngle(direction, ammo.transform.right);
        ammo.transform.Rotate(Vector3.back, angleDiff);
    }
}
