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
    public float spawnOffset;
    public Vector3 foodPosition;

    private void Start()
    {
        foodPosition = SpawnFood();
    }
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

    public Vector3 SpawnFood()
    {
        int position_index = Random.Range(0, foodSpawnPoints.Length);
        while (Vector3.Distance(foodSpawnPoints[position_index].position, foodPosition) <= 5)
        {
            position_index = Random.Range(0, foodSpawnPoints.Length);
        }
        int food_index = Random.Range(0, foods.Length);
        return foodPosition = Spawn(foods[food_index], foodSpawnPoints[position_index]);
    }

    public Vector3 Spawn(GameObject _object, Transform point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point.position, -Vector3.up, out hit))
        {
            Vector3 location = new Vector3(hit.point.x, hit.point.y + spawnOffset, hit.point.z);
            Instantiate(_object, location, _object.transform.rotation);
            return location;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
