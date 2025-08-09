using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource MusicSource;
    public AudioSource SFXSource;
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

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void StopSFX(AudioClip clip)
    {
        SFXSource.Stop();
    }
}
