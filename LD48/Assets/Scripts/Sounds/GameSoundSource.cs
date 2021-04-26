using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSoundSource : MonoBehaviour
{
    private List<AudioSource> audioSources = new List<AudioSource>();

    public AudioClip Clip {get; private set;}

    private float customVolume = 1f;

    public void Init(AudioClip clip, float volume = 1f) {
        Clip = clip;
        AudioSource source = Prefabs.Get<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.transform.SetParent(transform);
        customVolume = volume;
        audioSources.Add(source);
    }

    public void Play() {
        AudioSource source = audioSources.Find(source => !source.isPlaying); 
        if (source == null) {
            source = Prefabs.Get<AudioSource>();
            source.transform.SetParent(transform);
            source.clip = Clip;
            source.volume = customVolume;
            audioSources.Add(source);
        }
        source.Play();
    }
}