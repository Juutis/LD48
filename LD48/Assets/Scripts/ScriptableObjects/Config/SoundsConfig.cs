using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "SoundsConfig", menuName = "Config/SoundsConfig")]
public class SoundsConfig : ScriptableObject
{
    [SerializeField]
    private List<GameSound> sounds = new List<GameSound>();
    public List<GameSound> Sounds { get { return sounds; } }

}

[System.Serializable]
public class GameSound
{
    [field: SerializeField]
    public AudioClip Clip { get; private set; }

    [field: SerializeField]
    public List<AudioClip> Clips { get; private set; }

    [field: SerializeField]
    public GameSoundType Type { get; private set; }
}

public enum GameSoundType
{
    None,
    Whale,
    CreepySubmarineSound,
    TorpedoShoot,
    TorpedoHit
}