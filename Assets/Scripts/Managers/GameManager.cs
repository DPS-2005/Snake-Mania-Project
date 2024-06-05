using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<LevelModel> levelList;
    public Dictionary<string, LevelModel> levels;
    public GameObject canvas;
    public LevelModel currentLevel = null;
    public bool paused = true;
    public SceneAsset Home;
    public string dataPath;
    public GameObject Menu;

    private void Start()
    {
        dataPath = Application.persistentDataPath + "/levelData.json";
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
        Menu.SetActive(false);
        PanelManager.Instance.GoToPreviousPanel();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        LoadObstacles();
    }

    public void LoadObstacles()
    {
        GameObject SpawnManager = GameObject.FindGameObjectsWithTag("SpawnManager")[0];
        SpawnManager.GetComponent<SpawnManager>().InstantiateObstacles();
    }

    public void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
