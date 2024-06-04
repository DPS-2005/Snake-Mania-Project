using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<LevelModel> levelList;
    public Dictionary<string, LevelModel> levels;
    public GameObject canvas;
    public LevelModel currentLevel = null;
    public bool paused = true;

    private void Start()
    {
        levels = new Dictionary<string, LevelModel>();
        foreach(var level in levelList)
        {
            levels[level.levelID] = level;
        }
        levels.TryGetValue(SceneManager.GetActiveScene().name, out currentLevel);
    }

    public void IncreaseScore()
    {
        currentLevel.currentScore += currentLevel.deltaScore;
        currentLevel.highScore = Mathf.Max(currentLevel.highScore, currentLevel.currentScore);
    }

    public void LoadLevel(string levelID)
    {
        currentLevel = levels[levelID];
        currentLevel.currentScore = 0;
        paused = false;
        StartCoroutine(LoadSceneAsync());
    }

    public IEnumerator LoadSceneAsync()
    {
        AsyncOperation loaded = SceneManager.LoadSceneAsync(currentLevel.scene.name);
        while (!loaded.isDone)
            yield return null;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        LoadObstacles();
    }

    public void LoadObstacles()
    {
        GameObject SpawnManager = GameObject.FindGameObjectsWithTag("SpawnManager")[0];
        SpawnManager.GetComponent<SpawnManager>().InstantiateObstacles();
    }
}
