using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelBehaviour : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public SpawnManager spawnManager;
    public GameObject arrow;
    public float arrowOffset;
    public float spawnOffset;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle" || collision.collider.tag == "Body")
        {
            AudioManager.Instance.PlayOnce(GameManager.Instance.currentLevel.deathSound);
            GameOver();
        }
        if(collision.collider.tag == "Food")
        {
            AudioManager.Instance.PlayOnce(GameManager.Instance.currentLevel.eatSound);
            Debug.Log(GameManager.Instance.currentLevel.eatSound);
            GameManager.Instance.IncreaseScore();
            Destroy(collision.gameObject);
            scoreText.text = "Score: " + GameManager.Instance.currentLevel.currentScore;
            transform.GetComponent<Movement>().increased=true;
            spawnManager.SpawnFood();
        }
    }


    public void Pause()
    {
        AudioManager.Instance.Pause();
        Time.timeScale = 0;
        GameManager.Instance.paused = true;
    }

    public void Resume()
    {
        AudioManager.Instance.Play();
        Time.timeScale = 1;
        GameManager.Instance.paused = false;
        GameManager.Instance.canvas.transform.GetChild(0).gameObject.SetActive(false);
        arrow.SetActive(true);
    }

    public void GameOver()
    {
        Pause();
        GameManager.Instance.canvas.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        GameManager.Instance.paused = false;
        GameManager.Instance.canvas.transform.GetChild(1).gameObject.SetActive(false);
        GameManager.Instance.LoadLevel(GameManager.Instance.currentLevel.levelID);
    }

    public void LoadHomeScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.Menu.SetActive(true);
        AudioManager.Instance.PlayContinuous(AudioManager.Instance.theme);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.paused)
        {
            Pause();
            arrow.SetActive(false);
            GameManager.Instance.canvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            arrow.transform.position = transform.position +new Vector3(0, arrowOffset, 0);
            arrow.transform.right = (transform.position - spawnManager.foodPosition).normalized;
            //Debug.DrawRay(arrow.transform.position, spawnManager.foodPosition - transform.position, Color.red, 1);
        }
    }
}
