using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeConfig", menuName = "Configs/New UpgradeConfig")]
public class UpgradeConfig : ScriptableObject
{
    public List<Upgrade> damage;
    public List<Upgrade> attackSpeed;
    public List<Upgrade> movementSpeed;
    public List<Upgrade> lights;
    public List<Upgrade> health;

    [field:SerializeField]
    public List<DepthDamage> DepthDamages {get; private set;}

}

[Serializable]
public struct Upgrade
{
    public string loreText;
    public string description;
    public float value;
    public int price;
}
[System.Serializable]
public class DepthDamage
{
    [field: SerializeField]
    public float HealthBelow {get; private set;}
    [field: SerializeField]
    public float Depth {get; private set;}
    [field: SerializeField]
    public float DamageTaken {get; private set;}
    [field: SerializeField]
    public float DamageInterval {get; private set;}
    [field: SerializeField]
    [field: TextArea]
    public string Message {get; private set;}
}
