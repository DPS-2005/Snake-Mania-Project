using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

[Serializable]
public class LevelModel
{
    public string levelID;
    public SceneAsset scene;
    public int highScore;
    public int currentScore;
    public int deltaScore;
}
