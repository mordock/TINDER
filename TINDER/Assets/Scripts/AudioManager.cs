using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }

    public void SetPitch(float pitch)
    {
        audioSource.pitch = pitch;  
    }
}
