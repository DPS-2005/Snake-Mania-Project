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
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void ChangeSoundVolume()
    {
        soundVolume = soundSlider.value;
        Debug.Log(soundVolume);
    }

    public void ChangeMusicVolume()
    {
        musicVolume = musicSlider.value;
        Debug.Log(musicVolume);
    }

    public void PlayOnce( AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
