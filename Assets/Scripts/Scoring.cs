using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle" || collision.collider.tag == "Player")
        {
            //do some effects
            GameOver();
        }
        if(collision.collider.tag == "Food")
        {
            GameManager.Instance.IncreaseScore();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        GameManager.Instance.paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        GameManager.Instance.paused = false;
        GameManager.Instance.canvas.transform.GetChild(0).gameObject.SetActive(false);
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
        StartCoroutine(GameManager.Instance.LoadSceneAsync());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.paused)
        {
            Pause();
            GameManager.Instance.canvas.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
