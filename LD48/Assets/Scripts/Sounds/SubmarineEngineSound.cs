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
    private Submarine submarine;
    void Start()
    {
        submarine = GetComponent<Submarine>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = submarine.Speed * pitchFactor;
    }
}
