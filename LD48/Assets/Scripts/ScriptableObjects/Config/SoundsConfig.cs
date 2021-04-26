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

    public List<DepthSound> depthSounds = new List<DepthSound>();


    public void Init() {
        foreach(DepthSound depthSound in depthSounds) {
            depthSound.Reset();
        }
    }

    public DepthSound GetAvailableDepthSound(float depth) {
        DepthSound depthSound = depthSounds.Where(
            sound => depth > sound.DepthMin && depth < sound.DepthMax
        ).FirstOrDefault();
        return depthSound;
    }

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

    [SerializeField]
    public float Volume = 1f;
}
[System.Serializable]
public class DepthSound
{
    [field: SerializeField]
    public List<AudioClip> Clips { get; private set; }

    [field: SerializeField]
    [field: Range(0, 1000)]
    public float DepthMin { get; private set; }

    [field: SerializeField]
    [field: Range(0, 1000)]
    public float DepthMax { get; private set; }

    [field: SerializeField]
    public float Interval {get; private set;}
    
    [field: SerializeField]
    [field: Range(0, 1)]
    public float Chance {get; private set;}

    [HideInInspector]
    public float PlayedPreviously = 0f;
    [HideInInspector]
    public float Timer = 0f;

    public void Reset() {
        PlayedPreviously = 0f;
        Timer = 0f;
    }
}

public enum GameSoundType
{
    None,
    Whale,
    CreepySubmarineSound,
    TorpedoShoot,
    TorpedoHitWall,
    TorpedoHitFish,
    TorpedoExplodeWeak,
    TorpedoExplodeStrong
}