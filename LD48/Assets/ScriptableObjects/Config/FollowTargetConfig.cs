using UnityEngine;

[CreateAssetMenu(fileName = "FollowTargetConfig", menuName = "Configs/New FollowTargetConfig")]
public class FollowTargetConfig : ScriptableObject
{
    public bool FollowX = false;
    public bool FollowY = false;
    public bool FollowZ = false;
    public float Speed = 1f;
    public bool UseLateUpdate = false;
    public bool UseSmoothDamp = false;
}