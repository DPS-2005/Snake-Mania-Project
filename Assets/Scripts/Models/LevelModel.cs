using System;
using UnityEngine;

[Serializable]
public class LevelModel
{
    public string levelID;
    public int highScore;
    public int currentScore;
    public int deltaScore;
    public AudioClip backgroundSound;
    public AudioClip eatSound;
    public AudioClip deathSound;
}
