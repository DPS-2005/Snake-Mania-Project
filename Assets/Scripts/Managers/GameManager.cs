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
    private GameObject instantiatedCanvas = null;

    private void Start()
    {
        levels = new Dictionary<string, LevelModel>();
        foreach(var level in levelList)
        {
            levels[level.levelID] = level;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Pause();
            instantiatedCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
    }

    public void GameOver()
    {
        Pause();
        instantiatedCanvas.transform.GetChild(1).gameObject.SetActive(true);
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

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation loaded = SceneManager.LoadSceneAsync(currentLevel.scene.name);
        while (!loaded.isDone)
            yield return null;
        instantiatedCanvas = Instantiate(canvas);
        LoadObstacles();
    }

    public void LoadObstacles()
    {
        GameObject SpawnManager = GameObject.FindGameObjectsWithTag("SpawnManager")[0];
        SpawnManager.GetComponent<SpawnManager>().InstantiateObstacles();
    }
}
