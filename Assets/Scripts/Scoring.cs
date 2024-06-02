using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{

    public GameObject gameOverPanel;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle" || collision.collider.tag == "Player")
        {
            //do some effects
            GameManager.Instance.GameOver();
        }
        if(collision.collider.tag == "Food")
        {
            GameManager.Instance.IncreaseScore();
        }
    }
}
