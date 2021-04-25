using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSoundSource : MonoBehaviour
{
    private List<AudioSource> audioSources = new List<AudioSource>();

    public AudioClip Clip {get; private set;}

    public void Init(AudioClip clip) {
        Clip = clip;
        AudioSource source = Prefabs.Get<AudioSource>();
        source.clip = clip;
        source.transform.SetParent(transform);
        audioSources.Add(source);
    }

    public void Play() {
        AudioSource source = audioSources.Find(source => !source.isPlaying); 
        if (source == null) {
            source = Prefabs.Get<AudioSource>();
            source.transform.SetParent(transform);
            source.clip = Clip;
            audioSources.Add(source);
        }
        source.Play();
    }
}