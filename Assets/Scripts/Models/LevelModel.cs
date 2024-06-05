using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
#endif

[Serializable]
public class LevelModel
{
    public string levelID;
    public UnityEngine.Object scene;
    public int highScore;
    public int currentScore;
    public int deltaScore;
    public AudioClip backgroundSound;
}
