using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    private Vector3 lastpos = new Vector3(0, 0, 0);
    public float moveSpeed = 2;
    public float turnspeed = 180;
    public GameObject bodyPrefab;
    public GameObject TailPrefab;
    public int gap = 100;
    public List<GameObject> BodyList = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        AddTail();
        IncreaseLength();
        IncreaseLength();
        IncreaseLength();
        IncreaseLength();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(transform.up, turnspeed * turn * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseLength();
        }
        positionHistory.Insert(0, transform.position);

        // for keeping the list to only a certain length
        // Debug.Log(positionHistory.Count + " " + positionHistory[0] + " " + positionHistory[positionHistory.Count-1]);
        // if(positionHistory.Count > 1000){positionHistory.RemoveRange(1000,1);}
        int index = 0;
        int ans = new int();
        foreach (GameObject body in BodyList)
        {
            ans = index * gap;
            // Debug.Log("ans "+ans);

            Vector3 point = positionHistory[Mathf.Min(ans, positionHistory.Count - 1)];
            Vector3 pointDir = (point - body.transform.position).normalized;
            // Debug.Log(index + " " + point);
            body.transform.position += pointDir * moveSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;

        }
        lastpos = positionHistory[Mathf.Min(ans, positionHistory.Count - 1)];

        // foreach(var pos in positionHistory){
        //     Debug.Log(pos);
        // }


    }

    void IncreaseLength()
    {
        GameObject body = Instantiate(bodyPrefab);
        body.transform.position = lastpos;
        BodyList.Insert(BodyList.Count - 1, body);
    }
    void AddTail()
    {
        GameObject body = Instantiate(TailPrefab);
        body.transform.position = lastpos;
        BodyList.Add(body);
    }
}