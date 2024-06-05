using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public float musicVolume;
    public float soundVolume;
    public Slider soundSlider;
    public Slider musicSlider;
    public AudioSource source;
    public AudioClip theme;
    void Start()
    {
        source = GetComponent<AudioSource>();
        soundVolume = soundSlider.value;
        musicVolume = musicSlider.value;
        source.clip = theme;
        source.Play();
    }

    public void ChangeSoundVolume()
    {
        soundVolume = soundSlider.value;
    }

    public void ChangeMusicVolume()
    {
        musicVolume = musicSlider.value;
        source.volume = musicVolume;
    }

    public void PlayOnce( AudioClip clip)
    {
        source.PlayOneShot(clip, soundVolume);
    }

    public void PlayContinuous(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Play()
    {
        source.Play();
    }
}
