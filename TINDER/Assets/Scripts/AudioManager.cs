using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource asBackgroundMusic;
    [SerializeField]
    private AudioSource asSFX;
    [SerializeField]
    private AudioClip splash;
    void Start()
    {
        asBackgroundMusic = GetComponent<AudioSource>();
    }

    public void SetPitch(float pitch)
    {
        asBackgroundMusic.pitch = pitch;  
    }

    public void PlaySplash()
    {
        asSFX.PlayOneShot(splash);
    }
}
