using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource MusicSource;
    public AudioSource EndingSoundSource;
    public AudioSource SFXSource;
    public AudioSource WalkingSFXSource;
    public AudioSource JumpingSFXSource;
    [Header("Audio Clips")]
    public AudioClip BackgroundMusic;
    public AudioClip Pause;
    public AudioClip ClickOnPause;
    public AudioClip MouseClick;
    public AudioClip ButtonSound;
    public AudioClip JumpSound;
    public AudioClip SmallWalkSound;
    public AudioClip MediumWalkSound;
    public AudioClip LargeWalkSound;
    public AudioClip DoorSound;
    public AudioClip RouterSound;
    public AudioClip ElectricBoxSound;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
    public void PlayWalkingSFX(AudioClip clip)
    {
        if (!WalkingSFXSource.isPlaying)
        {
            WalkingSFXSource.clip = clip;
            WalkingSFXSource.Play();
        }
    }
    
    public void StopWalkingSFX()
    {
        WalkingSFXSource.Stop();
    }

    public void PlayJumpingSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
    public void StopSFX()
    {
        SFXSource.Stop();
    }
}
