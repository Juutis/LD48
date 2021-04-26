using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineEngineSound : MonoBehaviour
{
    [SerializeField]
    private bool Enabled = true;

    [SerializeField]
    private float pitchFactor = 1f;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource underWaterSoundAudioSource;

    [SerializeField]
    private float maxUnderGroundVolume = 1f;
    private float currentUnderGroundVolume = 0f;
    [SerializeField]
    private float depthUnderWaterSoundFactor = 1f;
    private Submarine submarine;
    void Start()
    {
        submarine = GetComponent<Submarine>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 1f && audioSource.isPlaying) {
            audioSource.Pause();
        } else if (Time.timeScale >= 0.9f && !audioSource.isPlaying) {
            audioSource.Play();
        }
        audioSource.pitch = submarine.Speed * pitchFactor;
        currentUnderGroundVolume = GameManager.main.PlayerDepth * depthUnderWaterSoundFactor;
        underWaterSoundAudioSource.volume = Mathf.Clamp(currentUnderGroundVolume, 0, maxUnderGroundVolume);
    }
}
