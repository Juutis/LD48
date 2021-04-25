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
}

[Serializable]
public struct Upgrade
{
    public string loreText;
    public string description;
    public float value;
    public int price;
}
