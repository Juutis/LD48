using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer main;
    void Awake()
    {
        main = this;
    }
    private List<GameSoundSource> soundSources = new List<GameSoundSource>();

    [SerializeField]
    private SoundsConfig config;

    public void PlaySound(GameSoundType soundType)
    {
        if (config == null)
        {
            Debug.Log("You must setup your sound config to play sounds!");
        }
        GameSound gameSound = config.Sounds.Find(sound => sound.Type == soundType);
        if (gameSound == null)
        {
            Debug.Log($"Couldn't find a sound for {soundType}!");
        }
        AudioClip clip = gameSound.Clip;
        if (gameSound.Clips != null && gameSound.Clips.Count > 0)
        {
            clip = gameSound.Clips[Random.Range(0, gameSound.Clips.Count)];
        }
        GameSoundSource source = soundSources.Find(sound => sound.Clip == clip);
        if (source == null)
        {
            source = Prefabs.Get<GameSoundSource>();
            source.transform.SetParent(transform);
            source.Init(clip);
            soundSources.Add(source);
        }
        source.Play();
    }
}