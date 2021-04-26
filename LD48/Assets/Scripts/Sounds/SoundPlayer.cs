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
        config.Init();
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
        PlayClip(clip, gameSound.Volume);
        /*
        GameSoundSource source = soundSources.Find(sound => sound.Clip == clip);
        if (source == null)
        {
            source = Prefabs.Get<GameSoundSource>();
            source.transform.SetParent(transform);
            source.Init(clip);
            soundSources.Add(source);
        }
        source.Play();*/
    }

    private void PlayClip(AudioClip clip, float volume = 1f) {
        GameSoundSource source = soundSources.Find(sound => sound.Clip == clip);
        if (source == null)
        {
            source = Prefabs.Get<GameSoundSource>();
            source.transform.SetParent(transform);
            source.Init(clip, volume);
            soundSources.Add(source);
        }
        source.Play();
    }

    void Update() {
        DepthSound depthSound = config.GetAvailableDepthSound(GameManager.main.PlayerDepth);
        if (depthSound == null) {
            return;
        }
        depthSound.Timer += Time.deltaTime;
        if (depthSound.Timer > depthSound.Interval) {
            float rng = Random.Range(0f, 1.0f);
            if (rng <= depthSound.Chance) {
                if (depthSound.Clips.Count > 0) {
                    AudioClip clip = depthSound.Clips[Random.Range(0, depthSound.Clips.Count)];
                    if (clip != null) {
                        PlayClip(clip);
                        Debug.Log($"played {clip} for depth range {depthSound.DepthMin} - {depthSound.DepthMax} ({rng * 100.0f}% -> {depthSound.Chance * 100.0f}%)");
                    }
                }
            } else {
                Debug.Log($"Rng was {rng * 100} and chance is {depthSound.Chance * 100}");
            }
            depthSound.Timer = 0f;
            depthSound.PlayedPreviously = Time.time;
        }
    }
}