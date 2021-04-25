using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeadlightConfig", menuName = "Configs/New HeadlightConfig")]
public class HeadlightConfig : ScriptableObject
{
    public List<HeadlightLevel> headlightLevels;
}

[Serializable]
public struct HeadlightLevel
{
    public float innerSpotAngle;
    public float outerSpotAngle;
    public float innerRadius;
    public float outerRadius;
    public float intensity;
} 