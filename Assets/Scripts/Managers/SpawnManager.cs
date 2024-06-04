using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;
using UnityEngine.AI;
using System.Drawing;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public GameObject[] obstacles;
    public GameObject[] foods;
    public Transform[] foodSpawnPoints;
    public List<Transform> obstacleInstantiationPoints;
    public float obstacleArea;
    public void InstantiateObstacles()
    {
        foreach(var obstaclePoint in obstacleInstantiationPoints)
        {
            float randomChooser = Random.Range(0.0f, 1.0f);
            if(randomChooser < obstacleArea)
            {
                int index = Random.Range(0, obstacles.Length);
                Spawn(obstacles[index], obstaclePoint);
            }
        }
    }

    public void SpawnFood()
    {
        int position_index = Random.Range(0, foodSpawnPoints.Length);
        int food_index = Random.Range(0, foods.Length);
        Spawn(foods[food_index], foodSpawnPoints[position_index]);
    }

    public void Spawn(GameObject _object, Transform point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point.position, -Vector3.up, out hit))
        {
            Vector3 location = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
            Instantiate(_object, location, Quaternion.identity);
        }
    }
}
