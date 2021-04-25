using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private bool playMusic = true;
    // Start is called before the first frame update
    [SerializeField]
    private AudioSource musicSource;
    void Start()
    {
        if (playMusic && musicSource != null) {
            musicSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
